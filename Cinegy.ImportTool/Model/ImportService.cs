using Cinegy.ImportTool.Infrastructure;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;

namespace Cinegy.ImportTool.Model
{
    public class ImportService : ObservableObject,
        IImportService
    {
        private readonly IDialogService _dialogService;
        private RelayCommand _command;

        private ITrack _currentProperty;

        #region Constructors

        public ImportService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #endregion

        #region Properties

        public RelayCommand Command => _command ?? (_command = new RelayCommand(Execute, CanExecute));

        public ITrack Current
        {
            get { return _currentProperty; }
            set
            {
                if (_currentProperty == value) return;

                _currentProperty = value;
                RaisePropertyChanged(nameof(Current));

                Command.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Members

        private bool CanExecute()
        {
            return Current != null;
        }

        private void Execute()
        {
            if (!CanExecute()) return;

            var result = $"Executed import for {Current}!";
            Messenger.Default.Send(result);
            _dialogService.ShowMessageBox(result, nameof(ImportService));
        }

        #endregion
    }
}