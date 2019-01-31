using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using ConstantName;

/// <summary>
/// マーカを読み込んだ際にどこの
/// </summary>
public class SetText : MonoBehaviour {
    [SerializeField, Header("表示するテキスト")]
    private Text BordText;
    [SerializeField, Header("現在いる学科を表示するテキスト")]
    private TextMeshProUGUI _DepartmentText;
    //現在のテキストを変更する
    public int SetTextNumber {  set; get; }
    public ButtonController _ButtonController;
    //ロック画像
    [SerializeField, Header("ロック画像")]
    public VideoPlayer[] _video;
    public VideoClip[] _videoClip;


    // Use this for initialization
    void Start()
    {
        //それぞれのVideoPlayerに各学科の動画を入れる
        //Unity2018からは複数のVideoPlayerを作る必要はないらしい
       for(int i=0;i<_videoClip.Length;i++)
        {
            if(_video[i].clip!=_videoClip[i])
            {
                _video[i].clip = _videoClip[i];
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("BordText"))
        {
            BordText = GameObject.Find("BordText").GetComponent<Text>();
        }
    }
    public void UIViewText()
    {
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
}
