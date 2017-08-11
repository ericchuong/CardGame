using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {
    public void triggerMenuBehavior(int i)
    {
        switch (i)
        {
            default:
            case (0):
                // Reload Game Scene button
                SceneManager.LoadScene("GameScene");
                break;
        }
    }
}
