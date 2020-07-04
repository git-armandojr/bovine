using Bovine.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Bovine
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            //On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            NavigationPage farmPage = new NavigationPage(new FarmPage());
            farmPage.IconImageSource = "farm.png";
            farmPage.Title = "Fazenda";
            farmPage.BarBackgroundColor = Color.FromHex("0078D7");
            this.Children.Add(farmPage);

            NavigationPage pasturePage = new NavigationPage(new PasturePage());
            pasturePage.IconImageSource = "grassland.png";
            pasturePage.Title = "Pasto";
            pasturePage.BarBackgroundColor = Color.FromHex("0078D7");
            this.Children.Add(pasturePage);

            NavigationPage cattlePage = new NavigationPage(new CattlePage());
            cattlePage.IconImageSource = "cow.png";
            cattlePage.Title = "Gado";
            cattlePage.BarBackgroundColor = Color.FromHex("0078D7");
            this.Children.Add(cattlePage);

            NavigationPage gpsPage = new NavigationPage(new GpsPage());
            gpsPage.IconImageSource = "point.png";
            gpsPage.Title = "Gps";
            gpsPage.BarBackgroundColor = Color.FromHex("0078D7");
            this.Children.Add(gpsPage);

            //this.Children.Add(new FarmPage());
            //this.Children.Add(new PasturePage());
            //this.Children.Add(new CattlePage());
            //this.Children.Add(new HomePage());
        }
    }
}
