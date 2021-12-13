using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;

namespace DragonJetzNotepad.AttachedProperties {
    /// <summary>
    /// A class for allowing horizontal scrolling on any control that has a scrollviewer 
    /// </summary>
    public static class HorizontalScrolling {
        public static readonly DependencyProperty UseHorizontalScrollingProperty = 
            DependencyProperty.RegisterAttached(
                "UseHorizontalScrolling",
                typeof(bool),
                typeof(HorizontalScrolling), 
                new PropertyMetadata(new PropertyChangedCallback(OnUseHorizontalScrollingValueChanged)));

        public static readonly DependencyProperty ForceHorizontalScrollingProperty = 
            DependencyProperty.RegisterAttached(
                "ForceHorizontalScrolling",
                typeof(bool), 
                typeof(HorizontalScrolling), 
                new PropertyMetadata(new PropertyChangedCallback(OnUseHorizontalScrollingValueChanged)));

        public static readonly DependencyProperty HorizontalScrollingAmountProperty =
            DependencyProperty.RegisterAttached(
                "HorizontalScrollingAmount",
                typeof(int),
                typeof(HorizontalScrolling),
                new PropertyMetadata(SystemParameters.WheelScrollLines)); //Forms.SystemInformation.MouseWheelScrollLines));

        public static bool SCROLL_HORIZONTAL_WITH_SHIFT_MOUSEWHEEL = true;

        public static bool GetUseHorizontalScrollingValue(DependencyObject d) {
            return (bool)d.GetValue(UseHorizontalScrollingProperty);
        }

        public static void SetUseHorizontalScrollingValue(DependencyObject d, bool value) {
            d.SetValue(UseHorizontalScrollingProperty, value);
        }

        public static bool GetForceHorizontalScrollingValue(DependencyObject d) {
            return (bool)d.GetValue(ForceHorizontalScrollingProperty);
        }

        public static void SetForceHorizontalScrollingValue(DependencyObject d, bool value) {
            d.SetValue(ForceHorizontalScrollingProperty, value);
        }

        public static int GetHorizontalScrollingAmountValue(DependencyObject d) {
            return (int)d.GetValue(HorizontalScrollingAmountProperty);
        }

        public static void SetHorizontalScrollingAmountValue(DependencyObject d, int value) {
            d.SetValue(HorizontalScrollingAmountProperty, value);
        }

        public static void OnUseHorizontalScrollingValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            if (sender is UIElement element) {
                element.PreviewMouseWheel -= OnPreviewMouseWheel;
                element.PreviewMouseWheel += OnPreviewMouseWheel;
            }
            else {
                throw new Exception("Attached property must be used with a UIElement");
            }
        }

        private static void OnPreviewMouseWheel(object sender, MouseWheelEventArgs args) {
            bool forceHorizontalScrolling;
            int horizontalScrollingAmount;

            if (!SCROLL_HORIZONTAL_WITH_SHIFT_MOUSEWHEEL)
                return;

            if (sender is UIElement element) {
                ScrollViewer scrollViewer = FindDescendant<ScrollViewer>(element);
                if (scrollViewer == null) {
                    throw new Exception($"Element ({element.GetType().Name}) did not contain a scroll viewer as a descendant");
                }

                forceHorizontalScrolling = GetForceHorizontalScrollingValue(element);
                horizontalScrollingAmount = GetHorizontalScrollingAmountValue(element);
                if (horizontalScrollingAmount < 1)
                    horizontalScrollingAmount = 3;

                if (!forceHorizontalScrolling && (Keyboard.Modifiers == ModifierKeys.Shift || Mouse.MiddleButton == MouseButtonState.Pressed)) {
                    if (args.Delta < 0) {
                        for (int i = 0; i < horizontalScrollingAmount; i++) {
                            scrollViewer.LineRight();
                        }
                    }
                    else {
                        for (int i = 0; i < horizontalScrollingAmount; i++) {
                            scrollViewer.LineLeft();
                        }
                    }

                    args.Handled = true;
                }
                else if (forceHorizontalScrolling) {
                    if (args.Delta < 0) {
                        for (int i = 0; i < horizontalScrollingAmount; i++) {
                            scrollViewer.LineRight();
                        }
                    }
                    else {
                        for (int i = 0; i < horizontalScrollingAmount; i++) {
                            scrollViewer.LineLeft();
                        }
                    }

                    args.Handled = true;
                }
            }
        }

        private static T FindDescendant<T>(DependencyObject d) where T : DependencyObject {
            if (d == null) {
                return null;
            }

            for (int i = 0, childCount = VisualTreeHelper.GetChildrenCount(d); i < childCount; i++) {
                DependencyObject child = VisualTreeHelper.GetChild(d, i);
                T result = child as T ?? FindDescendant<T>(child);
                if (result != null) {
                    return result;
                }
            }

            return null;
        }
    }
}
