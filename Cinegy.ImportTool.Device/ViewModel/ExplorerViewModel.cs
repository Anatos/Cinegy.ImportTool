using System.Windows.Input;
using Cinegy.ImportTool.Infrastructure;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Cinegy.ImportTool.Device.ViewModel
{
    public class ExplorerViewModel : ViewModelBase
    {
        private readonly IImportService _importService;
        private RelayCommand<object> _selectedCommand;

        #region Constructors

        public ExplorerViewModel(IImportService importService)
        {
            _importService = importService;
        }

        #endregion

        #region Properties

        public ICommand SelectedCommand
        {
            get
            {
                return _selectedCommand ?? (_selectedCommand = new RelayCommand<object>(args =>
                {
                    var arg = args as ITrack;
                    if (arg != null)
                        _importService.Current = arg;
                }));
            }
        }

        #endregion
    }
}