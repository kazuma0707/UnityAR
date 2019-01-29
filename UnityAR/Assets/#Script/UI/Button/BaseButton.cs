using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Buttonの親クラス
public class BaseButton : MonoBehaviour {
   public BaseButton button;
    private SetText setText;
    private void Start()
    {
        setText = GameObject.Find("SetText").GetComponent<SetText>();
    }
    public void OnClick()
    {
        if (button == null)
        {
            return;
        }
        button.OnClick(this.gameObject.name);
        //学科のボタンが押された時
        setText.UIViewText();
    }
    protected virtual void OnClick(string objectName)
    {
        // 呼ばれることはない
        Debug.Log("Base Button");
    }

}
