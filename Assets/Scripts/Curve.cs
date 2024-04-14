using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour
{

    public int size = 10;
    public Transform p1;
    public Transform p2;
    public float Roundness;

    public LineRenderer lr;

    private Vector3 v1;
    private Vector3 v2;

    private Vector3 v3;
    private Vector3 v4;

    private Vector3[] points;
    // Start is called before the first frame update
    void Start()
    {
        points = new Vector3[size+1];
        lr.positionCount = size + 1;
    }

    // Update is called once per frame
    void Update()
    {
        v1 = p1.position;
        v4 = p2.position;
        v2 = p1.position + p1.forward*Roundness;
        v3 = p2.position + p2.forward*Roundness;

        for (int i = 0; i < size; i++)
        {
            float t = ((float)i) / (float)size;
            float t1 = (1f - t) * (1f - t) * (1f - t);
            float t2 = 3 * t * (1f - t) * (1f - t);
            float t3 = 3 * t * t * (1f - t);
            float t4 = t * t * t;
            points[i] = v1 * t1 + v2 * t2 + v3 * t3 + v4 * t4;
        }
        points[0] = v1;
        points[size] = v4;

        lr.SetPositions(points);

        Debug.DrawLine(v1, v2);
        Debug.DrawLine(v2, v3);
        Debug.DrawLine(v3, v4);
    }
}
