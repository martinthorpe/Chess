using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    public bool side;
    public char look;
    public int points;

    //returns all possible moves for the piece in the y and x coords of the chessboard
    public virtual List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        return movesList;
    }

    //returns array with possible new position and points gained
    public int[] checkPos(int y, int x, int changeY, int changeX, Piece[,] chessBoard)
    {
        int[] newArray = new int[3];
        if (y + changeY < 0 || y + changeY > 7 || x + changeX < 0 || x + changeX > 7)
        {
            newArray = null;
        }
        else if (chessBoard[y + changeY, x + changeX] != null && chessBoard[y + changeY, x + changeX].side == side)
        {
            newArray = null;
        }
        else
        {
            if (chessBoard[y + changeY, x + changeX] == null)
            {
                newArray[0] = y + changeY;
                newArray[1] = x + changeX;
                newArray[2] = 0;
            }
            else if (chessBoard[y + changeY, x + changeX].side == !side)
            {
                newArray[0] = y + changeY;
                newArray[1] = x + changeX;
                newArray[2] = chessBoard[y + changeY, x + changeX].points;
            }
        }
        return newArray;
    }
}

class King : Piece
{
    public King(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'K';
        }
        else if (!s)
        {
            look = 'k';
        }
        points = 10 * difficulty;
    }

    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        for (int ny = -1; ny <= 1; ny++)
        {
            for (int nx = -1; nx <= 1; nx++)
            {
                if (ny == 0 && nx == 0)
                {

                }
                else
                {
                    int[] newArray = checkPos(y, x, ny, nx, chessBoard);
                    if (newArray != null)
                    {
                        movesList.Add(newArray);
                    }
                }
            }
        }
        return movesList;
    }
}

class Queen : Piece
{
    public Queen(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'Q';
        }
        else if (!s)
        {
            look = 'q';
        }
        points = 4 * difficulty;
    }

    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        bool top = true;
        bool bottom = true;
        bool left = true;
        bool right = true;
        bool topLeft = true;
        bool topRight = true;
        bool bottomLeft = true;
        bool bottomRight = true;
        int change = 1;
        while ((top || bottom || left || right || topLeft || topRight || bottomLeft || bottomRight) && change < 8)
        {
            if (top)
            {
                int[] newArray = checkPos(y, x, change * -1, 0, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        top = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    top = false;
                }
            }
            if (bottom)
            {
                int[] newArray = checkPos(y, x, change, 0, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottom = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottom = false;
                }
            }
            if (left)
            {
                int[] newArray = checkPos(y, x, 0, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        left = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    left = false;
                }
            }
            if (right)
            {
                int[] newArray = checkPos(y, x, 0, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        right = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    right = false;
                }
            }
            if (topLeft)
            {
                int[] newArray = checkPos(y, x, change * -1, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        topLeft = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    topLeft = false;
                }
            }
            if (topRight)
            {
                int[] newArray = checkPos(y, x, change * -1, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        topRight = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    topRight = false;
                }
            }
            if (bottomLeft)
            {
                int[] newArray = checkPos(y, x, change, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottomLeft = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottomLeft = false;
                }
            }
            if (bottomRight)
            {
                int[] newArray = checkPos(y, x, change, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottomRight = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottomRight = false;
                }
            }
            change++;
        }
        return movesList;
    }

}

class Bishop : Piece
{
    public Bishop(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'B';
        }
        else if (!s)
        {
            look = 'b';
        }
        points = 3 * difficulty;
    }
    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        bool topLeft = true;
        bool topRight = true;
        bool bottomLeft = true;
        bool bottomRight = true;
        int change = 1;
        while (topLeft || topRight || bottomLeft || bottomRight)
        {
            if (topLeft)
            {
                int[] newArray = checkPos(y, x, change * -1, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        topLeft = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    topLeft = false;
                }
            }
            if (topRight)
            {
                int[] newArray = checkPos(y, x, change * -1, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        topRight = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    topRight = false;
                }
            }
            if (bottomLeft)
            {
                int[] newArray = checkPos(y, x, change, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottomLeft = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottomLeft = false;
                }
            }
            if (bottomRight)
            {
                int[] newArray = checkPos(y, x, change, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottomRight = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottomRight = false;
                }
            }
            change++;
        }
        return movesList;
    }
}

class Knight : Piece
{
    public Knight(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'H';
        }
        else if (!s)
        {
            look = 'h';
        }
        points = 3 * difficulty;
    }

    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        int ny = -1;
        int nx = -2;
        for (int i = 0; i < 8; i++)
        {
            int[] newArray = checkPos(y, x, ny, nx, chessBoard);
            if (newArray != null)
            {
                movesList.Add(newArray);
            }
            int c = ny;
            ny = nx;
            nx = c;
            if ((ny < 0 && nx > 0) || (ny > 0 && nx < 0))
            {
                ny *= -1;
                nx *= -1;
            }
            if (i == 1)
            {
                nx *= -1;
            }
            else if (i == 3)
            {
                ny *= -1;
                nx *= -1;
            }
            else if (i == 5)
            {
                nx *= -1;
            }
        }
        return movesList;
    }
}

class Rook : Piece
{
    public Rook(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'R';
        }
        else if (!s)
        {
            look = 'r';
        }
        points = 3 * difficulty;
    }

    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        bool top = true;
        bool bottom = true;
        bool left = true;
        bool right = true;
        int change = 1;
        while (top || bottom || left || right)
        {
            if (top)
            {
                int[] newArray = checkPos(y, x, change * -1, 0, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        top = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    top = false;
                }
            }
            if (bottom)
            {
                int[] newArray = checkPos(y, x, change, 0, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        bottom = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    bottom = false;
                }
            }
            if (left)
            {
                int[] newArray = checkPos(y, x, 0, change * -1, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        left = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    left = false;
                }
            }
            if (right)
            {
                int[] newArray = checkPos(y, x, 0, change, chessBoard);
                if (newArray != null)
                {
                    if (newArray[2] != 0)
                    {
                        right = false;
                    }
                    movesList.Add(newArray);
                }
                else
                {
                    right = false;
                }
            }
            change++;
        }
        return movesList;
    }
}

class Pawn : Piece
{
    public Pawn(bool s, int difficulty)
    {
        side = s;
        if (s)
        {
            look = 'P';
        }
        else if (!s)
        {
            look = 'p';
        }
        points = 1 * difficulty;
    }

    public override List<int[]> possibleMoves(int y, int x, Piece[,] chessBoard)
    {
        List<int[]> movesList = new List<int[]>();
        int ny = 1;
        if (side)
        {
            ny *= -1;
        }
        for (int nx = -1; nx < 2; nx++)
        {
            int[] newArray = checkPos(y, x, ny, nx, chessBoard);
            if (newArray != null)
            {
                if ((newArray[2] == 0 && nx != 0) || (newArray[2] != 0 && nx == 0))
                {

                }
                else
                {
                    if (side && ny == 0 || !side && ny == 7)
                    {
                        Queen cq = new Queen(!side, points / 1);
                        newArray[2] += cq.points;
                    }
                    movesList.Add(newArray);
                }
            }
            if (newArray != null)
            {
                if (nx == 0 && newArray[2] == 0)
                {
                    if ((side && y == 6) || (!side && y == 1))
                    {
                        int[] tempArray = checkPos(y, x, ny * 2, 0, chessBoard);
                        if (tempArray != null)
                        {
                            if (chessBoard[y + (ny * 2), x] == null)
                            {
                                movesList.Add(tempArray);
                            }
                        }
                    }
                }
            }
        }
        return movesList;
    }
}