using System;
using System.Globalization;
using System.Windows.Data;
using Cinegy.ImportTool.Infrastructure.Model;
using GalaSoft.MvvmLight;

namespace Cinegy.ImportTool.Infrastructure.Converter
{
    public class ViewModelTypeResolver : IValueConverter
    {
        #region Static members

        public static ViewModelTypeResolver Singleton { get; } = new ViewModelTypeResolver();

        #endregion

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var repository = value as IViewModelIndexer;
            ViewModelBase item = null;

            try
            {
                item = repository?[parameter as Type];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return item;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}