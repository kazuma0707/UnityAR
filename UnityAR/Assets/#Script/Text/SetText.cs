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
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(this._ImageController.GetMarkerNumber);
        switch(this._ImageController.GetMarkerNumber)
        {
            case 1:
                this._text.text = "ここはゲームサイエンス学科です";
                break;
            case 2:
                this._text.text = "ここはCGスペシャリストです";
                break;
            case 3:
                this._text.text = "ここはWebデザイン学科です";

                break;
            case 4:
                this._text.text = "ここはCAD学科です";

                break;
            case 5:
                this._text.text = "高度情報学科です";

                break;
            case 6:
                this._text.text = "情報処理学科です";

                break;
            case 7:
                this._text.text = "ここはゲームサイエンス学科です";
    
                break;
        }
		
	}
}
