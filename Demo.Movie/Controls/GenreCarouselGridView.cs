
using System.Collections.Generic;
using Demo.Movie.Core.Model;
using Xamarin.Forms;

namespace Demo.Movie.Controls
{
    public class GenreCarouselGridView : Grid
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.CreateAttached(nameof(ItemTemplate), typeof(DataTemplate), typeof(GenreCarouselGridView), null, BindingMode.OneWay);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.CreateAttached(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(GenreCarouselGridView), null, BindingMode.OneWay, propertyChanged: OnItemsSourcePropertyChanged);

        public static readonly BindableProperty HeightDefinitionProperty =
            BindableProperty.CreateAttached(nameof(HeightDefinition), typeof(GridLength), typeof(GenreCarouselGridView), GridLength.Star, BindingMode.OneWay);

        public static readonly BindableProperty WidthDefinitionProperty =
            BindableProperty.CreateAttached(nameof(WidthDefinition), typeof(GridLength), typeof(GenreCarouselGridView), GridLength.Star, BindingMode.OneWay);


        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public IEnumerable<Genre> ItemsSource
        {
            get => (IEnumerable<Genre>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public GridLength HeightDefinition
        {
            get => (GridLength)GetValue(HeightDefinitionProperty);
            set => SetValue(HeightDefinitionProperty, value);
        }

        public GridLength WidthDefinition
        {
            get => (GridLength)GetValue(WidthDefinitionProperty);
            set => SetValue(WidthDefinitionProperty, value);
        }

        public GenreCarouselGridView() { }

        public void RenderCarouselGridLayout()
        {
            if (ItemsSource == null)
            {
                return;
            }

            Children.Clear();
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

            ColumnDefinitions.Add(new ColumnDefinition { Width = WidthDefinition });

            int rows = 0;

            foreach (var genre in ItemsSource)
            {
                RowDefinitions.Add(new RowDefinition { Height = HeightDefinition });

                var view = ItemTemplate.CreateContent() as View;

                if (view != null)
                {
                    view.BindingContext = genre;
                }
                else
                {
                    view = CreateDefaultView(genre);
                }

                Children.Add(view, 1, rows);

                rows++;
            }
        }

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var grid = bindable as GenreCarouselGridView;

            grid.RenderCarouselGridLayout();
        }

        private View CreateDefaultView(object item)
        {
            return new Label()
            {
                Text = item.ToString()
            };
        }
    }
}
