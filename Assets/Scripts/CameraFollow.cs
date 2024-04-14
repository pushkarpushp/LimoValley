using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviourPun
{
    public PhotonView view;
    private void Start()
    {
        if (!view.IsMine)
        {
            GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
    }


}
