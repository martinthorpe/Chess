using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMan : MonoBehaviour
{
    public static Board mainChessBoard;
    public static Computer ai;
    public static int difficulty = 0;
    public static bool inGame = false;
    public static bool cantMove = false;
    public static bool won = false;
    public static bool lost = false;
    public static bool inStalemate = false;
    public static bool choiceP = false;
    public static int[] moveCoords = { -1, -1, -1, -1 };
    public static string movingS = "";
    public static string nextS = "";
    public static string chosenP = "";
    public static int tempY = 0;
    public static int tempX = 0;
    public GameObject choosePiecePanel;

    void Update()
    {
        //this is for changing pawn to other piece
        if (choiceP)
        {
            choosePiecePanel.SetActive(true);
        }
        else if (!choiceP)
        {
            choosePiecePanel.SetActive(false);
        }
        if (choiceP)
        {
            switch (chosenP)
            {
                case "Q":
                    mainChessBoard.changeSquare(tempY, tempX, new Queen(true, difficulty));
                    choiceP = false;
                    inGame = true;
                    break;
                case "B":
                    mainChessBoard.changeSquare(tempY, tempX, new Bishop(true, difficulty));
                    choiceP = false;
                    inGame = true;
                    break;
                case "H":
                    mainChessBoard.changeSquare(tempY, tempX, new Knight(true, difficulty));
                    choiceP = false;
                    inGame = true;
                    break;
                case "R":
                    mainChessBoard.changeSquare(tempY, tempX, new Rook(true, difficulty));
                    choiceP = false;
                    inGame = true;
                    break;
                default:
                    break;
            }
        }
    }

    //this is for changing the moving coords array 
    public static void updatingMove(int y, int x)
    {
        if (y == moveCoords[0] && x == moveCoords[1])
        {
            moveCoords[0] = -1;
            moveCoords[1] = -1;
            moveCoords[2] = -1;
            moveCoords[3] = -1;
        }
        else if (moveCoords[0] == -1 && moveCoords[1] == -1)
        {
            moveCoords[0] = y;
            moveCoords[1] = x;
        }
        else if (moveCoords[0] != -1 && moveCoords[1] != -1)
        {
            moveCoords[2] = y;
            moveCoords[3] = x;
        }
        int ny = 0;
        if (moveCoords[0] != -1 && moveCoords[1] != -1)
        {
            switch (moveCoords[0])
            {
                case 7:
                    ny = 1;
                    break;
                case 6:
                    ny = 2;
                    break;
                case 5:
                    ny = 3;
                    break;
                case 4:
                    ny = 4;
                    break;
                case 3:
                    ny = 5;
                    break;
                case 2:
                    ny = 6;
                    break;
                case 1:
                    ny = 7;
                    break;
                case 0:
                    ny = 8;
                    break;
            }
            movingS = positionCal(moveCoords[1]) + ny.ToString();
        }
        else
        {
            movingS = "";
        }
        if (moveCoords[2] != -1 && moveCoords[3] != -1)
        {
            switch (moveCoords[2])
            {
                case 7:
                    ny = 1;
                    break;
                case 6:
                    ny = 2;
                    break;
                case 5:
                    ny = 3;
                    break;
                case 4:
                    ny = 4;
                    break;
                case 3:
                    ny = 5;
                    break;
                case 2:
                    ny = 6;
                    break;
                case 1:
                    ny = 7;
                    break;
                case 0:
                    ny = 8;
                    break;
            }
            nextS = positionCal(moveCoords[3]) + ny.ToString();
        }
        else
        {
            nextS = "";
        }
    }

    public static void play()
    {
        cantMove = false;
        int i = playerMoveInput();
        if (i == 1)
        {
            moveCoords[0] = -1;
            moveCoords[1] = -1;
            moveCoords[2] = -1;
            moveCoords[3] = -1;
            movingS = "";
            nextS = "";
            pawnCheck(0);
            ai.resetCurrentBoard(mainChessBoard);
            if (computerMove())
            {
                pawnCheck(7);
            }
            else
            {
                inGame = false;
            }
        }
        else if (i == 2)
        {
            moveCoords[0] = -1;
            moveCoords[1] = -1;
            moveCoords[2] = -1;
            moveCoords[3] = -1;
            movingS = "";
            nextS = "";
            cantMove = true;
        }
        else if (i == 3)
        {
            inGame = false;
            lost = false;
        }
        else if (i == 4)
        {
            inGame = false;
            inStalemate = false;
        }
    }

    //checks if any pawns can be changed into other piece
    static void pawnCheck(int y)
    {
        for (int x = 0; x < 8; x++)
        {
            if (mainChessBoard.returnSquare(y, x) != null)
            {
                if (mainChessBoard.returnSquare(y, x).look == 'P')
                {
                    inGame = false;
                    choiceP = true;
                    tempY = y;
                    tempX = x;
                }
                else if (mainChessBoard.returnSquare(y, x).look == 'p')
                {
                    mainChessBoard.changeSquare(y, x, new Queen(false, difficulty));
                }
            }
        }
    }

    //handles AI move
    static bool computerMove()
    {
        int[] move = ai.decideMove();
        if (move == null)
        {
            won = true;
        }
        else if (move[0] == -1)
        {
            inStalemate = false;
        }
        else
        {
            mainChessBoard.move(move[0], move[1], move[2], move[3]);
            ai.resetCurrentBoard(mainChessBoard);
            return true;
        }
        return false;
    }

    //handles palyer move input
    static int playerMoveInput()
    {
        int cy = moveCoords[0];
        int cx = moveCoords[1];
        int ny = moveCoords[2];
        int nx = moveCoords[3];
        if (cy == -1 || cx == -1 || ny == -1 || nx == -1)
        {
            return 2;
        }
        List<int[]> pm = checkIfPlayerCheck();
        if (pm == null)
        {
            return 3;
        }
        else if (pm[0][0] == -1)
        {
            return 4;
        }
        else
        {
            foreach (int[] i in pm)
            {
                if (i[0] == cy && i[1] == cx && i[2] == ny && i[3] == nx)
                {
                    if (checkPlayerMove(cy, cx, ny, nx))
                    {
                        mainChessBoard.move(cy, cx, ny, nx);
                        return 1;
                    }
                }
            }
            return 2;
        }
        return 3;
    }

    //checks if player is in check
    static List<int[]> checkIfPlayerCheck()
    {
        List<int[]> possibleMoves = new List<int[]>();
        Board b = new Board();
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                b.changeSquare(y, x, mainChessBoard.returnSquare(y, x));
            }
        }
        Node currentNode = new Node(0, 0, 0, 0, 0, true, b);
        currentNode.addToMoveList();
        if (currentNode.checkIfCheck() && currentNode.listOfPossibleMoves.Count == 0)
        {

        }
        else if (currentNode.listOfPossibleMoves.Count == 0)
        {
            int[] x = { -1 };
            possibleMoves.Add(x);
        }
        else
        {
            foreach (Node n in currentNode.listOfPossibleMoves)
            {
                int[] x = { n.lastY, n.lastX, n.newY, n.newX };
                possibleMoves.Add(x);
            }
        }
        return possibleMoves;
    }

    //checks if players move is possible
    static bool checkPlayerMove(int cy, int cx, int ny, int nx)
    {
        if (mainChessBoard.returnSquare(cy, cx) == null)
        {
            return false;
        }
        if (mainChessBoard.returnSquare(cy, cx).side != true)
        {
            return false;
        }
        List<int[]> movesList = mainChessBoard.returnSquare(cy, cx).possibleMoves(cy, cx, mainChessBoard.returnBoard());
        foreach (int[] a in movesList)
        {
            if (ny == a[0] && nx == a[1])
            {
                return true;
            }
        }
        return false;
    }

    static string positionCal(int p)
    {
        switch (p)
        {
            case 0:
                return "A";
            case 1:
                return "B";
            case 2:
                return "C";
            case 3:
                return "D";
            case 4:
                return "E";
            case 5:
                return "F";
            case 6:
                return "G";
            case 7:
                return "H";
        }
        return "";
    }
}
