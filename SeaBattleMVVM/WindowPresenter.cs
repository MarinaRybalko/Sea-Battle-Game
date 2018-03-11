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
                            var textColumn = new DataGridTextColumn();
                            var textColumn1 = new DataGridTextColumn();
                            if (!(child is DataGrid)) continue;
                            var childGrid = child as DataGrid;
                            IEnumerable players = (IEnumerable)pDataContext;
                                (child as DataGrid).ItemsSource = players;
                            textColumn.Header =
                                ResourceManagerService.GetResourceString("MainWindowRes", "Name_Lbl");
                            textColumn.Binding= new Binding("Name");
                               
                            childGrid.Columns.Add(textColumn);
                            textColumn1.Header =
                                ResourceManagerService.GetResourceString("MainWindowRes", "Rating_Lbl");
                            textColumn1.Binding = new Binding("Rating");
                            childGrid.Columns.Add(textColumn1);
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
