using System.Reflection;
using Autofac;
using Cinegy.ImportTool.FileSystem.View;
using Cinegy.ImportTool.FileSystem.ViewModel;
using Cinegy.ImportTool.Infrastructure;
using Cinegy.ImportTool.Infrastructure.Model;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Module = Autofac.Module;

namespace Cinegy.ImportTool.FileSystem
{
    internal class FileSystem : Module,
        IImportMode,
        IMessengerInjection
    {
        #region Properties

        public string Name => "File System";

        #endregion

        #region Override members

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<ExplorerViewModel>().InstancePerLifetimeScope();
            builder.RegisterType<ExplorerView>().InstancePerLifetimeScope();

            //builder.RegisterType<TypeRequiringWarmStart>().AutoActivate().OnActivating(e => e.Instance.Start());
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region IMessengerInjection Members

        public void Attach(IMessenger rootMsgr)
        {
            var explorerView = ServiceLocator.Current.GetInstance<ExplorerView>();

            RegionManager.Current.Set(ViewRegions.Plugin, explorerView);

            rootMsgr.Register<GenericMessage<string>>(this,
                arg =>
                {
                    if (arg.Content != Name) return;
                    else
                    {
                        RegionManager.Current.Set(ViewRegions.Plugin, ServiceLocator.Current.GetInstance<ExplorerView>());
                    }
                },
                true);
        }

        #endregion
    }
}