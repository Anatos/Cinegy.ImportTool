namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface ITrack
    {
        #region Properties

        long Length { get; }
        string Name { get; }
        TrackType Type { get; }

        #endregion
    }
}