using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Cinegy.ImportTool.Infrastructure;
using Cinegy.ImportTool.Infrastructure.Model;
using Cinegy.ImportTool.Model;
using Cinegy.ImportTool.ViewModel;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;

namespace Cinegy.ImportTool
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Static members

        public static IContainer Container { get; set; }

        static App()
        {
            DispatcherHelper.Initialize();
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            var message = "Unrecoverable unhandled exception has occurred.";
            if (exception != null) message += $"Details: {exception.Message}";

            Debug.WriteLine(new ApplicationException(message, exception));
        }

        private static IEnumerable<Assembly> GetPlugins<T>()
        {
            var baseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            var assembly = AssemblyName.GetAssemblyName(Assembly.GetExecutingAssembly().Location).Name;

            Func<string, bool> isPluginAssembly =
                file =>
                {
                    var name = Path.GetFileNameWithoutExtension(file);
                    return string.Equals(Path.GetExtension(file), ".dll", StringComparison.OrdinalIgnoreCase) &&
                           !$"{assembly}.Infrastructure".Equals(name) &&
                           $"{name}".StartsWith($"{assembly}.");
                };

            var assemblies = new List<Assembly>();
            foreach (var s in Directory.GetFiles(baseDirectoryPath).Where(isPluginAssembly))
            {
                Assembly currentAssembly;

                try
                {
                    currentAssembly = Assembly.Load(AssemblyName.GetAssemblyName(new FileInfo(s).FullName));
                }
                catch (Exception)
                {
                    continue;
                }

                currentAssembly.GetTypes()
                    .Where(t => t != typeof(T) && typeof(T).IsAssignableFrom(t)).ToList()
                    .ForEach(x =>
                    {
                        assemblies.Add(currentAssembly);
                        //builder.RegisterType(x).AsImplementedInterfaces();
                    });
            }

            return assemblies.ToArray();
        }

        #endregion

        #region Override members

        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            DispatcherUnhandledException += DispatcherOnUnhandledException;

            base.OnStartup(e);
            
            var builder = new ContainerBuilder();

            builder.RegisterType<ImportService>().As<IImportService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<Messenger>().As<IMessenger>().SingleInstance();

            builder.RegisterType<ViewModelIndexer>().As<IViewModelIndexer>().SingleInstance();
            builder.RegisterType<ViewModelLocator>().AsSelf().SingleInstance();

            builder.RegisterType<MainViewModel>();

            var pluginAssemblies = GetPlugins<IImportMode>();
            foreach (var a in pluginAssemblies) builder.RegisterAssemblyModules(a);

            Container = builder.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));

            App.Current.Resources["Locator"] = ServiceLocator.Current.GetInstance<ViewModelLocator>();
        }

        #endregion

        #region Event handlers

        public void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;

            {
                var message = e.Exception.Message;
                Debug.WriteLine(new ApplicationException(message, e.Exception));
            }
        }

        #endregion
    }
}