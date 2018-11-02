using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager: MonoBehaviour {

    public void startGame()
    {
        //  シーン切り替え
        SceneManager.LoadScene("Play");
    }

    public void exitGame()
    {
        //  シーン切り替え
        SceneManager.LoadScene("CharCreate");
    }

}
