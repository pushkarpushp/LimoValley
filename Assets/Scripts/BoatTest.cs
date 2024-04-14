using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.Demo.SlotRacer.Utils;
public class BoatTest : MonoBehaviour
{
    public BezierCurve curve;
    [Range(0f, 1f)]
    public float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = curve.GetPoint(t);
        transform.rotation = Quaternion.LookRotation(curve.GetDirection(t));
    }
}
