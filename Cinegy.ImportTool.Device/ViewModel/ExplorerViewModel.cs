using System.Collections.Generic;
using System.Windows.Input;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Cinegy.ImportTool.Device.ViewModel
{
    public class ExplorerViewModel : ViewModelBase
    {
        private readonly IImportService _importService;

        private List<IDevice> _itemSource = new List<IDevice>();
        private RelayCommand<object> _selectedCommand;

        #region Constructors

        public ExplorerViewModel(IImportService importService)
        {
            _importService = importService;
        }

        #endregion

        #region Properties

        public List<IDevice> ItemsSource
        {
            get { return _itemSource; }
            set
            {
                if (_itemSource == value) return;

                _itemSource = value;
                RaisePropertyChanged(nameof(ItemsSource));
            }
        }

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