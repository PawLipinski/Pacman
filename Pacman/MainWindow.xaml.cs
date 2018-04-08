using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Pacman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Board gameBoard;

        public MainWindow()
        {
            //this.BoardSpace.Background = new SolidColorBrush(Colors.Black);
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

            gameBoard = new Board(this);
            gameBoard.printBoard();
            gameBoard.StartTimer();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            this.gameBoard.Pacman.DemandChangeDirection(e.Key);
        }


    }
}
