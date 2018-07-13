using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public static class ExampleClass  {

    private static readonly Color mDisabledColor = new Color(1, 1, 1, 0.5f);

    private const int WIDTH = 16;
    private const int HEIGHT = 16;

    [InitializeOnLoadMethod]
    private static void Example()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnGUI;
    }

    private static void OnGUI(int instanceID, Rect selectionRect)
    {
        var go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go == null)
        {
            return;
        }

        var pos = selectionRect;
        pos.x = pos.xMax - WIDTH;
        pos.width = WIDTH;
        pos.height = HEIGHT;

        var components = go
            .GetComponents<Component>()
            .Where(c => c != null)
            .Where(c => !(c is Transform))
            .Reverse();

        var current = Event.current;

        foreach (var c in components)
        {
            Texture image = AssetPreview.GetMiniThumbnail(c);

            if (image == null && c is MonoBehaviour)
            {
                var ms = MonoScript.FromMonoBehaviour(c as MonoBehaviour);
                var path = AssetDatabase.GetAssetPath(ms);
                image = AssetDatabase.GetCachedIcon(path);
            }

            if (image == null)
            {
                continue;
            }

            if (current.type == EventType.MouseDown &&
                 pos.Contains(current.mousePosition))
            {
                c.SetEnable(!c.IsEnabled());
            }

            var color = GUI.color;
            GUI.color = c.IsEnabled() ? Color.white : mDisabledColor;
            GUI.DrawTexture(pos, image, ScaleMode.ScaleToFit);
            GUI.color = color;
            pos.x -= pos.width;
        }
    }

    public static bool IsEnabled(this Component self)
    {
        if (self == null)
        {
            return true;
        }

        var type = self.GetType();
        var property = type.GetProperty("enabled", typeof(bool));

        if (property == null)
        {
            return true;
        }

        return (bool)property.GetValue(self, null);
    }

    public static void SetEnable(this Component self, bool isEnabled)
    {
        if (self == null)
        {
            return;
        }

        var type = self.GetType();
        var property = type.GetProperty("enabled", typeof(bool));

        if (property == null)
        {
            return;
        }

        property.SetValue(self, isEnabled, null);
    }
}
