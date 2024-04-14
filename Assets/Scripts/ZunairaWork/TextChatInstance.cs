using System.Collections;
using System.Collections.Generic;
using Photon.Chat.Demo;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class TextChatInstance : MonoBehaviour
{
    public Text personName;
    public int msgCount;
    public GameObject MsgCountObj;
    //public GameObject PrivateChatPanelInstance;
  //  public GameObject chatPanel;
    private string nameOfSender;
    private void Start()
    {
    //    InstantiatePrivateChat();

    }
    public void OpenPanel()
    {
        //DisableOtherPrivateCHatPanel();
        //for (int i = 0; i < UIManager.Instance.MsgsCLone.Count; i++)
        //{
        //    if (UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().targetName == nameOfSender)
        //    {
        //       UIManager.Instance.MsgsCLone[i].gameObject.SetActive(true);
        //        break;
        //    }
        //}
    }
    public void SetSenderName()
    {
        //Debug.Log(chatPanel.gameObject.activeInHierarchy +"in stsart ghjhghgjg");
        //  if (chatPanel != null)
        //    DisableOtherPrivateCHatPanel();
        //   chatPanel.gameObject.SetActive(true);
        //   chatPanel.SetActive(true); 
        PrivateChatMessenger.Instance.targetName = personName.text;
        UIManager.Instance.PersonalTextChatPanel.SetActive(true);
        PrivateChatMessenger.Instance.GetMessage(personName.text);
        msgCount = 0;
        MsgCountObj.SetActive(false);
    }
    private void InstantiatePrivateChat()
    {
      //  //   if (PhotonNetwork.LocalPlayer.IsLocal)
      //  { 
      //  chatPanel = PhotonNetwork.Instantiate("PrivateChatPanel", UIManager.Instance.PrivateChatInstantiateParent.transform.position, Quaternion.identity);
      //      UIManager.Instance.MsgsCLone.Add(chatPanel);
      //      Debug.Log("here");
      //      chatPanel.transform.parent = UIManager.Instance.PrivateChatInstantiateParent.transform;
      //      chatPanel.transform.localScale= Vector3.one;
      //  //chatPanel = Instantiate(PrivateChatPanelInstance, UIManager.Instance.PrivateChatInstantiateParent.transform);
      //  }
      ////  chatPanel.GetComponent<PrivateChatMessenger>().PersonName.text=personName.text;
      // // chatPanel.GetComponent<PrivateChatMessenger>().targetName=personName.text;
      // // DisableOtherPrivateCHatPanel();
    }
    private void DisableOtherPrivateCHatPanel()
    {//Disable all other private chat panel.
        for(int i=2;i< UIManager.Instance.PrivateChatInstantiateParent.transform.childCount; i++)
        {
            UIManager.Instance.PrivateChatInstantiateParent.transform.GetChild(i).gameObject.SetActive(false);
            if (!UIManager.Instance.PrivateChatInstantiateParent.transform.GetChild(i).GetComponent<PhotonView>().IsMine)
            {
                Destroy(UIManager.Instance.PrivateChatInstantiateParent.transform.GetChild(i).gameObject);
            }
        }
    }
}
