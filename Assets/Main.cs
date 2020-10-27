
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class Main : MonoBehaviour
{
    public int SpheresMax = 10000;
    public int Speed = 500;
    private float fpsDeltaTime;
    private List<GameObject> spheres;
    void Start()
    {
        spheres = new List<GameObject>();
    }
    void Update()
    {
        fpsDeltaTime += (Time.unscaledDeltaTime - fpsDeltaTime) * 0.1f;
        SpawnSpheres();
        List<GameObject> toDestroy = new List<GameObject>();
        foreach (var sphere in spheres)
        {
            var p = sphere.transform.position;
            sphere.transform.SetPositionAndRotation(new Vector3(p.x, p.y, p.z - Time.deltaTime * Speed), Quaternion.identity);
            if (p.z < 0)
            {
                toDestroy.Add(sphere);
            }
        }
        foreach (var go in toDestroy)
        {
            var found = spheres.Find(o => o == go);
            spheres.Remove(found);
            Destroy(found);
        }
    }
    void SpawnSpheres()
    {
        while (spheres.Count < SpheresMax)
        {
            var s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            s.transform.position = Random.insideUnitSphere * 500 + new Vector3(0, 0, 500);
            spheres.Add(s);
        }
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
