using System.Collections.Generic;
using Cinegy.ImportTool.FileSystem.Model;

namespace Cinegy.ImportTool.FileSystem.Extension
{
    internal class ContainerComparer : IEqualityComparer<FolderModel>
    {
        #region IEqualityComparer<DescriptorBase> Members

        public int GetHashCode(FolderModel arg)
        {
            return arg.GetHashCode();
        }

        public bool Equals(FolderModel x1, FolderModel x2)
        {
            if (ReferenceEquals(x1, x2)) return true;

            if (ReferenceEquals(x1, null) || ReferenceEquals(x2, null))
                return false;

            return GetHashCode(x1) == GetHashCode(x2);
        }

        #endregion
    }
}