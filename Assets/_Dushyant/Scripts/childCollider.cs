using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class childCollider : MonoBehaviour
{

    private void Awake()
    {
        AddCollider();
    }
    //List of All mesh renderers 
    MeshRenderer[] r;
    public void AddCollider()
    {
        r = transform.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer t in r)
        {
            if(t.GetComponent<MeshCollider>() == null)
            t.gameObject.AddComponent<MeshCollider>();
        }
    }
}
