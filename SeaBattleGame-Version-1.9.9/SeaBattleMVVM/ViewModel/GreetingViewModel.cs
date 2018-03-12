using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LocalizatorHelper;
using SeaBattleMVVM.Annotations;

namespace SeaBattleMVVM.ViewModel
{
    public class GreetingViewModel:INotifyPropertyChanged
    {
        private const string Pattern = @"^[a-z]|[а-я]+$";      
        private string _name = string.Empty;
        /// <summary>
        /// Occurs when a property is changed on a component
        /// </summary>       
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Returns or sets close form or button ok click command
        /// </summary>
        public ICommand CloseCommand { get; set; }
        /// <summary>
        /// Returns or sets command for change language to russian
        /// </summary>
        public ICommand RussianCommand { get; set; }
        /// <summary>
        /// Returns or sets command for change language to english
        /// </summary>
        public ICommand EnglishCommand { get; set; }
        /// <summary>
        /// Returns or sets player name
        /// </summary>
        public string Name
        {
            get { return _name;}
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="GreetingViewModel"/> class
        /// </summary>
        public GreetingViewModel()
        {
            CloseCommand= new DelegateCommand(Close);
            RussianCommand= new DelegateCommand(Russian);
            EnglishCommand= new DelegateCommand(English);
            ResourceManagerService.RegisterManager("MainWindowRes", MainWindowRes.ResourceManager, true);

        }
        private void Russian(object obj)
        {

            ResourceManagerService.ChangeLocale("ru-RU");

        }
        private void English(object obj)
        {

            ResourceManagerService.ChangeLocale("en-US");
        }
        private void Close(object obj)
        {

            var window = obj as Window;
            if (Regex.IsMatch(Name, Pattern, RegexOptions.IgnoreCase) && !(string.IsNullOrEmpty(Name)))
            {
                if (window != null) window.Visibility = Visibility.Hidden;
            }
            else
            {
                if (window != null)
                {
                    var panel = (Panel) window.Content;
                    foreach (var child in panel.Children)
                    {
                        var label = child as Label;
                        if (label != null)
                        {
                            label.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
