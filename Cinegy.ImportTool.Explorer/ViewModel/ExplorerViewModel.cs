using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Autofac;
using Cinegy.ImportTool.FileSystem.Model;
using Cinegy.ImportTool.Infrastructure.Model;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace Cinegy.ImportTool.FileSystem.ViewModel
{
    public class FileTrack : ITrack

{
        private int _length;
        private string _name;
        private TrackType _type;

        public int Length
        {
            get { return _length; }
        }

        public string Name
        {
            get { return _name; }
        }

        public TrackType Type
        {
            get { return _type; }
        }
}
    public class ExplorerViewModel : ViewModelBase
    {
        private readonly ILifetimeScope _scope;
        private readonly IImportService _importService;
        private RelayCommand<object> _selectedCommand;
        private TreeViewModel _treeVm = new TreeViewModel() /*ServiceLocator.Current.GetInstance<TreeViewModel>()*/;

        #region Constructors

        public ExplorerViewModel(ILifetimeScope scope, IImportService importService)
        {
            _scope = scope;

            _importService = importService;
            ItemsSource = new ObservableCollection<ITrack>();
            MessengerInstance.Register<GenericMessage<DirectoryInfo>>(this,
                i =>
                {
                    foreach (var f in i.Content.GetFiles())
                    {
                        ItemsSource.Add(new FileTrack());
                    }
                    
                });
        }

        #endregion

        #region Properties

        public ObservableCollection<ITrack> ItemsSource { get; }

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

        public TreeViewModel TreeViewModel
        {
            get { return _treeVm; }
            set
            {
                if (_treeVm == value) return;

                _treeVm = value;
                RaisePropertyChanged(nameof(TreeViewModel));
            }
        }

        #endregion
    }
}