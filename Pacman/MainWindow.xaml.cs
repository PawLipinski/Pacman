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

            //Ghost myGhost = new Ghost(myBoard, 9,10, Color.FromRgb(255,0,0));
            //myGhost.PrintTheHero();

            myBoard.StartTimer();
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            myPacman.DemandChangeDirection(e.Key);
        }

    }
}
