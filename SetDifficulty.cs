using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetDifficulty : MonoBehaviour
{
    public GameObject panel;
    public int level;

    //sets the difficulty
    public void clicked()
    {
        panel.SetActive(false);
        GameMan.difficulty = level;
        GameMan.inGame = true;
        GameMan.mainChessBoard = new Board();
        GameMan.mainChessBoard.makeBoard(GameMan.difficulty);
        GameMan.ai = new Computer(GameMan.difficulty, GameMan.mainChessBoard);
    }
}
