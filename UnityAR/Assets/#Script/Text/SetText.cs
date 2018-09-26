using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.AugmentedImage;
using UnityEngine.UI;
using EnumName;

/// <summary>
/// マーカを読み込んだ際にどこの
/// </summary>
public class SetText : MonoBehaviour {
    [SerializeField]
    private AugmentedImageExampleController _ImageController;
    [SerializeField, Header("表示するテキスト")]
    private Text _text;
    [SerializeField,Header("現在いる学科を表示するテキスト")]
    private Text _DepartmentText;
    //現在のテキストを変更する
    public int SetTextNumber { set; get; }

    // Use this for initialization
    void Start () {
        //ImageCheckオブジェクトの鮎徳
        _ImageController = GameObject.Find("ImageCheck").GetComponent<AugmentedImageExampleController>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (GameObject.Find("BordText"))
        {
            _text = GameObject.Find("BordText").GetComponent<Text>();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _DepartmentText.text = "ここはゲームサイエンス学科です";

        }

        //読み込んだ画像によってテキストの受け渡し
        UIViewText();
        PanelViewText();




    }
    void UIViewText()
    {
        this.SetTextNumber = this._ImageController.GetMarkerNumber;
        
        switch (SetTextNumber)
        {
            case DepartmentName.GAME:
                _DepartmentText.text = "ここはゲームサイエンス学科です";
                break;
            case DepartmentName.CG:
                _DepartmentText.text = "ここはCGスペシャリストです";
                break;
            case DepartmentName.WEB:
                _DepartmentText.text = "ここはWebデザイン学科です";
                break;
            case DepartmentName.CAD:
                _DepartmentText.text = "ここはCAD学科です";
                break;
                case DepartmentName.CYBER_SECURITY:
                _DepartmentText.text = "ここはサイバーセキュリティ学科です";
                break;
            case DepartmentName.ADVANCED_INFORMATION:
                _DepartmentText.text = "高度情報学科です";
                break;
            case DepartmentName.INFORMATION_PROCESSING:
                _DepartmentText.text = "情報処理学科です";
                break;
            default:
                return;
        }
    }
    void PanelViewText()
    {
        //パネルが生成されてなければリターン
        if (_text == null) return;
        this.SetTextNumber = this._ImageController.GetMarkerNumber;
        switch (this._ImageController.GetMarkerNumber)
        {
            case DepartmentName.GAME:
                this._text.text = "ここはゲームサイエンス学科です";
                break;
            case DepartmentName.CG:
                this._text.text = "ここはCGスペシャリストです";
                break;
            case DepartmentName.WEB:
                this._text.text = "ここはWebデザイン学科です";
                break;
            case DepartmentName.CAD:
                this._text.text = "ここはCAD学科です";
                break;
            case DepartmentName.ADVANCED_INFORMATION:
                this._text.text = "高度情報学科です";
                break;
            case DepartmentName.INFORMATION_PROCESSING:
                this._text.text = "情報処理学科です";
                break;
            default:
                return;
        }
    }
}
