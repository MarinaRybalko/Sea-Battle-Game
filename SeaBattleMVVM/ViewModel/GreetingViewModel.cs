using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using SeaBattleMVVM.Annotations;

namespace SeaBattleMVVM.ViewModel
{
    public class GreetingViewModel:INotifyPropertyChanged
    {
        private const string Pattern = @"^[a-z]|[а-я]+$";
        private string _error="";
        private string _name = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged(nameof(Error)); }
        }
        public ICommand CloseCommand { get; set; }       
        public string Name
        {
            get { return _name;}
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }


        public GreetingViewModel()
        {

            CloseCommand= new DelegateCommand(Close);
        }
        public void Close(object obj)
        {
            
            var window = obj as Window;
            if (Regex.IsMatch(Name, Pattern, RegexOptions.IgnoreCase) && !(string.IsNullOrEmpty(Name)))
            {
                if (window != null) window.Visibility = Visibility.Hidden;
            }
            else
            {               
                Error = "uncorrect name";
             
            }
        }
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
