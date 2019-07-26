using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Cinegy.ImportTool.Infrastructure.Model;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace Cinegy.ImportTool.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _command;
        private List<IImportMode> _modesProperty;
        private IImportMode _selectedModeProperty;

        private string _statusProperty = string.Empty;

        #region Constructors

        public MainViewModel(IImportService importService)
        {
            if (importService == null) throw new ArgumentNullException(nameof(importService));

            ImportService = importService;

            _modesProperty = ServiceLocator.Current.GetAllInstances<IImportMode>().ToList();

            MessengerInstance.Register<NotificationMessage>(this, arg => Status = arg.Notification);
        }

        #endregion

        #region Properties

        public ICommand Command => _command ?? (_command = new RelayCommand(() =>
        {
            foreach (var i in ServiceLocator.Current.GetAllInstances<IMessengerInjection>().ToList())
                i.Attach(MessengerInstance);
            MessengerInstance.Send(new GenericMessage<string>($"{_selectedModeProperty}"));
        }));

        public IImportService ImportService { get; }

        public List<IImportMode> Modes
        {
            get { return _modesProperty; }
            set
            {
                if (_modesProperty == value) return;

                _modesProperty = value;
                RaisePropertyChanged(nameof(Modes));
            }
        }

        public IImportMode SelectMode
        {
            get { return _selectedModeProperty; }
            set
            {
                if (_selectedModeProperty == value) return;

                _selectedModeProperty = value;
                MessengerInstance.Send(new NotificationMessage($"Mode item changed to {value}!"));
                MessengerInstance.Send(new GenericMessage<string>(value.Name));
                RaisePropertyChanged(nameof(SelectMode));
            }
        }

        public string Status
        {
            get { return _statusProperty; }
            set
            {
                if (_statusProperty == value) return;

                _statusProperty = value;
                RaisePropertyChanged(nameof(Status));
            }
        }

        #endregion
    }
}