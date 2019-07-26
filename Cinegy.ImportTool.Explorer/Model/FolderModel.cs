using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
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
                try
                {
                    foreach (var directory in Directory.EnumerateDirectories())
                    {
                        Children.Add(new FolderModel(directory));
                        Thread.Sleep(100);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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