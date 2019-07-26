using Cinegy.ImportTool.Infrastructure.Model;

namespace Cinegy.ImportTool.FileSystem.Model
{
    public class FileTrack : ITrack
    {
        #region Constructors

        public FileTrack(string name)
        {
            Name = name;
        }

        public FileTrack(string name, TrackType type, long lenght) : this(name)
        {
            Type = type;
            Length = lenght;
        }

        #endregion

        #region Properties

        public long Length { get; }

        public string Name { get; }

        public TrackType Type { get; }

        #endregion

        #region Override members

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}