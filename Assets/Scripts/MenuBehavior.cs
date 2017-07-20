using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour {
    public void triggerMenuBehavior(int i)
    {
        switch(i)
        {
            default:
            case (0):
                // Start button
                SceneManager.LoadScene("GameScene");
                break;
            case (1):
                // Quit button
                Application.Quit();
                break;
        }
    }

}
