using Othello.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Board stonesBoard;
        Stone[] validSpaces;
        bool playerTurn = true;//white = true; black = false
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            //User clicked on the new game button
            stonesBoard = new Board();
            stonesBoard.Spaces[3, 3].IsActive = true;

            stonesBoard.Spaces[4, 3].IsActive = true;
            stonesBoard.Spaces[4, 3].Flip();

            stonesBoard.Spaces[3, 4].IsActive = true;
            stonesBoard.Spaces[3, 4].Flip();

            stonesBoard.Spaces[4, 4].IsActive = true;
            validSpaces = stonesBoard.ValidSpaces(true);

            CreateBoard();
            UpdateBoard();
        }

        private void Space_Click(object sender, RoutedEventArgs e)
        {
            //User clicked on a space
            Rectangle r = e.OriginalSource as Rectangle;
            int column = Grid.GetColumn(r);
            int row = Grid.GetRow(r);
            Debug.WriteLine(stonesBoard.CheckStoneIsValid(stonesBoard.Spaces[column, row], column, row, playerTurn));
            playerTurn = !playerTurn;
        }

        public void UpdateBoard()
        {
            for (int i = 0; i < stonesBoard.Spaces.GetLength(0); i++)
            {
                for (int j = 0; j < stonesBoard.Spaces.GetLength(1); j++)
                {
                    if (stonesBoard.Spaces[i, j].IsActive)
                    {
                        bool color = stonesBoard.Spaces[i, j].Color;

                        if (color == true)
                            GetSpace(PlayBoard, i, j).Fill = new SolidColorBrush(Colors.White);
                        if (color == false)
                            GetSpace(PlayBoard, i, j).Fill = new SolidColorBrush(Colors.Black);

                    }
                }
            }
        }

       

        Rectangle GetSpace(Grid g, int c, int r) //Helper method to access a grid's children using columns and rows
        {
            for (int i = 0; i < g.Children.Count; i++)
            {
                Rectangle b = (Rectangle)g.Children[i];
                if (Grid.GetRow(b) == r && Grid.GetColumn(b) == c)
                {
                    return b;
                }
            }
            return null;
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
