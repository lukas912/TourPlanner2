using System;
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
        //Variables
        public event PropertyChangedEventHandler PropertyChanged;

        private Tour _currentTour;

        private TourLog _currentTourLog;

        private ObservableCollection<Tour> _tours;

        private ObservableCollection<TourLog> _tourLogs;

        private ObservableCollection<TourLog> _currentTourLogs;

        private string _search_input;

        //Properties
        public string SearchInput
        {
            get
            {
                return _search_input;
            }

            set
            {
                _search_input = value;
                OnPropertyChanged(nameof(Tours));
            }
        }
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

        public ObservableCollection<TourLog> TourLogs
        {
            get
            {
                return _tourLogs;
            }

            set
            {
                _tourLogs = value;
                OnPropertyChanged(nameof(TourLogs));
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

        public TourLog CurrentTourLog
        {
            get
            {
                return _currentTourLog;
            }

            set
            {
                _currentTourLog = value;
                OnPropertyChanged(nameof(CurrentTourLog));
            }
        }

        //Commands
        public RelayCommand AddTourCommand { get; }
        public RelayCommand AddTourLogCommand {get;}
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand SelectTour { get; }
        public RelayCommand SelectTourLog { get; }
        public RelayCommand SearchTourCommand { get; }


        public MainViewModel()
        {
            //Commands

            //Create new Tour
            AddTourCommand = new RelayCommand((_) =>
            {
                Tours.Add(new Tour(100, "New Tour", "Tour Description"));
                Debug.Write("Tour added");
            });

            //Create new Tour Log
            AddTourLogCommand = new RelayCommand((_) =>
            {
                TourLogs.Add(new TourLog(CurrentTour.ID, 10, "4.5.2017", "Super", 12.65f, "6:34", 2));
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                Debug.Write("TourLog added");

            });

            //Select current Tour
            SelectTour = new RelayCommand(x =>
            {
                selectTour(x as Tour);
            });

            //Select current Tour Log
            SelectTourLog = new RelayCommand(x =>
            {
                selectTourLog(x as TourLog);
            });

            //Delete Tour
            DeleteTourCommand = new RelayCommand((_) =>
            {
                var t = Tours.Where(X => X.ID != CurrentTour.ID);
                Tours = new ObservableCollection<Tour>(t);
                Debug.Write("Tour deleted");
                CurrentTour = Tours.First();
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
            });

            //Delete Tour Log
            DeleteTourLogCommand = new RelayCommand((_) =>
            {
                TourLogs.Remove(TourLogs.Where(X => X.TourID == CurrentTour.ID && X.TourLogID == CurrentTourLog.TourLogID).Single());
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                Debug.Print("Tour Log deleted");
            });

            //Search Tour
            SearchTourCommand = new RelayCommand((_) =>
            {
                Debug.Print(SearchInput);
                CurrentTour = Tours.Where(X => X.Title.Contains(SearchInput)).FirstOrDefault();
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);

            });

            //Create ObservableCollectiions
            Tours = new ObservableCollection<Tour>();
            currentTourLogs = new ObservableCollection<TourLog>();
            TourLogs = new ObservableCollection<TourLog>();

            //add some tours
            Tours.Add(new Tour(0, "Tour 1", "Description 1"));
            Tours.Add(new Tour(1, "Tour 2", "Description 2"));

            //add some tour logs
            TourLogs.Add(new TourLog(0, 0, "1.1.2020", "Awesome", 23.02f, "1:34", 2));
            TourLogs.Add(new TourLog(0, 1, "21.1.2019", "Great", 13.42f, "1:14", 1));

            TourLogs.Add(new TourLog(1, 0, "1.2.2013", "Fine", 23.02f, "1:24", 2));
            TourLogs.Add(new TourLog(1, 1, "21.1.2016", "Amazing", 13.42f, "1:19", 2));

            //assign current tour
            CurrentTour = Tours.First();

            //assign tour logs
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

        private void selectTourLog(TourLog tour_log)
        {
            CurrentTourLog = TourLogs.Where(X => X.TourLogID == tour_log.TourLogID).FirstOrDefault();
            Debug.Print($"Selected Tour Log {tour_log.TourLogID}");
            Debug.Print("ID: " + CurrentTourLog.TourLogID);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
