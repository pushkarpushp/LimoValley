using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class InstantiatePlayers :Singleton<InstantiatePlayers>
{
    public GameObject parentText;
    public GameObject instnce;
    PhotonView photonView;
    private void Start()
    {
        photonView=GetComponent<PhotonView>();
    }
    public void ShowPlayersss()
    {
        photonView.RPC("ShowPlayer", RpcTarget.All);
    }
    [PunRPC]
    private void ShowPlayer()
    {
        //Destroy all gameOjects first to start new names instantiate. 
        for (int i = 0; i < parentText.transform.childCount; i++)
        {
            Destroy(parentText.transform.GetChild(i).gameObject);
        }
        //instantiate new name 
        foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (!p.IsLocal)
            {
                GameObject voice = Instantiate(instnce, parentText.transform);
                voice.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            }
        }
    }
}
