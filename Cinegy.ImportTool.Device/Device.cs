using System.Reflection;
using Autofac;
using Cinegy.ImportTool.Device.View;
using Cinegy.ImportTool.Device.ViewModel;
using Cinegy.ImportTool.Infrastructure;
using Cinegy.ImportTool.Infrastructure.Model;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Module = Autofac.Module;

namespace Cinegy.ImportTool.Device
{
    internal class Device : Module,
        IImportMode, IMessengerInjection
    {
        #region Constructors

        public Device(IComponentContext componentContext)
        {
            //Messenger messanger; TODO NOT NULL but NOT invoke on SET from outside
            //if (componentContext.TryResolve(out messanger))
            //    messanger.Register<GenericMessage<string>>(this,
            //        arg =>
            //        {
            //            if (arg.Content == Name)
            //                componentContext.IsRegistered<ExplorerViewModel>();

            //        });
        }

        #endregion

        #region Properties

        public string Name => nameof(Device);

        #endregion

        #region Override members

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<ExplorerViewModel>().InstancePerLifetimeScope();
            builder.RegisterType<ExplorerView>().InstancePerLifetimeScope();

            //builder.RegisterType<IStartable>().AutoActivate().OnActivating(e => e.Instance.Start());

            //var msgr = _scope.ResolveOptional<Messenger>(); TODO NULL
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion

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
    }
}