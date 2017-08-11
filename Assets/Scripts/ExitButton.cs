using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour {
    public void triggerMenuBehavior(int i)
    {
        switch (i)
        {
            default:
            case (0):
                // Exit to Main Menu button
                SceneManager.LoadScene("MainMenu");
                break;
        }
    }
}
