using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSquareText : MonoBehaviour
{
    public Text SText;
    public int which;

    // Update is called once per frame
    void Update()
    {
        if (which == 1)
        {
            SText.text = GameMan.movingS;
        }
        else if (which == 2)
        {
            SText.text = GameMan.nextS;
        }
    }
}
