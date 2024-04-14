using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GuideHelper : MonoBehaviourPun
{
    public Transform[] waypoints;
    int playersInside = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            playersInside++;
            if (playersInside > 1) return;
            PhotonView view = other.GetComponent<PhotonView>();
            if (view.IsMine)
            {
                GameObject guide = PhotonNetwork.Instantiate("Guide", transform.position, transform.rotation);
                Guide g = guide.GetComponent<Guide>();
                g.waypoints = waypoints;
                g.playerTransform = other.transform;
            }
        }
        
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
            playersInside--;
    }
}
