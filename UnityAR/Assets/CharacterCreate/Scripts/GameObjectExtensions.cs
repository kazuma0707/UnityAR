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

    /// <summary>
    /// 深い階層まで子オブジェクトを名前で検索して GameObject 型で取得します
    /// </summary>
    /// <param name="self">GameObject 型のインスタンス</param>
    /// <param name="name">検索するオブジェクトの名前</param>
    /// <param name="includeInactive">非アクティブなオブジェクトも検索する場合 true</param>
    /// <returns>子オブジェクト</returns>
    public static GameObject FindDeep(
        this GameObject self,
        string name,
        bool includeInactive = false)
    {
        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (transform.name == name)
            {
                return transform.gameObject;
            }
        }
        return null;
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

    /// <summary>
    /// 深い階層まで子オブジェクトを名前で検索して GameObject 型で取得します
    /// </summary>
    /// <param name="self">GameObject 型のインスタンス</param>
    /// <param name="name">検索するオブジェクトの名前</param>
    /// <param name="includeInactive">非アクティブなオブジェクトも検索する場合 true</param>
    /// <returns>子オブジェクト</returns>
    public static GameObject FindDeep(
        this Component self,
        string name, bool
        includeInactive = false)
    {
        var children = self.GetComponentsInChildren<Transform>(includeInactive);
        foreach (var transform in children)
        {
            if (transform.name == name)
            {
                return transform.gameObject;
            }
        }
        return null;
    }
}