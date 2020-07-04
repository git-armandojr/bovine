using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bovine.Controls
{
    public class PastureCell : ViewCell
    {
        Label idLabel, descriptionLabel, areaLabel;

        public static readonly BindableProperty IDProperty = BindableProperty.Create("ID", typeof(string), typeof(PastureCell), null);
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create("Description", typeof(string), typeof(PastureCell), "Description");
        public static readonly BindableProperty AreaProperty = BindableProperty.Create("Area", typeof(string), typeof(PastureCell), null);

        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public string Area
        {
            get { return (string)GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        public PastureCell()
        {
            var grid = new Grid { Padding = new Thickness(10, 10, 10, 10), HeightRequest = 70 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            idLabel = new Label();
            descriptionLabel = new Label();
            areaLabel = new Label();

            grid.Children.Add(idLabel, 0, 0);
            grid.Children.Add(descriptionLabel, 0, 1);
            grid.Children.Add(areaLabel, 0, 2);

            View = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                idLabel.Text = ID.ToString();
                descriptionLabel.Text = Description;
                areaLabel.Text = Area.ToString();
            }
        }
    }
}
