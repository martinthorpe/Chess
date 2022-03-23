using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer
{
    public Node currentNode;
    public int difficulty;

    public Computer(int d, Board mainChessBoard)
    {
        difficulty = d;
        resetCurrentBoard(mainChessBoard);
    }

    public void resetCurrentBoard(Board mainChessBoard)
    {
        Board b = new Board();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                b.changeSquare(y, x, mainChessBoard.returnSquare(y, x));
            }
        }
        currentNode = new Node(0, 0, 0, 0, 0, false, b);
    }

    public void possibleMoves(Node n)
    {
        n.addToMoveList();
    }

    public int[] decideMove()
    {
        //get all possible moves
        possibleMoves(currentNode);
        if (!currentNode.checkIfCheck())
        {
            if (difficulty > 1)
            {
                foreach (Node n in currentNode.listOfPossibleMoves)
                {
                    possibleMoves(n);
                    if (difficulty == 3)
                    {
                        foreach (Node z in n.listOfPossibleMoves)
                        {
                            possibleMoves(z);
                        }
                    }
                }
            }
        }
        //checks if can actually move
        if (currentNode.checkIfCheck() && currentNode.listOfPossibleMoves.Count == 0)
        {
            return null;
        }
        else if (currentNode.listOfPossibleMoves.Count == 0)
        {
            int[] other = { -1 };
            return other;
        }
        //if can move
        else
        {
            //sets mean for all poissble moves and their futures
            if (difficulty > 1)
            {
                foreach (Node q in currentNode.listOfPossibleMoves)
                {
                    if (q.listOfPossibleMoves.Count > 0)
                    {
                        int et = q.listOfPossibleMoves[0].pointsGained;
                        foreach (Node e in q.listOfPossibleMoves)
                        {
                            if (e.listOfPossibleMoves.Count > 0)
                            {
                                if (difficulty == 3)
                                {
                                    int rt = e.listOfPossibleMoves[0].pointsGained;
                                    foreach (Node r in e.listOfPossibleMoves)
                                    {
                                        rt += r.pointsGained;
                                    }
                                    e.pointsGained += rt / e.listOfPossibleMoves.Count - 1;
                                }
                                et += e.pointsGained;
                            }
                        }
                        q.pointsGained += et / q.listOfPossibleMoves.Count - 1;
                    }
                }
            }
            //makes array of all possible moves with points gained
            int value = 0;
            int[] paths = new int[currentNode.listOfPossibleMoves.Count];
            int i = 0;
            foreach (Node n in currentNode.listOfPossibleMoves)
            {
                paths[i] = value + n.pointsGained;
                i++;
            }
            //checks for heightest points gained
            int pointer = 0;
            int high = paths[0];
            int amount = 0;
            for (int w = 0; w < paths.Length; w++)
            {
                if (high < paths[w])
                {
                    high = paths[w];
                    pointer = w;
                    amount = 1;
                }
                else
                {
                    amount++;
                }
            }
            //if there are mutiple of the heightest points gained
            if (amount > 1)
            {
                //picks randomly out of those heightest points gained
                bool repeat = true;
                while (repeat)
                {
                    int newPointer = Random.Range(0, paths.Length);
                    if (paths[newPointer] == high)
                    {
                        pointer = newPointer;
                        repeat = false;
                    }
                }
            }
            //returns the move of the heightest points gained
            int[] fins = { currentNode.listOfPossibleMoves[pointer].lastY, currentNode.listOfPossibleMoves[pointer].lastX, currentNode.listOfPossibleMoves[pointer].newY, currentNode.listOfPossibleMoves[pointer].newX };
            return fins;
        }
    }
}
