using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Autofac;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Cinegy.ImportTool.FileSystem.Model;

namespace Cinegy.ImportTool.FileSystem.ViewModel
{
    public class ExplorerViewModel : ViewModelBase
    {
        private readonly IImportService _importService;
        private RelayCommand<object> _selectedCommand;
        private TreeViewModel _treeVm = new TreeViewModel() /*ServiceLocator.Current.GetInstance<TreeViewModel>()*/;

        #region Constructors

        public ExplorerViewModel(ILifetimeScope scope, IImportService importService)
        {
            _importService = importService;
            ItemsSource = new ObservableCollection<ITrack>();
            MessengerInstance.Register<GenericMessage<DirectoryInfo>>(this,
                i =>
                {
                    ItemsSource.Clear();
                    foreach (var f in i.Content.GetFiles())
                    {
                        ItemsSource.Add(new FileTrack(f.FullName, TrackType.Video, f.Length));
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
                return _selectedCommand ?? (_selectedCommand = new RelayCommand<object>(e =>
                {
                    var args = e as SelectionChangedEventArgs;
                    if (args == null) return;

                    var arg = args.AddedItems[0] as ITrack;
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