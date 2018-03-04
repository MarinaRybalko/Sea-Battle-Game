using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using SeaBattleMVVM.ViewModel;

namespace SeaBattleMVVM
{
    public class WindowPresenter
    {
        public static void Show(int pViewIndex, object pDataContext,  ref string playerName)
        {
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
                                (child as DataGrid).ItemsSource = (IEnumerable)pDataContext;
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
