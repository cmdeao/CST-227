/* Cameron Deao
 * CST-227
 * James Shinevar
 * 6/9/2019 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cameron_Deao_Milestone_1
{
    class Program
    {
        //Driver class that calls upon each function.
        static void Main(string[] args)
        {
            GameBoard board = new GameBoard(9);
            board.ActivateCell(9);
            board.SetCount();
            board.DisplayBoard();
            Console.ReadKey();
        }
    }
}

//Gameboard class.
class GameBoard
{
    //Two-dimensional array that will be used for the gameboard.
    public GameCell[,] board;
    
    //Function to set the size of the board.
    public GameBoard(int grid)
    {
        //Setting the index sizes based on the passed int value.
        board = new GameCell[grid, grid];
        //Populating each index with a gamecell.
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int x = 0; x < board.GetLength(1); x++)
            {
                board[i, x] = new GameCell();
            }
        }   
    }

    //Function to randomly activate various cells within the board.
    public void ActivateCell(int percentage)
    {
        //Creating two randoms to be used.
        Random active = new Random();
        Random cellChoice = new Random();
        //Variables used throughout the function.
        int rowChoice = 0;
        int colChoice = 0;
        //Performing the math to determine how many cells will go live.
        int totalCells = percentage * percentage;
        int range = active.Next(15, 21);
        double result = (double)range / (double)100;
        double totalPercent = result * (double)totalCells;
        int interval = (int)Math.Floor(totalPercent);
        //Randomly selecting indexes within the two-dimensional array to set live.
        for (int i = 0; i < interval; i++)
        {
            rowChoice = cellChoice.Next(0, 9);
            colChoice = cellChoice.Next(0, 9);
            board[rowChoice, colChoice].Live = true;
        }  
    }

    //Setting the count of each cell's live neighbors.
    public void SetCount()
    {
        //Iterating through the array and checking each direction to find a live cell.
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int x = 0; x < board.GetLength(1); x++)
            {
                //Counter for live neighbors.
                int neighborCounter = 0;
                //Each if statement checks if a cell is live in a specific direction.
                if (i > 0  && board[i - 1, x].Live == true)
                {
                    //Each statement sets the found index neighbors live value to 9 
                    //if found to be true.
                    board[i - 1, x].NeighborsLive = 9;
                    //Icrementing the counter up.
                    neighborCounter++;
                }
                if(x > 0 && board[i, x - 1].Live == true)
                {
                    board[i, x - 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(i > 0 && x > 0 && board[i - 1, x - 1].Live == true)
                {
                    board[i - 1, x - 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(i < board.GetLength(0) - 1 && board[i + 1, x].Live == true)
                {
                    board[i + 1, x].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(x < board.GetLength(0) - 1 && board[i, x + 1].Live == true)
                {
                    board[i, x + 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(i < board.GetLength(0) - 1 && x < board.GetLength(0) - 1 && board[i + 1, x + 1].Live == true)
                {
                    board[i + 1, x + 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(i < board.GetLength(0) - 1 && x > 0 && board[i + 1, x - 1].Live == true)
                {
                    board[i + 1, x - 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                if(i > 0 && x < board.GetLength(0) - 1 && board[i - 1, x + 1].Live == true)
                {
                    board[i - 1, x + 1].NeighborsLive = 9;
                    neighborCounter++;
                }
                //Setting the current index neighbors live value to the counter.
                board[i, x].NeighborsLive = neighborCounter;
            }
        }
    }

    //Function to display the board.
    public void DisplayBoard()
    {
        //Setting int values to size of the array.
        int rowLength = board.GetLength(0);
        int colLenth = board.GetLength(1);
        //Iterating the array to display.
        for(int i = 0; i < rowLength; i++)
        {
            for(int x = 0; x < colLenth; x++)
            {
                //Displaying an asterisk if a cell is live.
                if(board[i,x].Live == true)
                {
                    Console.Write(string.Format("`"));
                }
                //If the current cell is not live the neighbors live variable is displayed
                //with it's current value.
                else
                {
                    Console.Write(string.Format(board[i,x].NeighborsLive.ToString()));
                }
            }
            //Formatting to display the board properly.
            Console.Write(Environment.NewLine);
        }
    }
}

//Gamecell class to establish each property.
class GameCell
{
    //Each variable used within gamecell.
    private bool visited;
    private bool live;
    private int neighborsLive;
    private int row;
    private int col;

    public GameCell()
    {
        visited = false;
        live = false;
        neighborsLive = 0;
        row = -1;
        col = -1;
    }

    //Getters and setters used for each property.
    public bool VisitedCell
    {
        get { return visited; }
        set { visited = value; }
    }

    public bool Live
    {
        get { return live; }
        set { live = value; }
    }

    public int NeighborsLive
    {
        get { return neighborsLive; }
        set { neighborsLive = value; }
    }

    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    public int Col
    {
        get { return col; }
        set { col = value; }
    }
}