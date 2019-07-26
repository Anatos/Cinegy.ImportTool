using System.Collections.Generic;
using Autofac;
using Cinegy.ImportTool.Infrastructure.Extension;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;

namespace Cinegy.ImportTool.Device
{
    public class DeviceAja : ObservableObject,
        IDevice,
        IStartable
    {
        #region Constructors

        public DeviceAja()
        {
        }

        #endregion

        #region Properties

        public string Name => "Aja";

        public List<ITrack> Tracks { get; set; }

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
                new Track(RandomHelper.GetName(), TrackType.Video, RandomHelper.GetName().Length),
                new Track(RandomHelper.GetName(), TrackType.Data, RandomHelper.GetName().Length),
                new Track(RandomHelper.GetName(), TrackType.Audio, RandomHelper.GetName().Length),
            };
        }

        #endregion
    }
}