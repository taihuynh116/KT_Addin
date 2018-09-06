using Single;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BeamAddin
{
    public partial class BaseAttachedProperty
    {
        public static readonly DependencyProperty DeleteBeamInputClickedProperty = DependencyProperty.RegisterAttached(
            "DeleteBeamInputClicked", typeof(string), typeof(BaseAttachedProperty), new PropertyMetadata(null, new PropertyChangedCallback(OnDeleteBeamInputClickedPropertyChanged)));

        public static string GetDeleteBeamInputClickedProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(DeleteBeamInputClickedProperty);
        }
        public static void SetDeleteBeamInputClickedProperty(DependencyObject obj, string value)
        {
            obj.SetValue(DeleteBeamInputClickedProperty, value);
        }
        private static void OnDeleteBeamInputClickedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button btn = d as Button;
            if (btn == null) return;

            btn.Click -= DeleteBeamInputClicked;
            if ((string)e.NewValue != null)
            {
                btn.Click += DeleteBeamInputClicked;
            }
        }

        private static void DeleteBeamInputClicked(object sender, RoutedEventArgs e)
        {
            Singleton.Instance.WPFData.BeamInputs.Remove(((Button)sender).DataContext as BeamInput);
        }
    }
}
