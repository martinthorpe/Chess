using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class displayPieces : MonoBehaviour
{
    public GameObject self;
    public Image TempSprite;
    public Sprite Empty;
    public Sprite WhiteKing;
    public Sprite WhiteQueen;
    public Sprite WhiteBishop;
    public Sprite WhiteKnight;
    public Sprite WhiteRook;
    public Sprite WhitePawn;
    public Sprite BlackKing;
    public Sprite BlackQueen;
    public Sprite BlackBishop;
    public Sprite BlackKnight;
    public Sprite BlackRook;
    public Sprite BlackPawn;
    public int y;
    public int x;

    //changes square icon
    void Update()
    {
        if (GameMan.mainChessBoard.returnSquare(y, x) != null)
        {
            switch (GameMan.mainChessBoard.returnSquare(y, x).look)
            {
                case 'K':
                    TempSprite.sprite = WhiteKing;
                    self.SetActive(true);
                    break;
                case 'Q':
                    TempSprite.sprite = WhiteQueen;
                    self.SetActive(true);
                    break;
                case 'B':
                    TempSprite.sprite = WhiteBishop;
                    self.SetActive(true);
                    break;
                case 'H':
                    TempSprite.sprite = WhiteKnight;
                    self.SetActive(true);
                    break;
                case 'R':
                    TempSprite.sprite = WhiteRook;
                    self.SetActive(true);
                    break;
                case 'P':
                    TempSprite.sprite = WhitePawn;
                    self.SetActive(true);
                    break;
                case 'k':
                    TempSprite.sprite = BlackKing;
                    self.SetActive(true);
                    break;
                case 'q':
                    TempSprite.sprite = BlackQueen;
                    self.SetActive(true);
                    break;
                case 'b':
                    TempSprite.sprite = BlackBishop;
                    self.SetActive(true);
                    break;
                case 'h':
                    TempSprite.sprite = BlackKnight;
                    self.SetActive(true);
                    break;
                case 'r':
                    TempSprite.sprite = BlackRook;
                    self.SetActive(true);
                    break;
                case 'p':
                    TempSprite.sprite = BlackPawn;
                    self.SetActive(true);
                    break;
                default:
                    self.SetActive(false);
                    break;
            }
        }
        else
        {
            TempSprite.sprite = Empty;
        }
    }
}
