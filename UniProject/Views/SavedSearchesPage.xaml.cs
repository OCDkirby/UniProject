using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniProject.Models;
using UniProject.Utils;
using UniProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace UniProject.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavedSearchesPage : ContentPage
    {
        public SchoolModel Selected { get; set; }

        public SavedSearchesPage()
        {
            InitializeComponent();
        }

        async void SchoolClicked(object sender, SelectedItemChangedEventArgs e)
        {
            SchoolModel s = (((ListView) sender).BindingContext as SavedSearchesViewModel).Selected;
            await Navigation.PushAsync(new BuildingPage(s), true);
        }

        //Deletes the favorited university (Button is on the saved searches page)
        async void SchoolDeleteButton(object sender, EventArgs e)
        {
            SchoolModel s = ((SchoolModel) ((ImageButton) sender).BindingContext);
            String schoolname = s.SchoolName;
            //Remove the line below this at a later date.

            var schoolexists = DbConn.QueryScalar("Select * from savedsearches Where UserId = @1 AND SavedSchool = @2",
                Utilities.UserID, schoolname);
            if (schoolexists != null)
            {
                await DisplayAlert("Success!", "The University Was Removed From Your Saved Searches.", "Close");
                DbConn.Query("DELETE FROM savedsearches WHERE UserId = @1 AND SavedSchool = @2", Utilities.UserID,
                    schoolname);
            }
            else
            {
                await DisplayAlert("Error!", "Saved Item Not Found.", "Close");
            }
        }
    }
}