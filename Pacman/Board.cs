using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Pacman
{
    class Board
    {

        private List<Wall> walls;
        private MainWindow gameWindow;
        private Pac pacman;
        private DispatcherTimer boardTimer;
        private Food boardFood;

        private int[,] wallsDefinition = new int[22, 19]
        {
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,0,1},
            {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
            {1,1,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,1,1},
            {0,0,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0},
            {1,1,1,1,0,1,0,1,1,0,1,1,0,1,0,1,1,1,1},
            {0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0},
            {1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1},
            {0,0,0,1,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0},
            {1,1,1,1,0,1,0,1,1,1,1,1,0,1,0,1,1,1,1},
            {1,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,0,1},
            {1,0,1,1,0,1,1,1,0,1,0,1,1,1,0,1,1,0,1},
            {1,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1,0,0,1},
            {1,1,0,1,0,1,0,1,1,1,1,1,0,1,0,1,0,1,1},
            {1,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,0,1},
            {1,0,1,1,1,1,1,1,0,1,0,1,1,1,1,1,1,0,1},
            {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
        };


        public Board(MainWindow window)
        {
            this.gameWindow = window;
            this.walls = new List<Wall>();
            this.boardFood = new Food(this);
            boardFood.GenerateFood();
            boardFood.PrintTheDots();
            InitializeWalls();
            boardTimer = new DispatcherTimer();
            boardTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            boardTimer.Interval = new TimeSpan(0, 0, 0, 0, 35);

        }

        private void InitializeWalls()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (wallsDefinition[i,j]==1)
                    {
                        walls.Add(new Wall(j, i));
                    }
                }
                
            }
        }

        public void printBoard()
        {
            foreach (var item in walls)
            {
                Canvas.SetLeft(item.FieldShape, item.XCoord);
                Canvas.SetTop(item.FieldShape, item.YCoord);
                gameWindow.BoardSpace.Children.Add(item.FieldShape);
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            this.pacman.Move();
        }

        public void StartTimer()
        {
            boardTimer.Start();
        }


        public static int XSize { get { return 19; } }
        public static int YSize { get { return 22; } }
        public List<Wall> Walls { get { return walls; } }
        public Canvas gameCanvas { get{ return gameWindow.BoardSpace;} }
        public Pac Pacman { set { this.pacman = value; } }
        public Food BoardFood { get { return this.boardFood; } }
    }
}
