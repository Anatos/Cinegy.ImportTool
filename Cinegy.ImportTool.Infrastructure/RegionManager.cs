using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cinegy.ImportTool.Infrastructure
{
    public class RegionManager
    {
        #region Property definitions

        public static readonly DependencyProperty RegionProperty =
            DependencyProperty.RegisterAttached("Region", typeof(string), typeof(RegionManager), new UIPropertyMetadata(PropertyChanged));

        #endregion

        #region Static members

        public static RegionManager Current { get; } = new RegionManager();

        public static string GetRegion(DependencyObject obj)
        {
            return (string)obj.GetValue(RegionProperty);
        }

        public static void PropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue != null) Current.Unregister(sender, e.OldValue as string);
            if (e.NewValue != null) Current.Register(sender, e.NewValue as string);
        }

        public static void SetRegion(DependencyObject obj, string value)
        {
            obj.SetValue(RegionProperty, value);
        }

        #endregion

        private readonly Dictionary<string, DependencyObject> _regions = new Dictionary<string, DependencyObject>();

        #region Members

        public void Register(DependencyObject sender, string name)
        {
            if (sender.GetType().GetProperty(ContentControl.ContentProperty.Name) == null) throw new ArgumentException();

            _regions[name] = sender;
        }

        public void Set(string name, object element)
        {
            if (_regions.ContainsKey(name))
                _regions[name].SetValue(ContentControl.ContentProperty, element);
        }

        public void Unregister(DependencyObject sender, string name)
        {
            if (_regions.Contains(new KeyValuePair<string, DependencyObject>(name, sender)))
                _regions.Remove(name);
        }

        #endregion
    }
}