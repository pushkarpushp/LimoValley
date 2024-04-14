using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
//using Byn.Awrtc;
//using Byn.Awrtc.Unity;
//using Byn.Unity.Examples;
using System.Security.Cryptography;

public class VoiceChatInstance : MonoBehaviourPun
{
    public Text name;
   // public string nickname_Photon;
    public GameObject UnmuteBuuton,mutebutton;
    public string GroupName;
    public bool muteStatus;
    public string MyroomName;

    

    public void MuteRemotePlayer()
    {
 
     
        MuteUnmuteFunction(true);
    }
    public void UnMuteRemotePlayer()
    {
    
      
        MuteUnmuteFunction(false);
    }

    public void SetMicSprite()
    {
        mutebutton.SetActive(!muteStatus);
        UnmuteBuuton.SetActive(muteStatus);


        if(PlayerPrefs.GetString("RoomNo").Contains(MyroomName))
        {
            gameObject.SetActive(true);

        }
        else
        {
            gameObject.SetActive(false);
        }
        if(MyroomName=="")

        {
            gameObject.SetActive(false);
        }

    }





    public void MuteUnmuteFunction(bool mic)
    {

        if (PlayerPrefs.GetString("UserRole").Contains("cohost") || PlayerPrefs.GetString("UserRole").Contains("host"))
        {
            GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject playr in playersInGame)
            {
                if (playr.GetComponent<PhotonView>().Owner.NickName.Contains(name.text))
                {
                    playr.GetComponent<PhotonView>().RPC("SetMicStatusForPlayer", RpcTarget.AllBuffered, mic);
                   
                    //if(playr.GetComponent<Player_WebRTC>().playerRoll.Contains("host")|| playr.GetComponent<Player_WebRTC>().playerRoll.Contains("cohost"))
                    //playr.GetComponent<PhotonView>().RPC("MutedByHost_Cohost", RpcTarget.AllBuffered,false);
                    //else
                    playr.GetComponent<PhotonView>().RPC("MutedByHost_Cohost", RpcTarget.AllBuffered, mic);
                    playr.GetComponent<PhotonView>().RPC("UpdateVoiceChatList", RpcTarget.AllBuffered);

                }
            }
        }
    }


    


}
