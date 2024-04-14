using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class testingpur : MonoBehaviour
{
    public CameraFollow cam;
    // Start is called before the first frame update
   public void eeeStart()
    {
     GameObject p=   PhotonNetwork.Instantiate("1", transform.position, Quaternion.identity);
        cam.view = p.GetComponent<PhotonView>();
    }

}
