using Bovine.Models;
using Bovine.Models.AgriParcel;
using Bovine.Services;
using Bovine.ViewModels;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasturePage : ContentPage
    {
        readonly IList<AgriParcel> parcels = new ObservableCollection<AgriParcel>();
        readonly AgriParcelManager manager = new AgriParcelManager();

        //ObservableCollection<Pasture> pastures = new ObservableCollection<Pasture>();
        //public ObservableCollection<Pasture> Pastures { get { return pastures; } }

        public PasturePage()
        {
            BindingContext = parcels;
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

            //listViewPasture.ItemsSource = pastures;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            parcels.Clear();

            try
            {
                var parcelCollection = await manager.GetAll();

                if (parcelCollection.Count() > 0)
                {
                    foreach (AgriParcel parcel in parcelCollection)
                    {
                        //if (parcels.All(p => p.id != parcels.id))
                        parcels.Add(parcel);
                    }

                    if (App.SelectedAgriParcel == null)
                    {
                        listViewPasture.SelectedItem = parcels.FirstOrDefault();
                        App.SelectedAgriParcel = parcels.FirstOrDefault();
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Log.Warning("ArgumentNullException", e.Message);
            }

            /*
            if (App.SelectedFarm != null)
            {
                listViewPasture.ItemsSource = await App.PastureDatabase.GetPasturesByFarmAsyncExtensions(App.SelectedFarm);
                buttonNew.IsEnabled = true;
            }                
            else
            {
                List<Farm> firstFarm = await App.FarmDatabase.GetFarmsAsync();
                if (App.SelectedPasture == null && firstFarm.Count > 0) // se nenhum pasto estiver selecionado, o da primeira fazenda será
                {                    
                    listViewPasture.ItemsSource = await App.PastureDatabase.GetPasturesByFarmAsyncExtensions(firstFarm.FirstOrDefault());
                    listViewPasture.SelectedItem = await App.PastureDatabase.GetPasturesByFarmAsyncExtensions(firstFarm.FirstOrDefault());
                }
                else { buttonNew.IsEnabled = false; }                
            }

            //Pasture pasture = ((List<Pasture>)listViewPasture.ItemsSource).FirstOrDefault();
            */

        }

        async Task<List<Pasture>> GetAllPastureByFarmID(int farmID)
        {            
            return await App.PastureDatabase.GetPasturesAsync();
        }

        async void OnItemMarkerClicked(object sender, EventArgs e)
        {
            if (PolygonMarkerPage.instance == null && listViewPasture.SelectedItem != null)
            {
                var page = new NavigationPage(new PolygonMarkerPage());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                await Navigation.PushModalAsync(page);
            }

            //if (PolygonMarkerPage.instance == null && listViewPasture.SelectedItem != null)
            //{
            //    var page = new NavigationPage(new MarkerPage());
            //    page.BarBackgroundColor = Color.FromHex("0078D7");
            //    page.BindingContext = App.SelectedPasture;
            //    //listViewFarm.SelectedItem = null;
            //    await Navigation.PushModalAsync(page);
            //}

            //if (MarkerPage.instance == null)
            //{
            //    var page = new NavigationPage(MarkerPage.GetInstance());
            //    page.BarBackgroundColor = Color.FromHex("0078D7");
            //    page.BindingContext = App.SelectedPasture;
            //    //listViewFarm.SelectedItem = null;
            //    await Navigation.PushModalAsync(page);
            //}
        }

        async void OnItemEditClicked(object sender, EventArgs e)
        {
            if (PastureDetailPage.instance == null && listViewPasture.SelectedItem != null)
            {
                PastureDetailPage.buttonDeleteVisibility = true;
                var page = new NavigationPage(PastureDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                page.BindingContext = App.SelectedAgriParcel;
                await Navigation.PushModalAsync(page);
            }
            //if (PastureDetailPage.instance == null && listViewPasture.SelectedItem != null)
            //{
            //    PastureDetailPage.buttonDeleteVisibility = true;
            //    var page = new NavigationPage(PastureDetailPage.GetInstance());
            //    page.BarBackgroundColor = Color.FromHex("0078D7");
            //    page.BindingContext = App.SelectedPasture;
            //    //listViewFarm.SelectedItem = null;
            //    await Navigation.PushModalAsync(page);
            //}
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedAgriParcel = e.SelectedItem as AgriParcel;
            this.Title = "Pasto " + App.SelectedAgriParcel.id;
            this.Title.Remove(0);
            //App.SelectedPasture = e.SelectedItem as Pasture;
        }

        async void OnButtonAddClicked(object sender, EventArgs e)
        {
            if (PastureDetailPage.instance == null)
            {
                PastureDetailPage.buttonDeleteVisibility = false;
                var page = new NavigationPage(PastureDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                MessagingCenter.Send(GenerateNewAgriParcelWithId(), "AddItem");
                await Navigation.PushModalAsync(page);
            }
        }

        string GenerateNewAgriParcelWithId()
        {

            string id = string.Empty;

            if (parcels.Count() == 0)
            {
                id = "urn:ngsi-ld:AgriParcel:0001";
                return id;
            }

            var lastItem = parcels.OrderBy(o => o.id).LastOrDefault();

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