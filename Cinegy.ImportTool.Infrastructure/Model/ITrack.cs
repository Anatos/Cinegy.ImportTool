namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface ITrack
    {
        #region Properties

        int Length { get; }
        string Name { get; }
        TrackType Type { get; }

        #endregion
    }
}