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
        public static readonly DependencyProperty AddBeamInputClickedProperty = DependencyProperty.RegisterAttached(
            "AddBeamInputClicked", typeof(string), typeof(BaseAttachedProperty), new PropertyMetadata(null, new PropertyChangedCallback(OnAddBeamInputClickedPropertyChanged)));

        public static string GetAddBeamInputClickedProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(AddBeamInputClickedProperty);
        }
        public static void SetAddBeamInputClickedProperty(DependencyObject obj, string value)
        {
            obj.SetValue(AddBeamInputClickedProperty, value);
        }
        private static void OnAddBeamInputClickedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button btn = d as Button;
            if (btn == null) return;

            btn.Click -= AddBeamInputClicked;
            if ((string)e.NewValue != null)
            {
                btn.Click += AddBeamInputClicked;
            }
        }

        private static void AddBeamInputClicked(object sender, RoutedEventArgs e)
        {
            Singleton.Instance.WPFData.BeamInputs.Add(new BeamInput());
        }
    }
}
