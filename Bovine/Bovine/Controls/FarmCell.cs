using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bovine.Controls
{
    public class FarmCell : ViewCell
    {
        Label nameLabel, streetLabel, localityLabel;

        public static readonly BindableProperty NameProperty = BindableProperty.Create("Name", typeof(string), typeof(FarmCell), "Name");
        public static readonly BindableProperty StreetProperty = BindableProperty.Create(nameof(Street), typeof(string), typeof(FarmCell), "Street");
        public static readonly BindableProperty LocalityProperty = BindableProperty.Create(nameof(Locality), typeof(string), typeof(FarmCell), "Locality");

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public string Street
        {
            get { return (string)GetValue(StreetProperty); }
            set { SetValue(StreetProperty, value); }
        }

        public string Locality
        {
            get { return (string)GetValue(LocalityProperty); }
            set { SetValue(LocalityProperty, value); }
        }

        public FarmCell()
        {
            var grid = new Grid { Padding = new Thickness(10, 10, 10, 10), HeightRequest = 70 };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            nameLabel = new Label();
            streetLabel = new Label();
            localityLabel = new Label();

            grid.Children.Add(nameLabel, 0, 0);
            grid.Children.Add(streetLabel, 0, 1);
            grid.Children.Add(localityLabel, 0, 2);

            View = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                nameLabel.Text = Name;
                streetLabel.Text = Street;
                localityLabel.Text = Locality;
            }
        }
    }
}
