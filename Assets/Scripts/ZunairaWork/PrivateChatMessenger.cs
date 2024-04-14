using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PrivateChatMessenger : MonoBehaviourPunCallbacks
{
    public InputField privateInputField;
    ////public Transform privateChatMsgInstance;
    public GameObject ChatPanel;
    public Text SelectedChannelText;
    public Text PersonName;
    public string targetName;
    public Color  grey;
    public Color  blue;
    public ChatData chatData;
    public ChatMessages chatMessages;
  //  public PhotonView photonView;
    private bool IsFOund, isSender;
    public static PrivateChatMessenger Instance;
    void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
     //   photonView = GetComponent<PhotonView>();
    }
    private void Update()
    {
        OnEnterSend();
    }
    public void OnEnterSend()
    {
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
        {
            SendPrivateMessage();
        }
    }
    public void TypeValueChanged(string valueIn)
    {
        Debug.Log("typing");
        RemoveMsgCount();
    }
    public void SendPrivateMessage()
    {
        if (!string.IsNullOrEmpty(privateInputField.text) && !string.IsNullOrWhiteSpace(privateInputField.text) && !string.IsNullOrEmpty(targetName) && !string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            isSender = true;
            photonView.RPC("CheckChatMessage", RpcTarget.All, PhotonNetwork.NickName, targetName, privateInputField.text);
            privateInputField.text = "";
            privateInputField.ActivateInputField();
        }
    }

    [PunRPC]
    private void CheckChatMessage(string sender, string reciever, string Message)
    {
        GameObject msgs=null;
        if (PhotonNetwork.NickName == sender || PhotonNetwork.NickName == reciever)//only two side
        {
            //if (isSender)
            //{
            //    for (int i = 0; i < UIManager.Instance.MsgsCLone.Count; i++)
            //    {
            //        if (UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().targetName == reciever)
            //        {
            //            msgs = Instantiate(UIManager.Instance.privatemsgs, UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().privateChatMsgInstance.transform);
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < UIManager.Instance.MsgsCLone.Count; i++)
            //    {
            //        if (UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().targetName == sender )
            //        {
            //            msgs = Instantiate(UIManager.Instance.privatemsgs, UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().privateChatMsgInstance.transform);
            //            break;
            //        }
            //    }
            //}
            Debug.Log("I am here");
            string chatName;
            if (isSender) { chatName = reciever;
                //msgs.transform.GetComponent<VerticalLayoutGroup>().padding.left = 60;
                //msgs.transform.GetComponent<VerticalLayoutGroup>().childControlWidth = false;
                //msgs.transform.GetChild(0).transform.GetComponent<Image>().color = blue;
            }//For sender chat searching name will be with reciever
            else { chatName = sender;
                //msgs.transform.GetChild(0).transform.GetComponent<Image>().color = grey;
            }//For reciever will search with the name of sender
            for (int i = 0; i < chatData.chatMessages.Count; i++)
            {
                if (chatData.chatMessages[i].Name == chatName)
                {
                    chatData.chatMessages[i].Messages += "\n" + "<b><color=black><size=16>" + sender  + "</size></color></b>" + ": " + Message;
                    if (PersonName.text == chatName) SelectedChannelText.text = chatData.chatMessages[i].Messages;
                    //msgs.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = Message;
                    //Debug.Log("msggg"+Message);
                    IsFOund = true;
                }
            }
            if (!IsFOund)
            {
                ChatMessages chat = new ChatMessages();
                chatData.chatMessages.Add(chat);
                chatData.chatMessages[chatData.chatMessages.Count - 1].Name = chatName;
                chatData.chatMessages[chatData.chatMessages.Count - 1].Messages += "\n" + "<b><color=black><size=16>" + sender + "</size></color></b>"+ ": " + Message;
                //msgs.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = Message;
                if (PersonName.text == chatName) SelectedChannelText.text = chatData.chatMessages[chatData.chatMessages.Count - 1].Messages;
            }
            IsFOund = false;
          if (!isSender) { StartCoroutine(NotificationOfMessage(chatName)); }//Only display Messgae on recieving side.
            isSender = false;
        }
    }
    public void GetMessage(string name)
    {
        PersonName.text = name;
        SelectedChannelText.text = "";
        for (int i = 0; i < chatData.chatMessages.Count; i++)
        {
            if (chatData.chatMessages[i].Name == name)
            {
                SelectedChannelText.text = chatData.chatMessages[i].Messages;
            }
        }
    }
    IEnumerator NotificationOfMessage(string name)
    {
        Debug.Log(name);
        UnreadMsgCOunt(name);
        string[] myname = name.Split('(');
        Debug.Log(myname[1].Replace(")", ""));
        //nametext.text = myname[1].Replace(")", "");
        UIManager.Instance.MessageNotify.text = myname[1].Replace(")", "") + " send a message";
        UIManager.Instance.MessageNotify.transform.parent.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        UIManager.Instance.MessageNotify.text = "";
        UIManager.Instance.MessageNotify.transform.parent.gameObject.SetActive(false);
    }
    private void UnreadMsgCOunt(string name)
    {
        for (int i = 0; i < UIManager.Instance.PersonalTextChatParent.transform.childCount; i++)
        {
            if (UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetChild(0).GetComponent<Text>().text == name)
            {
                UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().msgCount += 1;
                //if (!(PersonName.text == PhotonNetwork.NickName))
                //{
                //    UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().MsgCountObj.SetActive(true);
                //}
                //else
                //{

                //}
                UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().MsgCountObj.SetActive(true);
                UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetChild(2).GetChild(0).GetComponent<Text>().text = UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().msgCount.ToString();
            }
        }
    }
    private void RemoveMsgCount()
    {
        Debug.Log(PersonName.text);
        for (int i = 0; i < UIManager.Instance.PersonalTextChatParent.transform.childCount; i++)
        {
            if (UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetChild(0).GetComponent<Text>().text == PersonName.text)
            {
                    UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().MsgCountObj.SetActive(false);
                UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).GetComponent<TextChatInstance>().msgCount = 0;
            }
        }
    }
    public void CloseChatPanel(bool b)
    {
        ChatPanel.SetActive(b);
    }
    public void OpenChatPanel()
    {
        RemoveMsgCount();
        ChatPanel.SetActive(true);
    }
}
