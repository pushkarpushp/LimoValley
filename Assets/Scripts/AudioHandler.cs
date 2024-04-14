using System.Collections;
using System.Collections.Generic;
//using agora_gaming_rtc;
//using agora_utilities;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class AudioHandler : MonoBehaviourPunCallbacks
{

   // public VoiceChatManager voiceChatManager;
   // [SerializeField]
    //public string name;
    private PhotonView pv;
    bool AudioOn;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        name = PhotonNetwork.NickName;
    //    voiceChatManager = GameObject.FindGameObjectWithTag("VManager").GetComponent<VoiceChatManager>();
        // voiceChatManager.MuteAll();
    }


    private void OnTriggerStay(Collider other)
    {
        if (pv != null)
        {
            if (other.gameObject.tag == "Player" && pv.IsMine)
            {
           //     EnableNames(other.GetComponent<PhotonView>().Owner.NickName, UIManager.Instance.VoiceChatParent.transform);
                //  EnableNames(other.GetComponent<PhotonView>().Owner.NickName, UIManager.Instance.PersonalTextChatParent.transform);
            //    voiceChatManager.UnMuteRemoteClient(other.GetComponent<PhotonView>().Owner.NickName);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
           // DisableNames(other.GetComponent<PhotonView>().Owner.NickName, UIManager.Instance.VoiceChatParent.transform);
            //  DisableNames(other.GetComponent<PhotonView>().Owner.NickName,UIManager.Instance.PersonalTextChatParent.transform);
          //  voiceChatManager.MuteRemoteClient(other.GetComponent<PhotonView>().Owner.NickName);
            Debug.Log("muted");
        }

    }
    private void EnableNames(string namePlayer, Transform parent)
    {
        int totalnames = parent.childCount;
        // Debug.Log("total"+totalnames);
        int i = 0;
        while (i < totalnames)
        {
            if (parent.GetChild(i).transform.GetChild(0).GetComponent<Text>().text == namePlayer)
            {
                // Debug.Log(i+"enableeeeeeeeeeeeee"+namePlayer);
                parent.GetChild(i).gameObject.SetActive(true);
                break;
            }
            i++;
            //Debug.Log(i);
        }
    }
    private void DisableNames(string namePlayer, Transform parent)
    {
        int totalnames = parent.childCount;
        int i = 0;
        while (i < totalnames)
        {
            if (parent.GetChild(i).transform.GetChild(0).GetComponent<Text>().text == namePlayer)
            {
                //  Debug.Log(i+"disableeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
                parent.GetChild(i).gameObject.SetActive(false);
                break;
            }
            i++;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        photonView.RPC("DestroyPlayerName", RpcTarget.Others, otherPlayer.NickName);
    }
    private void DestroyPlayerName(string name)
    {
        Debug.Log(name);
        int i = 0;
        while (i < UIManager.Instance.PersonalTextChatParent.transform.childCount)
        {
            if (UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text == name)
            {
                Destroy(UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).gameObject);
                break;
            }
            i++;
        }
        i = 0;
        while (i < UIManager.Instance.VoiceChatParent.transform.childCount)
        {
            if (UIManager.Instance.VoiceChatParent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text == name)
            {
                Destroy(UIManager.Instance.VoiceChatParent.transform.transform.GetChild(i).gameObject);
                break;
            }
            i++;
        }
    }
}
