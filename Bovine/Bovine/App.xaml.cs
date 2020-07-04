using Bovine.Data;
using Bovine.Models;
using Bovine.Models.AgriFarm;
using Bovine.Models.AgriParcel;
using Bovine.Models.Animal;
using Bovine.Pages;
using Bovine.Services;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Bovine
{
    public partial class App : Application
    {        
        static CattleDatabase cattleDatabase;
        static FarmDatabase farmDatabase;
        static PastureDatabase pastureDatabase;

        private static Farm selectedFarm;
        private static Pasture selectedPasture;
        private static Cattle selectedCattle;
        private static List<Position> positions = new List<Position>();

        private static AgriFarm selectedAgriFarm;
        private static AgriParcel selectedAgriParcel;
        private static Animal selectedAnimal;

        public static FarmDatabase FarmDatabase
        {
            get
            {
                if (farmDatabase == null)
                {
                    farmDatabase = new FarmDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Farms.db3"));
                }
                return farmDatabase;
            }
        }

        public static PastureDatabase PastureDatabase
        {
            get
            {
                if (pastureDatabase == null)
                {
                    pastureDatabase = new PastureDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Pastures.db3"));
                }
                return pastureDatabase;
            }
        }

        public static CattleDatabase CattleDatabase
        {
            get
            {
                if (cattleDatabase == null)
                {
                    cattleDatabase = new CattleDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Cattles.db3"));
                }
                return cattleDatabase;
            }
        }

        public static Farm SelectedFarm { get => selectedFarm; set => selectedFarm = value; }
        public static Pasture SelectedPasture { get => selectedPasture; set => selectedPasture = value; }
        public static Cattle SelectedCattle { get => selectedCattle; set => selectedCattle = value; }
        public static List<Position> Positions { get => positions; set => positions = value; }
        public static AgriFarm SelectedAgriFarm { get => selectedAgriFarm; set => selectedAgriFarm = value; }
        public static AgriParcel SelectedAgriParcel { get => selectedAgriParcel; set => selectedAgriParcel = value; }
        public static Animal SelectedAnimal { get => selectedAnimal; set => selectedAnimal = value; }

        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new MainPage());
            MainPage = new MainPage();
            //var page = new NavigationPage(new FarmPage());
            //MainPage = page;
            //(Application.Current).MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
