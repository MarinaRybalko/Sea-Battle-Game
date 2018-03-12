using System.Windows;
using SeaBattleMVVM.ViewModel;

namespace SeaBattleMVVM
{
    /// <summary>
    ///Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _model= new MainWindowViewModel();
        public MainWindow()
        {
           
            InitializeComponent();
            DataContext = _model;
          

        }
       

    }
}
