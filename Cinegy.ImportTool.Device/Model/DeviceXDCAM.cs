using System.Collections.Generic;
using Autofac;
using Cinegy.ImportTool.Infrastructure;
using GalaSoft.MvvmLight;

// ReSharper disable InconsistentNaming

namespace Cinegy.ImportTool.Device
{
    public class DeviceXDCAM : ObservableObject,
        IDevice,
        IStartable
    {
        private readonly int _boardNumber = 0;

        #region Constructors

        public DeviceXDCAM()
        {
        }

        #endregion

        #region Properties

        public string Name => $"XDCAM {_boardNumber}";

        public List<ITrack> Tracks { get; private set; }

        #endregion

        #region Override members

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region IStartable Members

        public void Start()
        {
            Tracks = new List<ITrack>
            {
                new Track(RandomHelper.GetName(), TrackType.Data, RandomHelper.GetName().Length),
                new Track(RandomHelper.GetName(), TrackType.Video, RandomHelper.GetName().Length),
                new Track(RandomHelper.GetName(), TrackType.Video, RandomHelper.GetName().Length),
                new Track(RandomHelper.GetName(), TrackType.Audio, RandomHelper.GetName().Length),
            };
        }

        #endregion
    }
}