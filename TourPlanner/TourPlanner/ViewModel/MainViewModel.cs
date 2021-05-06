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
        public ObservableCollection<TourLog> TourLogs { get; } = new ObservableCollection<TourLog>();

        public RelayCommand AddTourCommand { get; }

        public MainViewModel()
        {
            AddTourCommand = new RelayCommand((_) =>
            {
                Tours.Add(new Tour(100, "New Tour", "Tour Description"));
                Debug.Write("Tour added");
            });

            //add some tours
            Tours.Add(new Tour(0, "Tour 1", "Description 1"));
            Tours.Add(new Tour(1, "Tour 2", "Description 2"));

            //add some tour logs

            TourLogs.Add(new TourLog(0, 0, "1.1.2020", "Awesome", 23.02f, "1:34", 2));
            TourLogs.Add(new TourLog(0, 1, "21.1.2019", "Great", 13.42f, "1:14", 1));

            TourLogs.Add(new TourLog(1, 0, "1.2.2013", "Fine", 23.02f, "1:24", 2));
            TourLogs.Add(new TourLog(1, 1, "21.1.2016", "Amazing", 13.42f, "1:19", 2));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
