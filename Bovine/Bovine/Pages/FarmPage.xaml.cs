using Bovine.Models;
using Bovine.Models.AgriFarm;
using Bovine.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FarmPage : ContentPage
    {
        readonly IList<AgriFarm> farms = new ObservableCollection<AgriFarm>();
        readonly AgriFarmManager manager = new AgriFarmManager();

        //ObservableCollection<Farm> farms = new ObservableCollection<Farm>();
        //public ObservableCollection<Farm> Farms { get { return farms; } }

        public FarmPage()
        {
            BindingContext = farms;
            InitializeComponent();                      
            
            ToolbarItem toolbarItemEdit = new ToolbarItem
            {                
                Text = "Editar",                
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };
            toolbarItemEdit.Clicked += OnItemEditClicked;

            ToolbarItem toolbarItemMarker = new ToolbarItem
            {
                Text = "Perímetro",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };
            toolbarItemMarker.Clicked += OnItemMarkerClicked;

            this.ToolbarItems.Add(toolbarItemEdit);
            this.ToolbarItems.Add(toolbarItemMarker);
        }

        async void OnItemMarkerClicked(object sender, EventArgs e)
        {
            if (PolygonMarkerPage.instance == null && listViewFarm.SelectedItem != null)
            {            
                var page = new NavigationPage(new PolygonMarkerPage());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                await Navigation.PushModalAsync(page);
            }
        }

        async void OnItemEditClicked(object sender, EventArgs e)
        {            
            if (FarmDetailPage.instance == null && listViewFarm.SelectedItem != null)
            {
                FarmDetailPage.buttonDeleteVisibility = true;
                var page = new NavigationPage(FarmDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                page.BindingContext = App.SelectedAgriFarm;
                await Navigation.PushModalAsync(page);
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            farms.Clear();

            try
            {
                var farmCollection = await manager.GetAll();

                if (farmCollection.Count() > 0)
                {
                    foreach (AgriFarm farm in farmCollection)
                    {
                        //if (farms.All(f => f.id != farm.id))
                        farms.Add(farm);
                    }

                    if (App.SelectedAgriFarm == null)
                    {
                        listViewFarm.SelectedItem = farms.FirstOrDefault();
                        App.SelectedAgriFarm = farms.FirstOrDefault();
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Log.Warning("ArgumentNullException", e.Message);
            }
            
        }

        private async void OnButtonAddClicked(object sender, EventArgs e)
        {

            if (FarmDetailPage.instance == null)
            {
                var page = new NavigationPage(FarmDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                MessagingCenter.Send(GenerateNewAgriFarmWithId(), "AddItem");
                await Navigation.PushModalAsync(page);
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedAgriFarm = e.SelectedItem as AgriFarm;
            this.Title = "Fazenda " + App.SelectedAgriFarm.name.value;
            this.Title.Remove(0);
        }

        string GenerateNewAgriFarmWithId()
        {

            string id = string.Empty;

            if (farms.Count() == 0)
            {
                id = "urn:ngsi-ld:AgriFarm:0001";
                return id;
            }

            var lastItem = farms.OrderBy(o => o.id).LastOrDefault();

            string[] split = lastItem.id.Split(':');

            int lastId;

            Int32.TryParse(split.LastOrDefault(), out lastId);

            lastId++;

            //split = split.Take(split.Length - 1).ToArray();

            split[split.Length - 1] = String.Format("{0:0000}", lastId);

            id = String.Join(":", split);

            return id;
        }
    }
}