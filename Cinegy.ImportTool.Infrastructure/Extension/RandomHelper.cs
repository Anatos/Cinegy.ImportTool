using System;
using System.IO;

namespace Cinegy.ImportTool.Infrastructure.Extension
{
    public static class RandomHelper
    {
        #region Static members

        public static string GetName()
        {
            return Path.GetFileNameWithoutExtension(Path.GetRandomFileName());
        }

        public static Random Random { get; }

        static RandomHelper()
        {
            Random = new Random();
        }

        #endregion
    }
}