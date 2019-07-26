using System;
using Cinegy.ImportTool.Infrastructure;

namespace Cinegy.ImportTool.Device
{
    public class Track : ITrack
    {
        #region Constructors

        public Track()
        {
            
        }

        public Track(string name, TrackType type, int length)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            Name = name;
            Length = length;

            Type = type;
        }

        #endregion

        #region Properties

        public int Length { get; }

        public string Name { get; }

        public TrackType Type { get; }

        #endregion
    }
}