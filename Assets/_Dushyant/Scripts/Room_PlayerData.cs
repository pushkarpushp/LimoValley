using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Room_PlayerData : MonoBehaviour
{

    public List<string> PlayerInRoom;
    // Start is called before the first frame update
    void Start()
    {
       
        //instantiate new name 

        //  if (!_myPhoton.IsLocal)
        //{
       
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            PlayerInRoom.Add(other.gameObject.GetComponent<PhotonView>().Owner.NickName);

            //if (other.gameObject.GetComponent<PhotonView>().Owner.IsMasterClient)
            //    // {
            //    print("I am Master Client");
            //    for (int i = 0; i < UIManager.Instance.VoiceChatParent.transform.childCount; i++)
            //    {
            //        Destroy(UIManager.Instance.VoiceChatParent.transform.GetChild(i).gameObject);
            //    }
            //    if (PhotonNetwork.CurrentRoom.PlayerCount > 1) { UIManager.Instance.MuteAllVoiceChatButton.SetActive(false); }
            //    else { UIManager.Instance.MuteAllVoiceChatButton.SetActive(true); }

            // //   foreach (string s in PlayerInRoom)
            //   // {
            //        GameObject voice = Instantiate(UIManager.Instance.VoiceChatInstance, UIManager.Instance.VoiceChatParent.transform);
            //voice.transform.GetChild(0).GetComponent<Text>().text = other.gameObject.GetComponent<PhotonView>().Owner.NickName;
               // }
          //  }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            PlayerInRoom.Remove(other.gameObject.GetComponent<PhotonView>().Owner.NickName);
           //int  i = 0;
           // while (i < UIManager.Instance.VoiceChatParent.transform.childCount)
           // {
           //     if (UIManager.Instance.VoiceChatParent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text == other.gameObject.GetComponent<PhotonView>().Owner.NickName)
           //     {
           //         Destroy(UIManager.Instance.VoiceChatParent.transform.transform.GetChild(i).gameObject);
           //         break;
           //     }
           //     i++;
           // }
        }
    }
}

