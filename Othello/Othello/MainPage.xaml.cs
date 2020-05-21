using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Othello
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            //User clicked on the new game button
            CreateBoard();
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            //User clicked on a space
        }

        public bool CreateBoard()
        {
            PlayBoard.RowDefinitions.Clear();
            PlayBoard.ColumnDefinitions.Clear();
            PlayBoard.Children.Clear();

            for (int i = 0; i < 8; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(1, GridUnitType.Star);
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(1, GridUnitType.Star);

                PlayBoard.RowDefinitions.Add(rd);
                PlayBoard.ColumnDefinitions.Add(cd);
            }


            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Rectangle rect = new Rectangle();
                    rect.HorizontalAlignment = HorizontalAlignment.Stretch;
                    rect.VerticalAlignment = VerticalAlignment.Stretch;

                    rect.Stroke = new SolidColorBrush(Colors.SteelBlue);
                    rect.Fill = new SolidColorBrush(Colors.DarkGreen);
                    rect.Tapped += Space_Click;

                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);

                    PlayBoard.Children.Add(rect);
                }
            }

            return true;
        }
    }
}
