using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class board
    {
        public int rows { get; set; }
        public int cols { get; set; }
        public int density { get; set; }
        public cell[,] grid { get; set; }
        public bool active_status { get; set; }

        public board(int rows, int cols, int density, bool status)
        {
            this.rows = rows;
            this.cols = cols;
            this.density = density;
            this.active_status = status;
            this.setupGrid(rows, cols, density);
        }

        private void setupGrid(int rows, int cols, int density)
        {
            Random r = new Random();

            this.grid = new cell[rows, cols];

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    int x = r.Next(0, density); 
                    switch (x)
                    {
                        case 0:
                            grid[i, j] = new cell("X");
                            break;
                        default:
                            grid[i, j] = new cell(" ");
                            break;
                    }
                }
            }
        }

        public void displayGrid()
        {
            Console.Clear();
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    Console.Write(grid[i, j].state);
                }
                Console.Write("\r\n");
            }
        }
    }
}
