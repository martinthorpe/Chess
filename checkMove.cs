using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkMove : MonoBehaviour
{
    public void clicked()
    {
        if (GameMan.inGame)
        {
            GameMan.play();
        }
    }
}
