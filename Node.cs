using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int pointsGained;
    public int lastY;
    public int lastX;
    public int newY;
    public int newX;
    public bool nSide;
    public Board currentBoard;
    public List<Node> listOfPossibleMoves;

    public Node(int pg, int ly, int lx, int cy, int cx, bool s, Board cb)
    {
        pointsGained = pg;
        lastY = ly;
        lastX = lx;
        newY = cy;
        newX = cx;
        currentBoard = cb;
        nSide = s;
        listOfPossibleMoves = new List<Node>();
    }

    public void Add(int pg, int ly, int lx, int cy, int cx, bool s, Board cb)
    {
        listOfPossibleMoves.Add(new Node(pg, ly, lx, cy, cx, s, cb));
    }

    public void addToMoveList()
    {
        //checks every slot in the currentboard array
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (currentBoard.returnSquare(y, x) != null)
                {
                    //if slot has a black piece there
                    if (currentBoard.returnSquare(y, x).side == nSide)
                    {
                        //all possible moves that piece can do
                        List<int[]> moveList = currentBoard.returnSquare(y, x).possibleMoves(y, x, currentBoard.returnBoard());
                        foreach (int[] a in moveList)
                        {
                            //makes new board with the change
                            Board board = new Board();
                            for (int w = 0; w < 8; w++)
                            {
                                for (int e = 0; e < 8; e++)
                                {
                                    board.changeSquare(w, e, currentBoard.returnSquare(w, e));
                                }
                            }
                            board.move(y, x, a[0], a[1]);
                            //check player possible takeaways
                            int biggest = 0;
                            for (int c = 0; c < 8; c++)
                            {
                                for (int v = 0; v < 8; v++)
                                {
                                    if (board.returnSquare(c, v) != null)
                                    {
                                        if (board.returnSquare(c, v).side == !nSide)
                                        {
                                            List<int[]> playerMoveList = board.returnSquare(c, v).possibleMoves(c, v, board.returnBoard());
                                            foreach (int[] s in playerMoveList)
                                            {
                                                if (s[2] > biggest)
                                                {
                                                    biggest = s[2];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            a[2] -= biggest;
                            //makes new node with change in pieces and adds it to current node's list of possible moves
                            Add(a[2], y, x, a[0], a[1], nSide, board);
                        }
                    }
                }
            }
        }
        for (int u = 0; u < listOfPossibleMoves.Count;)
        {
            if (listOfPossibleMoves[u].checkIfCheck())
            {
                listOfPossibleMoves.RemoveAt(u);
            }
            else
            {
                u++;
            }
        }
    }

    //checks if king is in check
    public bool checkIfCheck()
    {
        int kingY = 0;
        int kingX = 0;
        for (int ky = 0; ky < 8; ky++)
        {
            for (int kx = 0; kx < 8; kx++)
            {
                if (currentBoard.returnSquare(ky, kx) != null)
                {
                    if (!nSide && currentBoard.returnSquare(ky, kx).look == 'k')
                    {
                        kingY = ky;
                        kingX = kx;
                        break;
                    }
                    else if (nSide && currentBoard.returnSquare(ky, kx).look == 'K')
                    {
                        kingY = ky;
                        kingX = kx;
                        break;
                    }
                }
            }
        }
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (currentBoard.returnSquare(y, x) != null)
                {
                    if (currentBoard.returnSquare(y, x).side == !nSide)
                    {
                        List<int[]> playerMoveList = currentBoard.returnSquare(y, x).possibleMoves(y, x, currentBoard.returnBoard());
                        foreach (int[] s in playerMoveList)
                        {
                            if (s[0] == kingY && s[1] == kingX)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
}
