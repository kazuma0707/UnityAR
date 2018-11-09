using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimePermissionHelper  {
    private RuntimePermissionHelper() { }

    // 実行中のActivityインスタンスを取得する
    private static AndroidJavaObject GetActivity()
    {
        using (var UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            return UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    // Android M以上かどうか
    private static bool IsAndroidMOrGreater()
    {
        using (var VERSION = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return VERSION.GetStatic<int>("SDK_INT") >= 23;
        }
    }

    // パーミッションを持っているかどうかを調べる
    public static bool HasPermission(string permission)
    {
        if (IsAndroidMOrGreater())
        {
            using (var activity = GetActivity())
            {
                return activity.Call<int>("checkSelfPermission", permission) == 0;
            }
        }

        return true;
    }

    // パーミッションが必要であることを説明するUIを出す必要があるか
    public static bool ShouldShowRequestPermissionRationale(string permission)
    {
        if (IsAndroidMOrGreater())
        {
            using (var activity = GetActivity())
            {
                return activity.Call<bool>("shouldShowRequestPermissionRationale", permission);
            }
        }

        return false;
    }

    // パーミッション許可ダイアログを表示する
    public static void RequestPermission(string[] permissiions)
    {
        if (IsAndroidMOrGreater())
        {
            using (var activity = GetActivity())
            {
                activity.Call("requestPermissions", permissiions, 0);
            }
        }
    }
}
