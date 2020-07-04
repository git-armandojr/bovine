using Bovine.Models;
using Bovine.Models.AgriParcel;
using Bovine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bovine.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PastureDetailPage : ContentPage
    {
        string id;
        readonly AgriParcelManager manager = new AgriParcelManager();
        public static bool buttonDeleteVisibility;

        public PastureDetailPage()
        {
            InitializeComponent();
            labelID.IsVisible = false;
            entryID.IsVisible = false;
            buttonDelete.IsVisible = buttonDeleteVisibility;

            MessagingCenter.Subscribe<string>(this, "AddItem", message => id = message);
        }

        #region Singleton
        //turn this page in a singleton class instance
        public static PastureDetailPage instance = null;
        private static readonly object _lock = new object();
        public static PastureDetailPage GetInstance()
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new PastureDetailPage();
                }

                return instance;
            }
        }
        #endregion

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.ff'Z'");
            
            float area;
            float.TryParse(entryArea.Text, out area);

            if (string.IsNullOrWhiteSpace(entryID.Text))
            {
                await manager.Add(new AgriParcel
                {
                    id = id,
                    type = "AgriParcel",
                    dateCreated = new DateCreated { type = "DateTime", value = dateTime },
                    dateModified = new DateModified { type = "DateTime", value = dateTime },
                    description = new Description { value = entryDescription.Text },
                    area = new Area { value = area }
                });
            }
            else
            {
                await manager.Update(new AgriParcel
                {
                    id = entryID.Text,
                    area = new Area { value = area },
                    description = new Description { value = entryDescription.Text }
                });
            }

            await Navigation.PopModalAsync();
            //Pasture pasture = App.SelectedPasture;

            //float area;

            //float.TryParse(entryArea.Text, out area);
            //if (!string.IsNullOrWhiteSpace(entryID.Text))
            //{
            //    await App.PastureDatabase.SavePastureAsync(new Pasture
            //    {
            //        ID = int.Parse(entryID.Text),
            //        Area = area,
            //        Description = entryDescription.Text,
            //        FarmID = App.SelectedFarm.ID
            //    });
            //}
            //else
            //{
            //    await App.PastureDatabase.SavePastureAsync(new Pasture
            //    {
            //        Area = area,
            //        Description = entryDescription.Text,
            //        FarmID = App.SelectedFarm.ID
            //    });
            //}
            //await Navigation.PopModalAsync();
            instance = null;
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            AgriParcel agriParcel = (AgriParcel)BindingContext;
            await manager.Delete(agriParcel.id);
            //Pasture pasture = (Pasture)BindingContext;
            //await App.PastureDatabase.DeletePastureAsync(pasture);
            await Navigation.PopModalAsync();
            instance = null;
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
            instance = null;
        }

        protected override bool OnBackButtonPressed()
        {
            instance = null;
            return base.OnBackButtonPressed();
        }
    }
}