using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Model;

namespace TourPlanner.ViewModel.Commands
{
    class AddTour : ICommand
    {
        private MainViewModel _mainViewModel;

        public AddTour(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            _mainViewModel.PropertyChanged += (sender, args) =>
            {
                Debug.Print("command: reveived prop changed");
                if (args.PropertyName == "Tour")
                {
                    Debug.Print("command: reveived prop changed of Tour");
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };

        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            Debug.Print("command: can execute");
            return true;
        }

        public void Execute(object parameter)
        {
            Debug.Print("command: add Tour");
            Tour t = new Tour(99, "New Tour", "Tour Description", "/Views/thumbnail.jpg", "Vienna", "Munich");
            _mainViewModel.Tours.Add(t);
            Debug.Print(_mainViewModel.Tours.Count.ToString());
            Debug.Print("command: add Tour Done");

        }
    }
}
