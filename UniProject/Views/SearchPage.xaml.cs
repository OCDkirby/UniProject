﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniProject.Models;
using UniProject.Utils;
using UniProject.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace UniProject.Views
{
    public partial class SearchPage : ContentPage
    {
        public SchoolModel Selected { get; set; }

        public SearchPage()
        {
            InitializeComponent();
            Navigation.PushModalAsync(new LoginPage());
        }

        async void SchoolClicked(object sender, EventArgs e)
        {
            //Whenever an item is selected, that item is assigned to the SchoolViewModel's Selected property
            //Get a reference to that item and pass it to the BuildingPage we push on the nav stack
            SchoolModel s = ((SchoolViewModel) ((ListView) sender).BindingContext).Selected;
            await Navigation.PushAsync(new BuildingPage(s));
        }

        private async void NavigateToSavedSearchButton(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavedSearchesPage());
        }

    }
}