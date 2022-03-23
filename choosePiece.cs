using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choosePiece : MonoBehaviour
{
    public string p;

    public void clicked()
    {
        GameMan.chosenP = p;
    }
}
