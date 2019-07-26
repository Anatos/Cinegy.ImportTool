using System;
using GalaSoft.MvvmLight;

namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface IViewModelIndexer
    {
        #region Properties

        ViewModelBase this[Type key] { get; }

        #endregion
    }
}