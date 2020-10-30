using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    private float fpsDeltaTime;
    private bool isInit = false;
    GUIStyle style;
    Rect rect;

    void Update()
    {
        fpsDeltaTime += (Time.unscaledDeltaTime - fpsDeltaTime) * 0.1f;
    }

    private void OnGUI()
    {
        if (!isInit)
        {
            int w = Screen.width, h = Screen.height;
            style = new GUIStyle();
            rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperLeft;
            style.fontSize = h * 3 / 100;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.5f, 1.0f);
            isInit = true;
        }
        float msec = fpsDeltaTime * 1000.0f;
        float fps = 1.0f / fpsDeltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
