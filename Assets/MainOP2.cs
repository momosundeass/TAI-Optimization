using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class MainOP2 : MonoBehaviour
{
    public int SpheresMax = 10000;
    public int Speed = 500;
    private Transform[] spheres;
    private float[] deltaTimes;
    private int deltaTimeWriteIndex = 0;
    private int objPerFrame = 0;

    private readonly static Quaternion identity = Quaternion.identity;

    [SerializeField]
    private GameObject prefab = default;

    [SerializeField]
    private int frameSkip = 4;

    void Start()
    {
        spheres = new Transform[SpheresMax];
        deltaTimes = new float[frameSkip];
        objPerFrame = SpheresMax / frameSkip;

        for (int i = 0; i < SpheresMax; i++)
        {
            spheres[i] = Instantiate(prefab, GetSphereInitPosition(), identity).transform;
        }
    }

    void Update()
    {
        deltaTimes[deltaTimeWriteIndex] = Time.deltaTime;

        float delta = 0;
        for (int i = 0; i < frameSkip; i++)
        {
            delta += deltaTimes[i];
        }
        delta = delta * Speed;
        for (int i = objPerFrame * deltaTimeWriteIndex; i < objPerFrame * (deltaTimeWriteIndex + 1); i++)
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

        deltaTimeWriteIndex++;
        deltaTimeWriteIndex = deltaTimeWriteIndex >= frameSkip ? 0 : deltaTimeWriteIndex;
    }

    private Vector3 GetSphereInitPosition()
    {
        var position = Random.insideUnitSphere * 500;
        position.z += 500;
        return position;
    }
}
