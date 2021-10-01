using System;
using System.Linq;
using Demo.Movie.Helpers;
using Xamarin.Forms;

namespace Demo.Movie.Behaviors
{
    public class OnButtonTouchBehavior : Behavior<Button>
    {
        private double _smallerSize = 0.97;

        public static readonly BindableProperty AttachBehaviorProperty =
            BindableProperty.CreateAttached("AttachBehavior", typeof(bool), typeof(OnButtonTouchBehavior), false, propertyChanged: OnAttachBehaviorChanged);

        public static bool GetAttachBehavior(BindableObject view)
        {
            return (bool)view.GetValue(AttachBehaviorProperty);
        }

        public static void SetAttachBehavior(BindableObject view, bool value)
        {
            view.SetValue(AttachBehaviorProperty, value);
        }

        protected override void OnAttachedTo(Button button)
        {
            base.OnAttachedTo(button);

            button.Clicked += OnButtonClicked;
            button.Pressed += OnButtonPressed;
            button.Released += OnButtonReleased;
        }

        protected override void OnDetachingFrom(Button button)
        {
            button.Clicked -= OnButtonClicked;
            button.Pressed -= OnButtonPressed;
            button.Released -= OnButtonReleased;

            base.OnDetachingFrom(button);
        }

        private void OnButtonClicked(object sender, EventArgs args)
        {
            var button = sender as Button;

            button.ScaleDownTo(_smallerSize);

            button.ScaleToOriginalSize();
        }

        private void OnButtonPressed(object sender, EventArgs args)
        {
            var button = sender as Button;

            button.ScaleDownTo(_smallerSize);
        }

        private void OnButtonReleased(object sender, EventArgs args)
        {
            var button = sender as Button;

            button.ScaleToOriginalSize();
        }

        private static void OnAttachBehaviorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var button = bindable as Button;

            if (button == null)
            {
                return;
            }

            bool attachBehavior = (bool)newValue;

            if (attachBehavior)
            {
                button.Behaviors.Add(new OnButtonTouchBehavior());
            }
            else
            {
                var behaviorToRemove = button.Behaviors.FirstOrDefault(b => b is OnButtonTouchBehavior);

                if (behaviorToRemove != null)
                {
                    button.Behaviors.Remove(behaviorToRemove);
                }
            }
        }
    }
}
