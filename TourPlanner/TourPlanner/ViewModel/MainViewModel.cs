﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.ViewModel.Commands;
using System.IO.Compression;
using System.Windows.Interactivity;

namespace TourPlanner.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Tour _currentTour;

        private ObservableCollection<Tour> _tours;

        private ObservableCollection<TourLog> _currentTourLogs;

        public ObservableCollection<Tour> Tours { 
            get
            {
                return _tours;
            }

            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        } 
        public ObservableCollection<TourLog> TourLogs { get; } = new ObservableCollection<TourLog>();

        public ObservableCollection<TourLog> currentTourLogs
        {
            get
            {
                return _currentTourLogs;
            }

            set
            {
                _currentTourLogs = value;
                OnPropertyChanged(nameof(currentTourLogs));
            }
        }


        public Tour CurrentTour
        {
            get
            {
                return _currentTour;
            }

            set
            {
                _currentTour = value;
                OnPropertyChanged(nameof(CurrentTour));
            }
        }

        public RelayCommand AddTourCommand { get; }
        public RelayCommand DeleteTourCommand { get; }

        public RelayCommand SelectTour { get; }


        public MainViewModel()
        {
            AddTourCommand = new RelayCommand((_) =>
            {
                Tours.Add(new Tour(100, "New Tour", "Tour Description"));
                Debug.Write("Tour added");
            });

            SelectTour = new RelayCommand(x =>
            {
                selectTour(x as Tour);
            });

            DeleteTourCommand = new RelayCommand((_) =>
            {
                var t = Tours.Where(X => X.ID != CurrentTour.ID);
                Tours = new ObservableCollection<Tour>(t);
                Debug.Write("Tour deleted");
                CurrentTour = Tours.First();
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
            });

            Tours = new ObservableCollection<Tour>();
            currentTourLogs = new ObservableCollection<TourLog>();

            //add some tours
            Tours.Add(new Tour(0, "Tour 1", "Description 1"));
            Tours.Add(new Tour(1, "Tour 2", "Description 2"));

            //add some tour logs

            TourLogs.Add(new TourLog(0, 0, "1.1.2020", "Awesome", 23.02f, "1:34", 2));
            TourLogs.Add(new TourLog(0, 1, "21.1.2019", "Great", 13.42f, "1:14", 1));

            TourLogs.Add(new TourLog(1, 0, "1.2.2013", "Fine", 23.02f, "1:24", 2));
            TourLogs.Add(new TourLog(1, 1, "21.1.2016", "Amazing", 13.42f, "1:19", 2));

            CurrentTour = Tours.First();
            var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
            currentTourLogs = new ObservableCollection<TourLog>(cl);

        }

        private void selectTour(Tour tour)
        {
            CurrentTour = Tours.Where(X => X.ID == tour.ID).FirstOrDefault();
            var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
            currentTourLogs = new ObservableCollection<TourLog>(cl);
            Debug.Print($"Selected Tour {tour.ID}");
            Debug.Print("ID: " + CurrentTour.ID);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
