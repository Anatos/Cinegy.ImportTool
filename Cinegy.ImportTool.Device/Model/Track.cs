using System;
using Cinegy.ImportTool.Infrastructure.Model;

namespace Cinegy.ImportTool.Device
{
    public class Track : ITrack
    {
        #region Constructors

        public Track(string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        public Track(string name, TrackType type, long length) : this(name)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            Length = length;

            Type = type;
        }

        #endregion

        #region Properties

        public long Length { get; }

        public string Name { get; }

        public TrackType Type { get; }

        #endregion
    }
}