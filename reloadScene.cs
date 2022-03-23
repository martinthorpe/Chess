using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reloadScene : MonoBehaviour
{
    public void clicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
