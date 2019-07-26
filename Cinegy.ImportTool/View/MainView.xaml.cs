using Cinegy.ImportTool.Infrastructure.Model;
using Cinegy.ImportTool.Model;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Messaging;

namespace Cinegy.ImportTool.View
{
    /// <summary>
    ///     Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        #region Constructors

        public MainView()
        {
            InitializeComponent();

            var msg = ServiceLocator.Current.GetInstance<IMessenger>();
            foreach (var i in ServiceLocator.Current.GetAllInstances<IMessengerInjection>()) i.Attach(msg);

            Title = Title = "Import Tool (MVVM Light)";
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        #endregion
    }
}