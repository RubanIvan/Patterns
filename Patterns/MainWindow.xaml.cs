﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Patterns
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlayerShip PlayerShip;

        public MainWindow()
        {
            InitializeComponent();
            PlayerShip=new PlayerShip();
            MainGrid.Children.Add(PlayerShip.Grid);
        }

        private void MainGrid_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    PlayerShip.MoveLeft();
                    break;

                case Key.Right:
                    PlayerShip.MoveRight();
                    break;

                case Key.Up:
                    PlayerShip.MoveUp();
                    break;

                case Key.Down:
                    PlayerShip.MoveDown();
                    break;

            }
        }
    }
}