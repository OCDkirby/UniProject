﻿// This ViewModel provides a View with access to lists of the School model which it can use in ListViews etc.

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using UniProject.Models;
using UniProject.Utils;
using Xamarin.Forms;
using UniProject.Annotations;
using Newtonsoft.Json;

namespace UniProject.ViewModels
{
    public class SchoolViewModel : INotifyPropertyChanged
    {
        public SchoolModel Selected { get; set; }
        private const string TextSearchQuery = "select * from School where SchoolName like @1";
        
        private ObservableCollection<SchoolModel> _schools;
        public ObservableCollection<SchoolModel> Schools
        { 
            get => _schools;
            set
            {
                _schools = value;
                OnPropertyChanged(nameof(Schools));
            } 
        }

        public SchoolViewModel()
        {
            Schools = new ObservableCollection<SchoolModel>();
        }

        public ICommand Search => new Command<string>((query) =>
        {
            //query to return schools based on a string in the search 
            //query API to return schools based on a string in the search
            try
            {
                Schools = (ObservableCollection<SchoolModel>)JsonConvert.DeserializeObject(APIConn.Request("api/School?name=" + query), typeof(ObservableCollection<SchoolModel>));
            }
            catch (Exception) { throw; }
            // Just to make the last one clear, it converts to integer (except if result is NULL in table then the value is null),
            // then if null pass -1 instead of the int value
        });
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}