using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.ViewModel.Commands;

namespace TourPlanner.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Tour> Tours { get; } = new ObservableCollection<Tour>();

        public RelayCommand AddTourCommand { get; }

        public MainViewModel()
        {
            AddTourCommand = new RelayCommand((_) =>
            {
                Tours.Add(new Tour("New Tour", "Tour Description"));
                Debug.Write("Tour added");
            });

            //add some tours
            Tours.Add(new Tour("Tour 1", "Description 1"));
            Tours.Add(new Tour("Tour 2", "Description 2"));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
