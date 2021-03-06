﻿using Othello.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
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
        public Board stonesBoard;
        Stone[] validSpaces;
        bool playerTurn = true;//white = true; black = false
        MediaElement placeSound = null;
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private async void NewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            //initializeSound
            placeSound = new MediaElement();
            placeSound.AutoPlay = false;
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound");
            var file = await folder.GetFileAsync("PlaceStone.mp3");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            placeSound.SetSource(stream, "");

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
            if(stonesBoard.CheckStoneIsValid(stonesBoard.Spaces[column, row], playerTurn, true))
            {
                playerTurn = !playerTurn;
                placeSound.Play();
                UpdateBoard();
            }
            validSpaces = stonesBoard.ValidSpaces(playerTurn);
            if (validSpaces.Length <= 0)
            {
                Frame.Navigate(typeof(GameOver), stonesBoard);
            }
            
        }

        public void UpdateBoard()
        {
            //Update turn display
            if (playerTurn == true) //white
            {
                TurnText.Text = "TURN: WHITE";
            }
            else
            {
                TurnText.Text = "TURN: BLACK";
            }

            for (int i = 0; i < stonesBoard.Spaces.GetLength(0); i++)
            {
                for (int j = 0; j < stonesBoard.Spaces.GetLength(1); j++)
                {
                    if (stonesBoard.Spaces[i, j].IsActive)
                    {
                        bool color = stonesBoard.Spaces[i, j].Color;

                        if (color == true)
                        {
                            GetSpace(PlayBoard, i, j).Fill = new ImageBrush()
                            {
                                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/circle.png"))
                            };
                        }

                        if (color == false)
                        {
                            GetSpace(PlayBoard, i, j).Fill = new ImageBrush()
                            {
                                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/circleBlack.png"))
                            };
                        }
                    }
                    else
                    {
                        if(stonesBoard.CheckStoneIsValid(stonesBoard.Spaces[i, j], playerTurn))
                        {
                            GetSpace(PlayBoard, i, j).Fill = new SolidColorBrush(Colors.Orange);

                        }
                        else
                        {
                            GetSpace(PlayBoard, i, j).Fill = new SolidColorBrush(Colors.DarkGreen);

                        }
                    }
                }
            }

            //Update pieces count
            WhitePieces.Text = "" + stonesBoard.GetWhitePieces();
            BlackPieces.Text = "" + stonesBoard.GetBlackPieces();
        }

       

        public Rectangle GetSpace(Grid g, int c, int r) //Helper method to access a grid's children using columns and rows
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

            PlayBoard.Background = new SolidColorBrush(Colors.DarkGreen);

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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
