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

        //Properties

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


        public MainViewModel()
        {
            isEditableTour = false;



            //Commands

            //Create new Tour
            AddTourCommand = new RelayCommand((_) =>
            {
                Tours.Add(new Tour(Tours.Count, "New Tour", "Tour Description", "/Views/Images/thumbnail.jpg"));
                Debug.Write("Tour added");
                log.Error("Tour added!");

                log4net.Config.XmlConfigurator.Configure();
                log.Info("Application is working");
            });

            //Create new Tour Log
            AddTourLogCommand = new RelayCommand((_) =>
            {
                TourLogs.Add(new TourLog(CurrentTour.ID, currentTourLogs.Count, "4.5.2017", "Super", 12.65f, "6:34", 2));
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
                if(Tours.Where(X => X.Title.Contains(SearchInput)).FirstOrDefault() != null)
                {
                    Debug.Print(SearchInput);
                    CurrentTour = Tours.Where(X => X.Title.Contains(SearchInput)).FirstOrDefault();
                    var cl = TourLogs.Where(X => X.TourID == CurrentTour.ID);
                    currentTourLogs = new ObservableCollection<TourLog>(cl);
                }

                else
                {
                    MessageBox.Show("Keine Ergebnisse gefunden :(");
                }


            });

            //Edit Tour

            EditTourCommand = new RelayCommand((_) =>
            {
                if(isEditableTour == true)
                {
                    isEditableTour = false;
                }

                else
                {
                    isEditableTour = true;
                }
            });

            //Edit Tour Log

            EditTourLogCommand = new RelayCommand((_) =>
            {
                MessageBox.Show("Not yet implemented");
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
            });

            //Close Application

            CloseApplication = new RelayCommand((_) =>
            {
                System.Windows.Application.Current.Shutdown();
            });

            //Copy Tour

            CopyTourCommand = new RelayCommand((_) =>
            {
                Tour t = new Tour(Tours.Count, CurrentTour.Title, CurrentTour.Description, CurrentTour.Image);
                Tours.Add(t);
            });

            //Export Tour JSON

            ExportTourCommandJSON = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "Json files (*.json)|*.json" );
            });

            //Export Tour CSV

            ExportTourCommandCSV = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "CSV file (*.csv)|*.csv");
            });

            //Export Tour TXT

            ExportTourCommandTXT = new RelayCommand((_) =>
            {
                saveTour(CurrentTour, "txt files (*.txt)|*.txt");
            });

            //Export Tour Logs JSON

            ExportTourLogsCommandJSON = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "Json files (*.json)|*.json");
            });

            //Export Tour Logs CSV

            ExportTourLogsCommandCSV = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "CSV file (*.csv)|*.csv");
            });

            //Export Tour Logs TXT

            ExportTourLogsCommandTXT = new RelayCommand((_) =>
            {
                saveTourLogs(currentTourLogs.ToList(), "txt files (*.txt)|*.txt");
            });

            //Import Tour from JSON

            ImportTourCommandJSON = new RelayCommand((_) =>
            {
                importTour("Json files(*.json) | *.json");

            });

            //Import Tour from CSV

            ImportTourCommandCSV = new RelayCommand((_) =>
            {
                importTour("CSV file (*.csv)|*.csv");
            });

            //Import TourLogs from JSON

            ImportTourLogsCommandJSON = new RelayCommand((_) =>
            {
                importTourLogs("Json files(*.json) | *.json");


            });

            //Import TourLogs from CSV

            ImportTourLogsCommandCSV = new RelayCommand((_) =>
            {
                importTourLogs("CSV file (*.csv)|*.csv");
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
            });







            //Create ObservableCollectiions
            Tours = new ObservableCollection<Tour>();
            currentTourLogs = new ObservableCollection<TourLog>();
            TourLogs = new ObservableCollection<TourLog>();

            //add some tours
            Tours.Add(new Tour(0, "Tour 1", "Description 1", "/Views/Images/thumbnail.jpg"));
            Tours.Add(new Tour(1, "Tour 2", "Description 2", "/Views/Images/thumbnail2.jpg"));

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

        //On Property Changed
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
