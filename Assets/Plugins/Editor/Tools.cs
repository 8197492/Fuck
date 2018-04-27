using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tools
{
    [UnityEditor.MenuItem("Assets/RemoveHideFlags")]
    public static void RemoveHideFlags()
    {
        for (int i = 0; i < Selection.objects.Length; i++)
        {
            Selection.objects[i].hideFlags = HideFlags.None;
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
