using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteScript : MonoBehaviour {

    public Text Qtext;
    public bool SetFlag
    {
        set
        {
            a_flag = value;
        }
    }
    public float SetColor
    {
        set
        {
            a_color = value;
        }
    }
    private bool a_flag;
    private float a_color;
    // Use this for initialization
    void Start()
    {
        a_flag = false;
        a_color = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックでa_flagをTrueにする

        //a_flagがtrueの間実行する
        if (a_flag)
        {
            //テキストの透明度を変更する
            Qtext.color = new Color(0, 0, 0, a_color);
            a_color -= Time.deltaTime;
            //透明度が0になったら終了する。
            if (a_color < 0)
            {
                a_color = 0;
                a_flag = false;
            }
        }
    }
}
