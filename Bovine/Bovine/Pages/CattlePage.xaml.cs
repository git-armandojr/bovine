using Bovine.Controls;
using Bovine.Models;
using Bovine.Models.Animal;
using Bovine.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CattlePage : ContentPage
    {
        readonly IList<Animal> animals = new ObservableCollection<Animal>();
        readonly AnimalManager manager = new AnimalManager();

        //ObservableCollection<Cattle> cattles = new ObservableCollection<Cattle>();
        //public ObservableCollection<Cattle> Cattles { get { return cattles;  } }

        public CattlePage()
        {
            BindingContext = animals;
            InitializeComponent();
            listViewCattle.ItemsSource = animals;

            ToolbarItem toolbarItemEdit = new ToolbarItem
            {
                Text = "Editar",
                Order = ToolbarItemOrder.Secondary,
                Priority = 0
            };
            toolbarItemEdit.Clicked += OnItemEditClicked;

            this.ToolbarItems.Add(toolbarItemEdit);

            //cattles.Add(new Cattle(1, "Mimosa", 400));
            //cattles.Add(new Cattle(2, "Rainha", 300));
            //cattles.Add(new Cattle(3, "Abelha", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(5, "Rei", 500));
            //cattles.Add(new Cattle(6, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(4, "Bandido", 500));
            //cattles.Add(new Cattle(9, "Bandido", 500));

            //listViewCattle.ItemsSource = cattles;
        }

        async void OnItemEditClicked(object sender, EventArgs e)
        {
            if (CattleDetailPage.instance == null && listViewCattle.SelectedItem != null)
            {
                CattleDetailPage.buttonDeleteVisibility = true;
                var page = new NavigationPage(CattleDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                page.BindingContext = App.SelectedAnimal;
                await Navigation.PushModalAsync(page);
            }
        }

        async void OnButtonAddClicked(object sender, EventArgs e)
        {
            if (CattleDetailPage.instance == null)
            {
                CattleDetailPage.buttonDeleteVisibility = false;
                var page = new NavigationPage(CattleDetailPage.GetInstance());
                page.BarBackgroundColor = Color.FromHex("0078D7");
                MessagingCenter.Send(GenerateNewAnimalId(), "AddItem");
                await Navigation.PushModalAsync(page);
            }
            //if (CattleDetailPage.instance == null)
            //{
            //    CattleDetailPage.buttonDeleteVisibility = false;
            //    var page = new NavigationPage(CattleDetailPage.GetInstance());               
            //    page.BarBackgroundColor = Color.FromHex("0078D7");                
            //    await Navigation.PushModalAsync(page);
            //}            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            animals.Clear();

            try
            {
                var animalCollection = await manager.GetAll();

                if (animalCollection.Count() > 0)
                {
                    foreach (Animal animal in animalCollection)
                    {
                        //if (parcels.All(p => p.id != parcels.id))
                        animals.Add(animal);
                    }

                    if (App.SelectedAgriParcel == null)
                    {
                        listViewCattle.SelectedItem = animals.FirstOrDefault();
                        App.SelectedAnimal = animals.FirstOrDefault();
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Log.Warning("ArgumentNullException", e.Message);
            }

            //listViewCattle.ItemsSource = await App.CattleDatabase.GetCattlesAsync();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            App.SelectedAnimal = e.SelectedItem as Animal;
            this.Title = "Gado " + App.SelectedAnimal.legalID.value;
            this.Title.Remove(0);

            //if (listViewCattle.SelectedItem != null)
            //{
            //    if (CattleDetailPage.instance == null)
            //    {
            //        CattleDetailPage.buttonDeleteVisibility = true;
            //        var page = new NavigationPage(CattleDetailPage.GetInstance());
            //        page.BarBackgroundColor = Color.FromHex("0078D7");
            //        page.BindingContext = e.SelectedItem as Cattle;                    
            //        listViewCattle.SelectedItem = null;
            //        await Navigation.PushModalAsync(page);
            //    }       
            //}
        }
        
        string GenerateNewAnimalId()
        {

            string id = string.Empty;

            if (animals.Count() == 0)
            {
                id = "urn:ngsi-ld:AgriParcel:0001";
                return id;
            }

            var lastItem = animals.OrderBy(o => o.id).LastOrDefault();

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