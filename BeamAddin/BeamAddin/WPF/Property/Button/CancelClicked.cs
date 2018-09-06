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
        public static readonly DependencyProperty CancelClickedProperty = DependencyProperty.RegisterAttached(
            "CancelClicked", typeof(string), typeof(BaseAttachedProperty), new PropertyMetadata(null, new PropertyChangedCallback(OnCancelClickedPropertyChanged)));

        public static string GetCancelClickedProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(CancelClickedProperty);
        }
        public static void SetCancelClickedProperty(DependencyObject obj, string value)
        {
            obj.SetValue(CancelClickedProperty, value);
        }
        private static void OnCancelClickedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Button btn = d as Button;
            if (btn == null) return;

            btn.Click -= CancelClicked;
            if ((string)e.NewValue != null)
            {
                btn.Click += CancelClicked;
            }
        }

        private static void CancelClicked(object sender, RoutedEventArgs e)
        {
            Singleton.Instance.WPFData.InputForm.Close();
        }
    }
}
