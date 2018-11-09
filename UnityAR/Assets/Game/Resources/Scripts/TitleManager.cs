using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager: MonoBehaviour {

    //  シーンのロードが複数行われるのの防止
    bool isLoad = false;
    //  FadeObject
    GameObject fadeObj;

    //  ボタン
    [SerializeField]
    Button startButton;
    [SerializeField]
    Button endButton;


    private void Start()
    {
        fadeObj = GameObject.FindGameObjectWithTag("FadeObj");
    }

    private void Update()
    {
        if(fadeObj.GetComponent<FadeManager>().GetFadeFlag() == true)
        {
            startButton.interactable = false;
            endButton.interactable = false;
        }
        else
        {
            startButton.interactable = true;
            endButton.interactable = true;
        }
    }

    public void startGame()
    {
        if (!isLoad)
        {
            //  シーン切り替え
            //SceneManager.LoadScene("Play");
            FadeManager.Instance.LoadScene("Play", 2.0f);
        }
        isLoad = true;
    }

    public void exitGame()
    {
        //  シーン切り替え
        SceneManager.LoadScene("CharCreate");
    }

}
