using Cinegy.ImportTool.Infrastructure.Model;
using Cinegy.ImportTool.ViewModel;
using CommonServiceLocator;
//using GalaSoft.MvvmLight.Ioc;

namespace Cinegy.ImportTool.Model
{
    public class ViewModelLocator : IViewModelRepository
    {
        #region Static members

        public static void Cleanup()
        {
            //SimpleIoc.Default.Unregister<IImportService>();
            //SimpleIoc.Default.Unregister<IDialogService>();

            //SimpleIoc.Default.Unregister<MainViewModel>();

            //SimpleIoc.Default.Reset();
        }

        #endregion

        #region Constructors

        public ViewModelLocator()
        {
            //ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //SimpleIoc.Default.Register<IImportService, ImportService>();
            //SimpleIoc.Default.Register<IDialogService, DialogService>();
            //SimpleIoc.Default.Register<IMessenger, Messenger>();

            //if (ViewModelBase.IsInDesignModeStatic) { }
            //else { }

            //SimpleIoc.Default.Register<MainViewModel>();
        }

        #endregion

        #region Properties

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public IViewModelIndexer ViewModels => ServiceLocator.Current.GetInstance<IViewModelIndexer>();

        #endregion
    }
}