using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setRoomsActive : MonoBehaviour
{
    public GameObject[] rooms_;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ActiveRooms",3.0f);
    }


    void ActiveRooms()
    {
        foreach (GameObject t in rooms_)
        {
            t.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
