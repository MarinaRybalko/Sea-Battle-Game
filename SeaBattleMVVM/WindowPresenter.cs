using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using LocalizatorHelper;
using SeaBattleMVVM.ViewModel;

namespace SeaBattleMVVM
{
    public class WindowPresenter
    {
        /// <summary>
        /// Shows childs windows
        /// </summary>
        /// <param name="pViewIndex"></param>
        /// <param name="pDataContext"></param>
        /// <param name="playerName"></param>
        public static void Show(int pViewIndex, object pDataContext,  ref string playerName)
        {
            ResourceManagerService.RegisterManager("MainWindowRes", MainWindowRes.ResourceManager, true);

            Window window;
            switch (pViewIndex)
            {
                case 0:
                {
                    window = new GeetingWindow();
                    {
                        window.DataContext=new GreetingViewModel();
                        window.ShowDialog();
                        var panel = (Panel)window.Content;
                        foreach (var child in panel.Children)
                        {
                            if (child is TextBox)
                            {
                                playerName=(child as TextBox).Text;
                            }
                        }

                    }
                    break;
                    }

                case 1:
                {
                    window = new RatingWindow();

                    {
                        var panel = (Panel)window.Content;
                        foreach (var child in panel.Children)
                        {
                            if (child is DataGrid)
                            {

                                (child as DataGrid).ItemsSource = (IEnumerable) pDataContext;
                                Binding source = new Binding("MainWindowRes.Name_Lbl");
                                //(child as DataGrid).Items[0]= new Binding("MainWindowRes.Rating_Lbl");
                                    //(child as DataGrid).Columns[1].Header = new Binding("MainWindowRes.Rating_Lbl");



                                }
                        }

                        window.ShowDialog();

                    }
                    break;
                    }
     
                default:
                    throw new ArgumentOutOfRangeException(nameof(pViewIndex), @"Index out of range");
            }
           
        }

    }
}
