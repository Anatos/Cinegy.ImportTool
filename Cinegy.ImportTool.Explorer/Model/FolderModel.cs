using System;
using System.Collections.ObjectModel;
using System.IO;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;

namespace Cinegy.ImportTool.FileSystem.Model
{
    public class FolderModel : ObservableObject,
        IFolderSync
    {
        #region Constructors

        public FolderModel(DirectoryInfo directory)
        {
            if (directory == null) throw new ArgumentNullException(nameof(directory));

            Directory = directory;
            Children = new ObservableCollection<FolderModel>();
        }

        #endregion

        #region Properties

        public ObservableCollection<FolderModel> Children { get; }

        public DirectoryInfo Directory { get; }

        #endregion

        #region IFolderSync Members

        public void Loaded()
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                foreach (var directory in Directory.EnumerateDirectories())
                {
                    Children.Add(new FolderModel(directory));
                }
            });
        }

        public void Unloaded()
        {
            Children.Clear();
        }

        #endregion

        public override string ToString()
        {
            return Directory.Name;
        }
    }
}