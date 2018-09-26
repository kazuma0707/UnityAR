using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Buttonの親クラス
public class BaseButton : MonoBehaviour {
   public BaseButton button;
    public void OnClick()
    {
        if (button == null)
        {
            return;
        }
        button.OnClick(this.gameObject.name);
    }
    protected virtual void OnClick(string objectName)
    {
        // 呼ばれることはない
        Debug.Log("Base Button");
    }

}
