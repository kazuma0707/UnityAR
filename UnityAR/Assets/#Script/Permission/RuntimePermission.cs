using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RuntimePermission : MonoBehaviour {
    private string permission = "android.permission.WRITE_EXTERNAL_STORAGE";
    private bool permissionRequested = false;

    void Start()
    {
        if (!RuntimePermissionHelper.HasPermission(permission))
        {
            if (RuntimePermissionHelper.ShouldShowRequestPermissionRationale(permission))
            {
                // パーミッションを要求する意味を説明するUIを表示
            }
            else
            {
                // パーミッションをリクエスト
                RuntimePermissionHelper.RequestPermission(new string[] { permission });
                permissionRequested = true;
            }
        }
    }

    // パーミッションダイアログから戻ってきたときなどに呼ばれる
    void OnApplicationPause(bool pauseStatus)
    {
        // ポーズからの復帰時かつパーミッションリクエストの直後の場合
        if (!pauseStatus && permissionRequested)
        {
            // パーミッションを持っているかどうか
            if (RuntimePermissionHelper.HasPermission(permission))
            {
                Debug.Log("パーミッションリクエスト成功!");
                permissionRequested = false;
            }
        }
    }
}
