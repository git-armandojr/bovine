using Bovine.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Bovine.Pages
{
    public class PolygonsPage : ContentPage
    {

        ObservableCollection<Position> positions = new ObservableCollection<Position>();

        public ObservableCollection<Position> Position { get { return positions; } }

        Map map;
        Polygon polygon;
        public PolygonsPage()
        {
            Title = "Polygon";
            BindingContext = positions;
            //Visual.IsMaterial();

            map = new Map();

            Button polygonButton = new Button
            {
                Text = "construir"
            };
            polygonButton.Clicked += BuildPolygon;

            IList<Position> listPositions = App.Positions;

            polygon = new Polygon();
            polygon.StrokeWidth = App.Positions.Count;
            polygon.StrokeColor = Color.FromHex("#1BA1E2");
            polygon.FillColor = Color.FromHex("#881BA1E2");

            foreach(Position position in App.Positions)
            {
                polygon.Geopath.Add(position);
            }


            //polygon = new Polygon
            //{
            //    StrokeWidth = 8,
            //    StrokeColor = Color.FromHex("#1BA1E2"),
            //    FillColor = Color.FromHex("#881BA1E2"),
            //    Geopath =
            //    {
            //        new Position(-23.6928550941079,-46.626814417541),
            //        new Position(-23.6921934716355,-46.6265877708793),
            //        new Position(-23.6932536010604,-46.6259705275297)
            //    }
            //};

            Content = new StackLayout
            {
                Children = {
                    map,
                    polygonButton
                }
            };
            
            map.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(-23.6925783148035, -46.625897269696), Distance.FromMiles(0.04)));
        }

        private void BuildPolygon(object sender, EventArgs e)
        {
            if (!map.MapElements.Contains(polygon))
            {
                map.MapElements.Add(polygon);
            }
        }
    }
}