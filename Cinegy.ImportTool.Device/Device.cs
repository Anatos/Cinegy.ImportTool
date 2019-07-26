using System.Reflection;
using Autofac;
using Cinegy.ImportTool.Device.ViewModel;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Module = Autofac.Module;

namespace Cinegy.ImportTool.Device
{
    internal class Device : Module,
        IImportMode
    {
        #region Constructors

        public Device(IComponentContext componentContext)
        {
            Messenger messanger;
            if (componentContext.TryResolve(out messanger))
                messanger.Register<GenericMessage<string>>(this,
                    arg =>
                    {
                        if (arg.Content == Name)
                            componentContext.IsRegistered<ExplorerViewModel>();

                    });
        }

        #endregion

        #region Properties

        public string Name => nameof(Device);

        #endregion

        #region Override members

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules();

            //builder.RegisterAssemblyTypes();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<ExplorerViewModel>().As<ViewModelBase>();

            //builder.RegisterType<IStartable>().AutoActivate().OnActivating(e => e.Instance.Start());

            //var msgr = _scope.ResolveOptional<Messenger>();
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}