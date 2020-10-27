using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MainOP1 : MonoBehaviour
{
    public int SpheresMax = 10000;
    public int Speed = 500;
    private float fpsDeltaTime;
    private Transform[] spheres;

    private readonly static Quaternion identity = Quaternion.identity;

    [SerializeField]
    private GameObject prefab = default;

    void Start()
    {
        spheres = new Transform[SpheresMax];
        for (int i = 0; i < SpheresMax; i++)
        {
            spheres[i] = Instantiate(prefab, GetSphereInitPosition(), identity).transform;
        }
    }

    void Update()
    {
        var delta = Time.deltaTime * Speed;
        for (int i = 0; i < SpheresMax; i++)
        {
            var st = spheres[i];
            Vector3 p = st.position;
            p.z -= delta;
            if (p.z < 0)
            {
                st.position = GetSphereInitPosition();
            }
            else
            {
                st.position = p;
            }
        }

        fpsDeltaTime += (Time.unscaledDeltaTime - fpsDeltaTime) * 0.1f;
    }

    private Vector3 GetSphereInitPosition()
    {
        var position = Random.insideUnitSphere * 500;
        position.z += 500;
        return position;
    }

    private void OnGUI()
    {
        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 3 / 100;
        style.normal.textColor = new Color(1.0f, 1.0f, 1.5f, 1.0f);
        float msec = fpsDeltaTime * 1000.0f;
        float fps = 1.0f / fpsDeltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }
}
