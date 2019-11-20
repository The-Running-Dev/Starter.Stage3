using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Starter.WPF.Extensions
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;

            if (!(bool) e.NewValue) return;

            var action = new Action(() =>
            {
                uie.Dispatcher?.BeginInvoke((Action) (() =>
                {
                    Console.WriteLine("Setting Focus");
                    uie.Focusable = true;
                    Keyboard.Focus(uie);

                    uie.Focus();
                }));
            });
            
            Task.Factory.StartNew(action);
        }
    }
}