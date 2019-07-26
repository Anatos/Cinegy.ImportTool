using System.Collections.Generic;
using Cinegy.ImportTool.Infrastructure;

namespace Cinegy.ImportTool.Device
{
    public interface IDevice
    {
        #region Properties

        string Name { get; }
        List<ITrack> Tracks { get; }

        #endregion
    }
}