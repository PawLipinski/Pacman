using System;
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

namespace Pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Pac myPacman;

        public MainWindow()
        {
            //this.BoardSpace.Background = new SolidColorBrush(Colors.Black);
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            Board myBoard = new Board(this);

            myBoard.printBoard();

            myPacman = new Pac(myBoard);
            myPacman.PrintTheHero();
            myBoard.Pacman = myPacman;

            myBoard.StartTimer();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            myPacman.DemandChangeDirection(e.Key);
        }

    }
}
