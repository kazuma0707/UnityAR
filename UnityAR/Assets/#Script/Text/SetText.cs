using System.Collections;
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
    public VideoPlayer[] _video;
    public VideoClip[] _videoClip;


    // Use this for initialization
    void Start()
    {
        //ImageCheckオブジェクトの鮎徳
        //   _ImageController = GameObject.Find("ImageCheck").GetComponent<AugmentedImageExampleController>();
        //_video = new VideoPlayer[_videoClip.Length];
        //for(int i=0;i<_videoClip.Length;i++)
        //{
        //    _video[i]= GameObject.Find("VideoPlane").GetComponent<VideoPlayer>();
        //}
       
         _video[0].clip = _videoClip[0];
        _video[1].clip = _videoClip[1];
        _video[2].clip = _videoClip[2];
        _video[3].clip = _videoClip[3];
        _video[4].clip = _videoClip[4];
        _video[5].clip = _videoClip[5];
        _video[6].clip = _videoClip[6];


    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("BordText"))
        {
            BordText = GameObject.Find("BordText").GetComponent<Text>();
        }

        //読み込んだ画像によってテキストの受け渡し
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
                PlayVedeo(DepartmentName.GAME);
                break;
            case DepartmentName.CG:
                _DepartmentText.text = "ここはCGスペシャリスト学科です";
                PlayVedeo(DepartmentName.CG);
                break;
            case DepartmentName.WEB:
                _DepartmentText.text = "ここはWebデザイン学科です";
                PlayVedeo(DepartmentName.WEB );
                break;
            case DepartmentName.CAD:
                _DepartmentText.text = "ここはCAD学科です";
               PlayVedeo(DepartmentName.CAD);
                break;
            case DepartmentName.CYBER_SECURITY:
                _DepartmentText.text = "ここはサイバーセキュリティ学科です";        
                PlayVedeo(DepartmentName.CYBER_SECURITY);
                break;
            case DepartmentName.ADVANCED_INFORMATION:
                _DepartmentText.text = "ここは高度情報学科です";               
                PlayVedeo(DepartmentName.ADVANCED_INFORMATION);
                break;
            case DepartmentName.INFORMATION_PROCESSING:
                _DepartmentText.text = "ここは情報処理学科です";
                PlayVedeo(DepartmentName.INFORMATION_PROCESSING);
                break;
            default:
                return;
        }
        //AugmentedImageExampleController.isLoadImage = false;
    }
    void PlayVedeo(int videoName)
    {
        for(int i=0;i<_videoClip.Length;i++)
        {
            _video[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        _video[videoName-1].gameObject.GetComponent<MeshRenderer>().enabled = true;

        _video[videoName-1].Stop();
        _video[videoName-1].Prepare();
        if (_video[videoName-1].isPrepared)
        {
            return;
        }


        _video[videoName-1].Play();
        if (!_video[videoName-1].isPlaying)
        {
            return;
        }

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
