using System.Collections.Generic;
using Cinegy.ImportTool.Infrastructure.Model;

namespace Cinegy.ImportTool.FileSystem.Extension
{
    internal class TrackComparer : IEqualityComparer<ITrack>
    {
        #region IEqualityComparer<DescriptorBase> Members

        public int GetHashCode(ITrack arg)
        {
            return arg.Name?.GetHashCode() ?? 0;
        }

        public bool Equals(ITrack x1, ITrack x2)
        {
            if (ReferenceEquals(x1, x2))
                return true;
            if (ReferenceEquals(x1, null) ||
                ReferenceEquals(x2, null))
                return false;
            var result = x1.Name == x2.Name;
            return result;
        }

        #endregion
    }
}