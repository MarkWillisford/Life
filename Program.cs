using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameRunning = true;
            ConsoleKeyInfo userKey;

            int cols = 75;
            int rows = 20;
            int density = 4;

            board grid1 = new board(rows, cols, density, true);
            board grid2= new board(rows, cols, density, false);

            // A quick test to varify that my Tick function is properly opperating on the original grids
            //Tick(ref grid2, ref grid1);     // passing in F / T
            //Console.WriteLine("Grid 1 active status should be false");
            //Console.WriteLine("Grid 1 active status is " + grid1.active_status);
            //Console.WriteLine("Grid 2 active status should be true");
            //Console.WriteLine("Grid 2 active status is " + grid2.active_status);
            //Console.ReadLine();

            while (gameRunning)
            {
                // Begin with processing input
                if (Console.KeyAvailable)
                {
                    // We have input, process accordingly
                    userKey = Console.ReadKey(true);

                    switch (userKey.Key)
                    {
                        case ConsoleKey.Escape:
                            // Exit the game when pressed
                            gameRunning = false;
                            break;
                    }
                }

                Tick(ref grid1, ref grid2, rows, cols);
            }
            Console.ReadLine();

        }

        static void Tick(ref board board_x, ref board board_y, int row, int col)
        {
            int rows = row;
            int cols = col;
            board newGrid, oldGrid;
            // this sets our grid variables so that we are operating on the correct grids
            if (board_x.active_status == true)
            {
                oldGrid = board_x;
                newGrid = board_y;
            }
            else
            {
                oldGrid = board_y;
                newGrid = board_x;
            }

            // we check the values of the old grid and if applicable we set the new grid to it.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    // Took this part out because I wanted to upgrade it to wrap around the edges
                    //// if we are on the edge we just set the new to the old
                    //if (i == 0 || i == (rows - 1) || j == 0 || j == (cols - 1))
                    //{
                    //    newGrid.grid[i, j].state = oldGrid.grid[i, j].state;
                    //}
                    //else
                    //{
                        // if not, we need to count neighbors
                        int neightbors = countNeighbors(oldGrid, i, j, rows, cols);
                        if (oldGrid.grid[i,j].state == " " && neightbors == 3) // ie it is 'dead' and it has 3 living neighbors
                        {
                            newGrid.grid[i, j].state = "X";
                        }
                        else if(oldGrid.grid[i, j].state == "X" && ( neightbors < 2 || neightbors > 3 ))
                        {
                            // or it is alive and has less than 2 or more than 3 living neighbors
                            newGrid.grid[i, j].state = " ";
                        }
                        else
                        {
                            newGrid.grid[i, j].state = oldGrid.grid[i, j].state;
                        }
                    //}
                }
            }

            // now we reverse which grid is active. setting the new one to true
            newGrid.active_status = true;
            oldGrid.active_status = false;

            // finally we display the new grid
            newGrid.displayGrid();
        }

        static int countNeighbors(board oldGrid, int i, int j, int row, int col)
        {
            int result = 0;

            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    // This is to wrap the whole board into a sphere (basically)
                    int rows = (x + i + row) % row;
                    int cols = (y + j + col) % col;

                    if (oldGrid.grid[rows,cols].state != " ")
                    {
                        result++;
                    }
                }
            }
            // this removes the current cell from the count if needed
            if(oldGrid.grid[i,j].state != " ")
            {
                result--;
            }
            return result;
        }
    }
}