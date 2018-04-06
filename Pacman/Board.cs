using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
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
        private GhostManager gameGhostManager;
        private List<Key>[,] availableDirections;

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
            this.AssignDirections();
            this.gameWindow = window;
            this.walls = new List<Wall>();
            this.boardFood = new Food(this);
            boardFood.GenerateFood();
            boardFood.PrintTheDots();
            InitializeWalls();
            boardTimer = new DispatcherTimer();
            boardTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            boardTimer.Interval = new TimeSpan(0, 0, 0, 0, 32);

            pacman = new Pac(this);
            pacman.PrintTheHero();

            this.gameGhostManager = new GhostManager(this);
        }

        private void InitializeWalls()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (wallsDefinition[i, j] == 1)
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
            this.gameGhostManager.MoveThemAll();
        }

        public void StartTimer()
        {
            boardTimer.Start();
        }

        private void AssignDirections()
        {
            this.availableDirections = new List<Key>[22, 19];

            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (wallsDefinition[i, j] == 0)
                    {
                        List<Key> newList = new List<Key>();
                        if (j > 0)
                        {
                            if (wallsDefinition[i, j - 1] == 0)
                            {
                                newList.Add(Key.Left);
                            }
                            if (wallsDefinition[i, j + 1] == 0)
                            {
                                newList.Add(Key.Right);
                            }
                        }
                        else if ((i == 10) && (j == 0))
                        {
                            newList.Add(Key.Left);
                        }
                        else if ((i == 10) && (j == 18))
                        {
                            newList.Add(Key.Right);
                        }
                        if (wallsDefinition[i - 1, j] == 0)
                        {
                            newList.Add(Key.Up);
                        }
                        if (wallsDefinition[i + 1, j] == 0)
                        {
                            newList.Add(Key.Down);
                        }

                        this.availableDirections[i, j] = newList;
                    }
                    else this.availableDirections[i, j] = null;
                }
            }
        }

        public List<Key> FieldAvailableDirections(int x, int y)
        {
            int tempX = x / Field.Module;
            int tempY = y / Field.Module;

            return this.availableDirections[tempY, tempX];
        }

        public bool CheckPositioinClean(int x, int y)
        {
            if ((x % Field.Module == 0) && (y % Field.Module == 0))
            {
                return true;
            }
            else return false;
        }

        public static int XSize { get { return 19; } }
        public static int YSize { get { return 22; } }
        public List<Wall> Walls { get { return walls; } }
        public Canvas gameCanvas { get { return gameWindow.BoardSpace; } }
        public Pac Pacman { get { return this.pacman; } set { this.pacman = value; } }
        public Food BoardFood { get { return this.boardFood; } }
    }
}
