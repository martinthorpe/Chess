using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertBoxDisplay : MonoBehaviour
{
    public Text alertText;

    // Update is called once per frame
    void Update()
    {
        if (GameMan.cantMove)
        {
            alertText.text = "Can't make that move";
        }
        if (GameMan.won)
        {
            alertText.text = "You Win";
        }
        if (GameMan.lost)
        {
            alertText.text = "You Lost";
        }
        if (GameMan.inStalemate)
        {
            alertText.text = "Stalemate";
        }
        if (GameMan.choiceP)
        {
            alertText.text = "Choose new piece";
        }
        if (!GameMan.cantMove && !GameMan.won && !GameMan.lost && !GameMan.inStalemate && !GameMan.choiceP)
        {
            alertText.text = "ALERT BOX";
        }
    }
}
