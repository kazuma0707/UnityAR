﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GoogleARCore.Examples.AugmentedImage;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using ConstantName;

/// <summary>
/// マーカを読み込んだ際にどこの
/// </summary>
public class SetText : MonoBehaviour {
    //[SerializeField]
    //private AugmentedImageExampleController _ImageController;
    [SerializeField, Header("表示するテキスト")]
    private Text BordText;
    [SerializeField, Header("現在いる学科を表示するテキスト")]
    private TextMeshProUGUI _DepartmentText;
    //現在のテキストを変更する
    public int SetTextNumber { set; get; }
    public ButtonController _ButtonController;
    //ロック画像
    [SerializeField, Header("ロック画像")]
   // private Image[] LockImage;
    public VideoPlayer _video;
    public VideoClip[] _videoClip;


    // Use this for initialization
    void Start()
    {
        //ImageCheckオブジェクトの鮎徳
     //   _ImageController = GameObject.Find("ImageCheck").GetComponent<AugmentedImageExampleController>();

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("BordText"))
        {
            BordText = GameObject.Find("BordText").GetComponent<Text>();
        }
        if (GameObject.Find("VideoPlane"))
        {
            _video = GameObject.Find("VideoPlane").GetComponent<VideoPlayer>();
   
            //_video.Stop();
        }

        //読み込んだ画像によってテキストの受け渡し
        UIViewText();
        PanelViewText();

    }
    public void UIViewText()
    {
        //if (AugmentedImageExampleController.isLoadImage)
        //{
        //    this.SetTextNumber = this._ImageController.GetMarkerNumber;
        //}


        switch (SetTextNumber)
        {
            case DepartmentName.GAME:
                _DepartmentText.text = "ここはゲームサイエンス学科です";
                _video.clip = _videoClip[DepartmentName.GAME - 1];

               // this.LockImage[DepartmentName.GAME-1].enabled = false;

                break;
            case DepartmentName.CG:
                _DepartmentText.text = "ここはCGスペシャリスト学科です";

                _video.clip = _videoClip[DepartmentName.CG - 1];
           //     this.LockImage[DepartmentName.CG-1].enabled = false;

                break;
            case DepartmentName.WEB:
                _DepartmentText.text = "ここはWebデザイン学科です";
                _video.clip = _videoClip[DepartmentName.WEB - 1];

             //  this.LockImage[DepartmentName.WEB-1].enabled = false;

                break;
            case DepartmentName.CAD:
                _DepartmentText.text = "ここはCAD学科です";
                _video.clip = _videoClip[DepartmentName.CAD - 1];

               // this.LockImage[DepartmentName.CAD-1].enabled = false;

                break;
            case DepartmentName.CYBER_SECURITY:
                _DepartmentText.text = "ここはサイバーセキュリティ学科です";
                _video.clip = _videoClip[DepartmentName.CYBER_SECURITY - 1];


             //   this.LockImage[DepartmentName.CYBER_SECURITY-1].enabled = false;

                break;
            case DepartmentName.ADVANCED_INFORMATION:
                _DepartmentText.text = "ここは高度情報学科です";
                _video.clip = _videoClip[DepartmentName.CYBER_SECURITY - 1];


                //this.LockImage[DepartmentName.ADVANCED_INFORMATION-1].enabled = false;

                break;
            case DepartmentName.INFORMATION_PROCESSING:
                _DepartmentText.text = "ここは情報処理学科です";
                _video.clip = _videoClip[DepartmentName.CYBER_SECURITY - 1];

              //  this.LockImage[DepartmentName.INFORMATION_PROCESSING-1].enabled = false;

                break;
            default:
                return;
        }


        //AugmentedImageExampleController.isLoadImage = false;
    }
    void PanelViewText()
    {
        //パネルが生成されてなければリターン
        if (BordText == null) return;
        //if (AugmentedImageExampleController.isLoadImage)
        //{
        //    this.SetTextNumber = this._ImageController.GetMarkerNumber;
        //}
        switch (this.SetTextNumber)
        {
            case DepartmentName.GAME:
                this.BordText.text = "ここはゲームサイエンス学科です";
                break;
            case DepartmentName.CG:
                this.BordText.text = "ここはCGスペシャリスト学科です";
                break;
            case DepartmentName.WEB:
                this.BordText.text = "ここはWebデザイン学科です";
                break;
            case DepartmentName.CAD:
                this.BordText.text = "ここはCAD学科です";
                break;
            case DepartmentName.CYBER_SECURITY:
                _DepartmentText.text = "ここはサイバーセキュリティ学科です";
                break;
            case DepartmentName.ADVANCED_INFORMATION:
                this.BordText.text = "高度情報学科です";
                break;
            case DepartmentName.INFORMATION_PROCESSING:
                this.BordText.text = "情報処理学科です";
                break;
            default:
                return;
        }
        //AugmentedImageExampleController.isLoadImage = false;

    }
}
