using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore.Examples.AugmentedImage;
using UnityEngine.UI;

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

        }

        //読み込んだ画像によってテキストの受け渡し
        switch (this._ImageController.GetMarkerNumber)
            {
                case 1:
                _DepartmentText.text = "ここはゲームサイエンス学科です";
                this._text.text = "ここはゲームサイエンス学科です";
                break;
                case 2:
                _DepartmentText.text = "ここはCGスペシャリストです";
                this._text.text = "ここはCGスペシャリストです";
                break;
                case 3:
                _DepartmentText.text = "ここはWebデザイン学科です";
                this._text.text = "ここはWebデザイン学科です";
                break;
                case 4:
                _DepartmentText.text = "ここはCAD学科です";
                this._text.text = "ここはCAD学科です";
                break;
                case 5:
                _DepartmentText.text = "高度情報学科です";
                this._text.text = "高度情報学科です";
                break;
                case 6:
                _DepartmentText.text = "情報処理学科です";
                this._text.text = "情報処理学科です";
                break;
                 default:
                return;
            }

    }
}
