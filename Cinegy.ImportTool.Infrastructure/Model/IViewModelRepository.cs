namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface IViewModelRepository
    {
        #region Properties

        IViewModelIndexer ViewModels { get; }

        #endregion
    }
}