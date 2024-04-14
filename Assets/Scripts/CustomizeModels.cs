using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeModels : MonoBehaviour
{
    public GameObject[] heads;// drag and drop all head models
    private int currentHead;// current index of the array that we want visible!

    private void Start()
    {
      //  Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        for (int i = 0; i < heads.Length; i++)
        {
            if (i == currentHead)
            {
                heads[i].SetActive(true);
            }
            else
            {
                heads[i].SetActive(false);
            }

        }
       
    }

   public void Unlock()
    {
      //  Cursor.lockState = CursorLockMode.Locked;
    }

    public void SwitchHeads()
    {
        if (currentHead == heads.Length - 1) { currentHead = 0; }

        else
        {
            currentHead++;

        }
    }
}



