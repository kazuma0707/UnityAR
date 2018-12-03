using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RuntimePermission : MonoBehaviour {
    private string[] permission = { "android.permission.WRITE_EXTERNAL_STORAGE" ,
                                                 "android.permission.CAMERA" ,
                                                 "android.permission.WRITE_EXTERNAL_STORAGE" };
    private bool permissionRequested = false;
    private const int MAX_PERMISSION=3;
    void Start()
    {
        for (int i = 0; i < MAX_PERMISSION; i++)
        {

        
            if (!RuntimePermissionHelper.HasPermission(permission[i]))
            {
                if (RuntimePermissionHelper.ShouldShowRequestPermissionRationale(permission[i]))
                {
                    // パーミッションを要求する意味を説明するUIを表示
                }
                else
                {
                    // パーミッションをリクエスト
                    RuntimePermissionHelper.RequestPermission(new string[] { permission[i] });
                    permissionRequested = true;
                }
            }
        }
    }

    // パーミッションダイアログから戻ってきたときなどに呼ばれる
    void OnApplicationPause(bool pauseStatus)
    {
        // ポーズからの復帰時かつパーミッションリクエストの直後の場合
        if (!pauseStatus && permissionRequested)
        {
            for (int i = 0; i < MAX_PERMISSION; i++)
            {
                // パーミッションを持っているかどうか
                if (RuntimePermissionHelper.HasPermission(permission[i]))
                {
                    Debug.Log("パーミッションリクエスト成功!");
                    permissionRequested = false;
                }
            }
        }
    }
}
