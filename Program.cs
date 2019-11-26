using System;
using System.Net;
using System.Threading;

namespace TicTacToeOOP
{
    static class Program 
    {
        public static void Main(string[] args)
        {
            int result;
            int roundCounter = 1;

            Board newBoard = new Board();
            Print printTheMessage=new Print();
            GameRules newGameRules= new GameRules();
            
            printTheMessage.PrintTheMessage("Welcome to Tic Tac Toe!");
            printTheMessage.PrintTheMessage("\n");
            printTheMessage.PrintTheMessage("Here's the current board:");
            printTheMessage.PrintTheMessage("\n");
            newBoard.PrintBoard(newBoard.BoardArray);

            do
            {
                Players player = new Players();

                printTheMessage.PrintTheMessage("\n");
                printTheMessage.PrintWhichPlayerShouldPlay(roundCounter);
                string position = Console.ReadLine();
                int choiceOfPlayer = player.GetArrayIndex(position);

                if (choiceOfPlayer == 10)
                {
                    printTheMessage.PrintTheMessage("The position coordinate is invalid, try again......");
                    printTheMessage.PrintTheMessage("\n");
                    Thread.Sleep(2000);
                }

                if ((newBoard.BoardArray[choiceOfPlayer] == player.Player1 && choiceOfPlayer != 0) || (newBoard.BoardArray[choiceOfPlayer] == player.Player2 && choiceOfPlayer != 0))
                {
                    printTheMessage.PrintTheMessage("Oh no, a piece is already at this place! Try again..");
                    printTheMessage.PrintTheMessage("\n");
                    roundCounter++;
                }

                player.UpdateTokens(player.Player1, player.Player2, newBoard.BoardArray, roundCounter, choiceOfPlayer);
                newBoard.PrintBoard(newBoard.BoardArray);
                roundCounter++;
                result = newGameRules.CheckIfThePlayerWin(player.Player1, player.Player2, newBoard.BoardArray);

            } while (result != 1 && result != -1);
            {   
                newBoard.PrintBoard(newBoard.BoardArray); 
                newGameRules.CheckIfConditionIsWinning(result,roundCounter,newBoard.BoardArray);
            }
        }
    }
}
public class Print
{
    public void PrintTheMessage(string message)
    {
        Console.Write(message);
    }

    public void PrintWhichPlayerShouldPlay(int roundCounter)
    {
        if (roundCounter % 2 == 0)
        {
            PrintTheMessage("Player 2 enter a coord x,y to place your X or enter 'q' to give up: ");
        }

        else
        {
            PrintTheMessage("Player 1 enter a coord x,y to place your X or enter 'q' to give up: ");
        }
    }
}

public class Board {

    private char[] _boardArray = {'.', '.', '.', '.', '.', '.', '.', '.', '.', '.'};
    public char[] BoardArray
    { 
        get
        {
            return _boardArray;
        }
    }
    public void PrintBoard(char[] boardArray)
    {
        Console.WriteLine("  {0}    {1}    {2}", _boardArray[1], _boardArray[2], _boardArray[3]);
        Console.WriteLine("  {0}    {1}    {2}", _boardArray[4], _boardArray[5], _boardArray[6]);
        Console.WriteLine("  {0}    {1}    {2}", _boardArray[7], _boardArray[8], _boardArray[9]);
    }
}

public class GameRules
{ 
    public int CheckIfThePlayerWin(char player1, char player2, char[] boardArray)
    {
        //check the horizontal condition
        int i = 1;
        for (i = 1; i < 8; i += 3)
        {
            if ((boardArray[i] == player1 && boardArray[i + 1] == player1 && boardArray[i + 2] == player1) ||
                (boardArray[i] == player2 && boardArray[i + 1] == player2 && boardArray[i + 2] == player2))
            {
                return 1;
            }
        }

        //check the vertical condition
        for (i = 1; i < 4; i++)
        {
            if ((boardArray[i] == player1 && boardArray[i + 3] == player1 && boardArray[i + 6] == player1) ||
                (boardArray[i] == player2 && boardArray[i + 3] == player2 && boardArray[i + 6] == player2))
            {
                return 1;
            }
        }

        //check diagonal condition
        for (i = 1; i < 2; i++)
        {
            if ((boardArray[i] == player1 && boardArray[i + 4] == player1 && boardArray[i + 8] == player1) ||
                (boardArray[i] == player2 && boardArray[i + 4] == player2 && boardArray[i + 8] == player2))
                return 1;

        }

        for (i = 3; i < 4; i++)
        {
            if ((boardArray[i] == player2 && boardArray[i + 2] == player2 && boardArray[i + 4] == player2) ||
                (boardArray[i] == player1 && boardArray[i + 2] == player1 && boardArray[i + 4] == player1))
                return 1;
        }

        //check if it's a draw
        if (boardArray[1] != '.' && boardArray[2] != '.' && boardArray[3] != '.' && boardArray[4] != '.' &&
            boardArray[5] != '.' && boardArray[6] != '.' && boardArray[7] != '.' && boardArray[8] != '.' &&
            boardArray[9] != '.')
        {
            return -1;
        }

        return 0; 
    }
    
    public void CheckIfConditionIsWinning(int result, int roundCounter, char[] boardArray)
    {   
        Console.Clear();
        Board newBoard=new Board();
        Print printTheMessage=new Print();
        //newBoard.PrintBoard(boardArray);
        
        if (result == 1)
        {   
            printTheMessage.PrintTheMessage("\n");
            printTheMessage.PrintTheMessage($"Well done Player {roundCounter % 2 + 1} won the game!");
            printTheMessage.PrintTheMessage("\n");
        }

        if (result == -1)
        {
            printTheMessage.PrintTheMessage("Draw");
            printTheMessage.PrintTheMessage("\n");
        }
    }
}


public class Players
{
        private const char _player1 = 'X';
        private const char _player2 = 'O';
        
        public char Player1
        {
            get { return _player1; }
        }

        public char Player2
        {
            get { return _player2; }
        }

        public int GetArrayIndex(string position)
        {
            var coordinatesStrings = new string[11] {"0,0", "1,1", "1,2", "1,3", "2,1", "2,2", "2,3", "3,1", "3,2", "3,3", "q"};
            for (int i = 1; i < 10; i++)
            {
                if (position == coordinatesStrings[i])
                {
                    return i;
                }
            }

            if (position == coordinatesStrings[10])
            {
                return 0;
            }
            return 10;
        }

        public void UpdateTokens(char player1, char player2, char[] boardArray, int roundCounter, int choiceOfPlayer)
        {    
            Board newBoard=new Board();
            Print printTheMessage=new Print();
            
            if (choiceOfPlayer == 0)
            {
                roundCounter++;
                printTheMessage.PrintTheMessage($"Player {(roundCounter % 2) + 1} has given up for this round");
                printTheMessage.PrintTheMessage("\n");
                roundCounter += 2;
            }

            if (choiceOfPlayer != 0 && boardArray[choiceOfPlayer] != player1 && boardArray[choiceOfPlayer] != player2)
            {
                if (roundCounter % 2 == 0)
                {
                    boardArray[choiceOfPlayer] = player2;
                }

                else
                {
                    boardArray[choiceOfPlayer] = player1;
                }
                printTheMessage.PrintTheMessage("Move accepted, here's the current board: ");
                printTheMessage.PrintTheMessage("\n");
            }
        } 
}

