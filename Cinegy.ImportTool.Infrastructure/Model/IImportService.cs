using GalaSoft.MvvmLight.CommandWpf;

namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface IImportService
    {
        #region Properties

        RelayCommand Command { get; }
        ITrack Current { get; set; }

        #endregion
    }
}