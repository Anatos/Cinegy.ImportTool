using Cinegy.ImportTool.Infrastructure.Model;

namespace Cinegy.ImportTool.FileSystem.ViewModel
{
    public class FileTrack : ITrack

    {
        public int Length { get; }

        public string Name { get; }

        public TrackType Type { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}