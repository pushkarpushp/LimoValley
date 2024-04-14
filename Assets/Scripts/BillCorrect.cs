using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BillCorrect : MonoBehaviour
{
    GameObject[] Follow;
    public PhotonView view;



    private void FixedUpdate()
    {


        Follow = GameObject.FindGameObjectsWithTag("Name");

        foreach (GameObject C in Follow)
        {

            //C.SetActive(false);
             C.GetComponent<Billboard>().enabled = false;


            if (view.IsMine)
            {
                if (C.name == this.name)
                {

                  //  C.SetActive(true);
                    C.GetComponent<Billboard>().enabled = true;
                    //  this.GetComponent<CinemachineVirtualCamera>().enabled = true;
                }


            }

        }

    }

}
