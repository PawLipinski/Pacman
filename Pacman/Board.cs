using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Pacman
{
    class Board
    {

        public List<Wall> walls;
        private MainWindow gameWindow;

        public Board(MainWindow window)
        {
            this.gameWindow = window;
            this.walls = new List<Wall>();
            InitializeWalls();
        }

        private void InitializeWalls()
        {
            InitializeExternalWalls();
            InitializeInternalWalls();
            MirrorBoard();
        }

        private void InitializeInternalWalls()
        {

            List<int> myList = new List<int> { 1, 4, 8 };

            for (int j = 2; j < 4; j++)
            {
                AddHorizontalWallExcept(myList, j);
            }

            AddHorizontalWallExcept(new List<int> { 1, 4, 6 }, 5);
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 6, 7, 8 }, 6);
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 8 }, 7);
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 6, 7, 8, 9 }, 8);
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 6, 9 }, 9);
            walls.Add(new Wall(7, 10));
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 6}, 11);
            walls.Add(new Wall(5,12));
            AddHorizontalWallExcept(new List<int> { 1, 2, 3, 4, 6 }, 13);
            walls.Add(new Wall(9, 14));
            AddHorizontalWallExcept(new List<int> { 1, 4, 8 }, 15);
            walls.Add(new Wall(3, 16));
            AddHorizontalWallExcept(new List<int> { 2,4,6},17);
            walls.Add(new Wall(5, 18));
            walls.Add(new Wall(9, 18));
            AddHorizontalWallExcept(new List<int>{1,8},19);
        }

        private void AddHorizontalWallExcept(List<int> exceptionsList, int YPosition)
        {
            for (int i = 1; i < Board.XSize / 2 + 1; i++)
            {
                if (!exceptionsList.Contains(i))
                {
                    walls.Add(new Wall(i, YPosition));
                }
            }
        }

        private void InitializeExternalWalls()
        {
            //Extreme horizontal lines
            for (int i = 0; i <= XSize / 2; i++)
            {
                walls.Add(new Wall(i, 0));
                walls.Add(new Wall(i, Board.YSize - 1));
            }

            //Vertical lines
            List<int> myList = new List<int> { 8, 10, 12 };
            for (int i = 1; i <= Board.YSize - 1; i++)
            {
                if (!myList.Contains(i))
                {
                    walls.Add(new Wall(0, i));
                }
            }
            myList = null;

            for (int i = 1; i <= 3; i++)
            {
                walls.Add(new Wall(i, (Board.YSize / 2 - 4)));
                walls.Add(new Wall(i, (Board.YSize / 2 - 2)));
                walls.Add(new Wall(i, (Board.YSize / 2)));
                walls.Add(new Wall(i, (Board.YSize / 2 + 2)));
            }

            walls.Add(new Wall(3, 8));
            walls.Add(new Wall(3, 12));

            walls.Add(new Wall(9, 1));

        }

        private void MirrorBoard()
        {
            List<Wall> newWalls = new List<Wall>();
            foreach (var item in walls)
            {
                newWalls.Add(item.MirrorWall());
            }

            walls.AddRange(newWalls);
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


        public static int XSize { get { return 19; } }
        public static int YSize { get { return 22; } }
        public List<Wall> Walls { get { return walls; } }
    }
}
