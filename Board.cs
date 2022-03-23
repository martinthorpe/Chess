using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    Piece[,] chessBoard;

    public Board()
    {
        chessBoard = new Piece[8, 8];
    }

    public void makeBoard(int difficulty)
    {
        chessBoard[0, 0] = new Rook(false, difficulty);
        chessBoard[0, 1] = new Knight(false, difficulty);
        chessBoard[0, 2] = new Bishop(false, difficulty);
        chessBoard[0, 3] = new Queen(false, difficulty);
        chessBoard[0, 4] = new King(false, difficulty);
        chessBoard[0, 5] = new Bishop(false, difficulty);
        chessBoard[0, 6] = new Knight(false, difficulty);
        chessBoard[0, 7] = new Rook(false, difficulty);
        for (int i = 0; i < 8; i++)
        {
            chessBoard[1, i] = new Pawn(false, difficulty);
        }
        chessBoard[7, 0] = new Rook(true, difficulty);
        chessBoard[7, 1] = new Knight(true, difficulty);
        chessBoard[7, 2] = new Bishop(true, difficulty);
        chessBoard[7, 3] = new Queen(true, difficulty);
        chessBoard[7, 4] = new King(true, difficulty);
        chessBoard[7, 5] = new Bishop(true, difficulty);
        chessBoard[7, 6] = new Knight(true, difficulty);
        chessBoard[7, 7] = new Rook(true, difficulty);
        for (int i = 0; i < 8; i++)
        {
            chessBoard[6, i] = new Pawn(true, difficulty);
        }
    }

    public Piece[,] returnBoard()
    {
        return chessBoard;
    }

    public void changeBoard(Piece[,] b)
    {
        chessBoard = b;
    }

    public Piece returnSquare(int y, int x)
    {
        return chessBoard[y, x];
    }

    public void changeSquare(int y, int x, Piece p)
    {
        chessBoard[y, x] = p;
    }

    public void move(int cy, int cx, int ny, int nx)
    {
        //replaces old place with null and moves the piece
        chessBoard[ny, nx] = chessBoard[cy, cx];
        chessBoard[cy, cx] = null;
    }
}