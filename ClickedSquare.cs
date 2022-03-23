using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedSquare : MonoBehaviour
{
    public int y;
    public int x;

    public void clicked()
    {
        GameMan.updatingMove(y, x);
    }
}