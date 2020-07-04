using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bovine.Controls
{
    public class CattleCell : ViewCell
    {
        Label idLabel, identifierLabel, weightLabel;

        public static readonly BindableProperty IDProperty = BindableProperty.Create("ID", typeof(string), typeof(CattleCell), null);
        public static readonly BindableProperty IdentifierProperty = BindableProperty.Create("Identifier", typeof(string), typeof(CattleCell), "Identifier");
        public static readonly BindableProperty WeightProperty = BindableProperty.Create("Weight", typeof(string), typeof(CattleCell), null);

        public string ID
        {
            get { return (string)GetValue(IDProperty); }
            set { SetValue(IDProperty, value);  }
        }

        public string Identifier
        {
            get { return (string)GetValue(IdentifierProperty); }
            set { SetValue(IdentifierProperty, value); }
        }

        public string Weight
        {
            get { return (string)GetValue(WeightProperty); }
            set { SetValue(WeightProperty, value); }
        }

        public CattleCell()
        {
            var grid = new Grid { Padding = new Thickness(10, 10, 10, 10),  HeightRequest = 70  };
            
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });            
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            idLabel = new Label();
            identifierLabel = new Label();
            weightLabel = new Label();

            grid.Children.Add(idLabel, 0, 0);
            grid.Children.Add(identifierLabel, 0, 1);
            grid.Children.Add(weightLabel, 0, 2);

            View = grid;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                idLabel.Text = ID.ToString();
                identifierLabel.Text = Identifier;
                weightLabel.Text = Weight.ToString();
            }
        }
    }
}
