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
using Microsoft.Win32;
using System.IO;

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

        private bool _isEditableTour;

        private WeatherData _wdFrom;

        private WeatherData _wdTo;


        //Properties

        public WeatherData WDFrom { 
            get 
            {
                return _wdFrom;
            } 

            set
            {
                _wdFrom = value;
                OnPropertyChanged(nameof(WDFrom));
            }
        
        }

        public WeatherData WDTo
        {
            get
            {
                return _wdTo;
            }

            set
            {
                _wdTo = value;
                OnPropertyChanged(nameof(WDTo));
            }

        }

        public bool isEditableTour
        {
            get
            {
                return _isEditableTour;
            }

            set
            {
                _isEditableTour = value;
                OnPropertyChanged(nameof(isEditableTour));
            }
        }
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

        //Log4net

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //Commands
        public RelayCommand AddTourCommand { get; }
        public RelayCommand AddTourLogCommand {get;}
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand SelectTour { get; }
        public RelayCommand SelectTourLog { get; }
        public RelayCommand SearchTourCommand { get; }
        public RelayCommand EditTourCommand { get; }
        public RelayCommand EditTourLogCommand { get; }
        public RelayCommand OpenGitRepo { get; }
        public RelayCommand CloseApplication { get; }
        public RelayCommand CopyTourCommand { get; }
        public RelayCommand ExportTourCommandJSON { get; }
        public RelayCommand ExportTourCommandCSV { get; }
        public RelayCommand ExportTourCommandTXT { get; }
        public RelayCommand ExportTourLogsCommandJSON { get; }
        public RelayCommand ExportTourLogsCommandCSV { get; }
        public RelayCommand ExportTourLogsCommandTXT { get; }
        public RelayCommand ImportTourCommandJSON { get; }
        public RelayCommand ImportTourCommandCSV { get; }
        public RelayCommand ImportTourLogsCommandJSON { get; }
        public RelayCommand ImportTourLogsCommandCSV { get; }
        public RelayCommand PdfExportCommand { get; }
        public RelayCommand PdfExportCommand2 { get; }


        Tour_Export tour_export = new Tour_Export();
        TourLog_Export tour_log_export = new TourLog_Export();

        Tour_Import tour_import = new Tour_Import();
        TourLog_Import tour_log_import = new TourLog_Import();

        PDF_Export pdf_export = new PDF_Export();

        MapQuest mapQuest = new MapQuest();
        OpenWeather openWeather = new OpenWeather();

        Data data = new Data();


        public MainViewModel()
        {
            isEditableTour = false;
            log4net.Config.XmlConfigurator.Configure();
            log.Info("Application started");

            //Commands

            //Create new Tour
            AddTourCommand = new RelayCommand((_) =>
            {
                Tour t = new Tour(Tours.Count, "New Tour", "Tour Description", mapQuest.getRouteImage("Vienna", "Munich"), "Vienna", "Munich");
                Tours.Add(t);
                log.Info("Tour added");

                //Database

                data.addTour(t);
            });

            //Create new Tour Log
            AddTourLogCommand = new RelayCommand((_) =>
            {
                TourLog tl = new TourLog(CurrentTour.ID, currentTourLogs.Count, "4.5.2017", "Super", 12.65f, "6:34", 2);
                TourLogs.Add(tl);
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                log.Info("Tour Log added");

                //DB
                data.addTourLog(tl);

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

                //DB
                data.deleteTour(CurrentTour.ID);


                CurrentTour = Tours.First();
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                log.Info("Tour deleted");


            });

            //Delete Tour Log
            DeleteTourLogCommand = new RelayCommand((_) =>
            {
                //DB
                data.deleteTourLog(CurrentTourLog);

                TourLogs.Remove(TourLogs.Where(X => X.TourID == CurrentTour.ID && X.TourLogID == CurrentTourLog.TourLogID).Single());
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                log.Info("Tour Log deleted");
            });

            //Search Tour
            SearchTourCommand = new RelayCommand((_) =>
            {
                if(Tours.Where(X => X.Title.Contains(SearchInput)).FirstOrDefault() != null)
                {
                    Debug.Print(SearchInput);
                    CurrentTour = Tours.Where(X => X.Title.Contains(SearchInput)).FirstOrDefault();
                    var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                    currentTourLogs = new ObservableCollection<TourLog>(cl);
                    log.Info("Search function: Tour found");
                }

                else
                {
                    MessageBox.Show("Keine Ergebnisse gefunden :(");
                    log.Error("Search function: No Tour found");
                }


            });

            //Edit Tour

            EditTourCommand = new RelayCommand((_) =>
            {
                if(isEditableTour == true)
                {
                    isEditableTour = false;
                    updateTour();
                }

                else
                {
                    isEditableTour = true;
                }
            });

            //Edit Tour Log

            EditTourLogCommand = new RelayCommand((_) =>
            {
                data.editTourLog(CurrentTourLog);
                Debug.WriteLine("EDIT TOUR LOG"+ CurrentTourLog.Report);
            });

            //Open Github Repository

            OpenGitRepo = new RelayCommand((_) =>
            {
                var ps = new ProcessStartInfo("https://github.com/lukas912/TourPlanner2")
                {
                    UseShellExecute = true,
                    Verb = "open"
                };
                Process.Start(ps);
                log.Info("Opened GitHub");
            });

            //Close Application

            CloseApplication = new RelayCommand((_) =>
            {
                System.Windows.Application.Current.Shutdown();
            });

            //Copy Tour

            CopyTourCommand = new RelayCommand((_) =>
            {
                Tour t = new Tour(Tours.Count, CurrentTour.Title, CurrentTour.Description, CurrentTour.Image, CurrentTour.From, CurrentTour.To);
                Tours.Add(t);
                log.Info("Tour copied");
            });

            //Export Tour JSON

            ExportTourCommandJSON = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "Json files (*.json)|*.json" );
                log.Info("Tour exported to JSON");
            });

            //Export Tour CSV

            ExportTourCommandCSV = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "CSV file (*.csv)|*.csv");
                log.Info("Tour exported to CSV");
            });

            //Export Tour TXT

            ExportTourCommandTXT = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "txt files (*.txt)|*.txt");
                log.Info("Tour exported to TXT");
            });

            //Export Tour Logs JSON

            ExportTourLogsCommandJSON = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "Json files (*.json)|*.json");
                log.Info("Tour Logs exported to JSON");
            });

            //Export Tour Logs CSV

            ExportTourLogsCommandCSV = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "CSV file (*.csv)|*.csv");
                log.Info("Tour Logs exported to CSV");
            });

            //Export Tour Logs TXT

            ExportTourLogsCommandTXT = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "txt files (*.txt)|*.txt");
                log.Info("Tour Logs exported to TXT");
            });

            //Import Tour from JSON

            ImportTourCommandJSON = new RelayCommand((_) =>
            {
                importTour("Json files(*.json) | *.json");
                log.Info("Tour imported from JSON");

            });

            //Import Tour from CSV

            ImportTourCommandCSV = new RelayCommand((_) =>
            {
                importTour("CSV file (*.csv)|*.csv");
                log.Info("Tour imported from CSV");
            });

            //Import TourLogs from JSON

            ImportTourLogsCommandJSON = new RelayCommand((_) =>
            {
                importTourLogs("Json files(*.json) | *.json");
                log.Info("Tour Logs imported from JSON");


            });

            //Import TourLogs from CSV

            ImportTourLogsCommandCSV = new RelayCommand((_) =>
            {
                importTourLogs("CSV file (*.csv)|*.csv");
                log.Info("Tour Logs imported from CSV");
            });

            //Export Tour Report to Pdf

            PdfExportCommand = new RelayCommand((_) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";

                if (saveFileDialog.ShowDialog() == true)
                {
                    pdf_export.exportTourReport(CurrentTour, currentTourLogs.ToList(), saveFileDialog.FileName);
                }

                log.Info("Tour Report exported to PDF");
            });

            //Export Tour Summarize Report to Pdf

            PdfExportCommand2 = new RelayCommand((_) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";

                if (saveFileDialog.ShowDialog() == true)
                {
                    pdf_export.exportTourSummarizeReport(CurrentTour, currentTourLogs.ToList(), saveFileDialog.FileName);
                }

                log.Info("Tour Summarize Report exported to PDF");
            });







            //Create ObservableCollectiions
            Tours = new ObservableCollection<Tour>();
            currentTourLogs = new ObservableCollection<TourLog>();
            TourLogs = new ObservableCollection<TourLog>();


            //add some tours
            //Tours.Add(new Tour(0, "Tour 1", "Description 1", mapQuest.getRouteImage("Berlin", "Munich"), "Berlin", "Munich"));
            //Tours.Add(new Tour(1, "Tour 2", "Description 2", mapQuest.getRouteImage("Vienna", "Munich"), "Vienna", "Munich"));

            //add some tour logs
            //TourLogs.Add(new TourLog(0, 0, "1.1.2020", "Awesome", 23.02f, "1:34", 2));
            //TourLogs.Add(new TourLog(0, 1, "21.1.2019", "Great", 13.42f, "1:14", 1));

            //TourLogs.Add(new TourLog(1, 0, "1.2.2013", "Fine", 23.02f, "1:24", 2));
            //TourLogs.Add(new TourLog(1, 1, "21.1.2016", "Amazing", 13.42f, "1:19", 2));

            initApplication();

        }

        private int genTourID()
        {
            int counter = 0;

            if(!IDexists(counter))
            {
                return counter;
            }

            else
            {
                for(int i = counter; i <= Tours.Count; i++)
                {
                    if(!IDexists(i))
                    {
                        return i;
                    }
                }

                return Tours.Count;
            }
        }

        private bool IDexists(int id)
        {
            foreach(Tour t in Tours)
            {
                if(id == t.ID)
                {
                    return true;
                }

                else if (id != t.ID)
                {
                    return false;
                }
            }

            return false;
        }

        private void initApplication()
        {

            loadTourData();

            //assign current tour
            CurrentTour = Tours.First();

            //assign tour logs
            var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
            currentTourLogs = new ObservableCollection<TourLog>(cl);

            //load weather data

            WDFrom = getWeatherData(CurrentTour.From);
            WDTo = getWeatherData(CurrentTour.To);
        }

        private void loadTourData()
        {
            //Load Tours

            this.Tours = new ObservableCollection<Tour>(data.getTours());

            //Load Tour Logs

            this.TourLogs = new ObservableCollection<TourLog>(data.getTourLogs());
        }

        private void updateTour()
        {
            //update route image
            this.CurrentTour.Image = mapQuest.getRouteImage(CurrentTour.From, CurrentTour.To);

            CurrentTour = Tours.Where(X => X.ID == CurrentTour.ID).FirstOrDefault();

            //update database
            data.editTour(CurrentTour);
        }

        //Methods
        private void selectTour(Tour tour)
        {
            if(tour != null)
            {
                CurrentTour = Tours.Where(X => X.ID == tour.ID).FirstOrDefault();
                var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                currentTourLogs = new ObservableCollection<TourLog>(cl);
                Debug.Print($"Selected Tour {tour.ID}");
                Debug.Print("ID: " + CurrentTour.ID);

                //get Weather Data
                WDFrom = getWeatherData(CurrentTour.From);
                Debug.Write(WDFrom.Description);
                WDTo = getWeatherData(CurrentTour.To);
            }

            else
            {
                Debug.Write("Keine Tour ausgewählt");
            }

        }

        private void selectTourLog(TourLog tour_log)
        {
            if(tour_log != null)
            {
                CurrentTourLog = TourLogs.Where(X => X.TourLogID == tour_log.TourLogID).FirstOrDefault();
                Debug.Print($"Selected Tour Log {tour_log.TourLogID}");
                Debug.Print("ID: " + CurrentTourLog.TourLogID);
            }

            else
            {
                Debug.Write("Keinen Tour Log ausgwählt");
            }

        }

        //Save File (Tour)

        private void saveTour(Tour tour, string type)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = type;

            if (saveFileDialog.ShowDialog() == true)
            {
                switch (type)
                {
                    case "Json files (*.json)|*.json":
                        tour_export.JSON_Export(CurrentTour, saveFileDialog.FileName);
                        break;
                    case "CSV file (*.csv)|*.csv":
                        tour_export.CSV_Export(CurrentTour, saveFileDialog.FileName);
                        break;
                    case "txt files (*.txt)|*.txt":
                        tour_export.TXT_Export(CurrentTour, saveFileDialog.FileName);
                        break;
                }
                
            }
        }


        //Save File (TourLog)

        private void saveTourLogs(List<TourLog> tour_logs, string type)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = type;

            if (saveFileDialog.ShowDialog() == true)
            {
                switch (type)
                {
                    case "Json files (*.json)|*.json":
                        tour_log_export.JSON_Export(tour_logs.ToList(), saveFileDialog.FileName);
                        break;
                    case "CSV file (*.csv)|*.csv":
                        tour_log_export.CSV_Export(tour_logs.ToList(), saveFileDialog.FileName);
                        break;
                    case "txt files (*.txt)|*.txt":
                        tour_log_export.TXT_Export(tour_logs.ToList(), saveFileDialog.FileName);
                        break;
                }

            }
        }

        //Import Tour

        private void importTour(string type)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = type;
            ofd.ShowDialog();
            if(ofd.CheckPathExists == true)
            {
                if(type == "Json files(*.json) | *.json")
                {
                    Tour t = tour_import.JSON_Import(ofd.FileName);
                    Tours.Add(t);
                }

                if(type == "CSV file (*.csv)|*.csv")
                {
                    Tour t = tour_import.CSV_Import(ofd.FileName);
                    Tours.Add(t);
                }

            }
        }

        //Import TourLogs

        private void importTourLogs(string type)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = type;
            ofd.ShowDialog();
            if (ofd.CheckPathExists == true)
            {
                if (type == "Json files(*.json) | *.json")
                {
                    List<TourLog> t = tour_log_import.JSON_Import(ofd.FileName);
                    foreach (var item in t)
                        TourLogs.Add(item);
                }

                if (type == "CSV file (*.csv)|*.csv")
                {
                    List<TourLog> t = tour_log_import.CSV_Import(ofd.FileName);
                    foreach (var item in t)
                        TourLogs.Add(item);
                }

            }
        }

        //Get Weather Data

        private WeatherData getWeatherData(string city)
        {
            return openWeather.getWeatherData(city);
        }

        //On Property Changed
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
