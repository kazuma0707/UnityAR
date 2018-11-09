using System.Linq;
using UnityEngine;

public static class GameObjectExtensions
{
    /// <summary>
    /// すべての子オブジェクトを返します
    /// </summary>
    /// <param name="self">GameObject 型のインスタンス</param>
    /// <param name="includeInactive">非アクティブなオブジェクトも取得する場合 true</param>
    /// <returns>すべての子オブジェクトを管理する配列</returns>
    public static GameObject[] GetChildren(
        this GameObject self,
        bool includeInactive = false)
    {
        return self.GetComponentsInChildren<Transform>(includeInactive)
            .Where(c => c != self.transform)
            .Select(c => c.gameObject)
            .ToArray();
    }

    /// <summary>
    /// コンポーネントを削除します
    /// </summary>
    public static void RemoveComponent<T>(this GameObject self) where T : Component
    {
        GameObject.Destroy(self.GetComponent<T>());
    }
}

public static class ComponentExtensions
{
    /// <summary>
    /// すべての子オブジェクトを返します
    /// </summary>
    /// <param name="self">Component 型のインスタンス</param>
    /// <param name="includeInactive">非アクティブなオブジェクトも取得する場合 true</param>
    /// <returns>すべての子オブジェクトを管理する配列</returns>
    public static GameObject[] GetChildren(
        this Component self,
        bool includeInactive = false)
    {
        return self.GetComponentsInChildren<Transform>(includeInactive)
            .Where(c => c != self.transform)
            .Select(c => c.gameObject)
            .ToArray();
    }

    /// <summary>
    /// コンポーネントを削除します
    /// </summary>
    public static void RemoveComponent<T>(this Component self) where T : Component
    {
        GameObject.Destroy(self.GetComponent<T>());
    }
}