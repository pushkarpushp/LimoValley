using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PublicChat : MonoBehaviour,IChatClientListener {
    public Color grey;
    public Color blue;
    private AudioSource audioSource;
    public bool istyu;
    ChatClient chatClient;
    bool isConnected;
    string UserName;
    public void UsernameOnValueChange(string valueIn) {
        //=valueIn;
    }
    public void ChatConectOnCLick() {
        UserName = PhotonNetwork.NickName;
        isConnected = true;
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(UserName));
        Debug.Log("Conecting");
    }
   // public GameObject ChatPanel;
    string privateReceiever = "";
    string currentChat;
    public InputField chatField;
    public Text chatDisplay;
    void Start() {
        audioSource = GetComponent<AudioSource>();
       Debug.Log("user Conecting");
     //   ChatPanel.SetActive(false); 
        ChatConectOnCLick();
    }
    void Update() {
        if (this.chatClient != null) {
            this.chatClient.Service();
        }
        if (!string.IsNullOrEmpty(chatField.text) && !string.IsNullOrWhiteSpace(chatField.text)&&(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))) {
            chatClient.Service();
            SubmitPublicChat();
        } }
    public void SubmitPublicChat() {
        if (!string.IsNullOrEmpty(chatField.text) && !string.IsNullOrWhiteSpace(chatField.text)){
            chatClient.PublishMessage("Region", currentChat);
            currentChat = "";
            chatField.text = "";
            chatField.ActivateInputField();
        }
    }
    public void TypeValueChanged(string valueIn) {
        currentChat=valueIn;
        Debug.Log("typing");
    }
    #region callBack
    public void DebugReturn(DebugLevel level, string message) {
    }

    public void OnDisconnected() {
    }

    public void OnConnected() {
        chatClient.Subscribe(new string[] { "Region" });
        Debug.Log("connecting to chanel");
    }

    public void OnChatStateChange(ChatState state) {
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages) {
        string msg = "";
        for (int i = 0; i < senders.Length; i++) {
          
            if (senders[i] == PhotonNetwork.NickName/*istyu*/)
            {
                GameObject msgs = Instantiate(UIManager.Instance.rightPrivateMsg, UIManager.Instance.publicChatParent.transform);
                msgs.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "<b><color=black><size=16>" + senders[i] + "</size></color></b>" + ": " + messages[i];
                //  msgs.transform.GetComponent<RectTransform>().position+=new Vector3(40f,transform.position.y,transform.position.z);
                msgs.transform.GetComponent<VerticalLayoutGroup>().padding.left = 30;
                //msgs.transform.GetComponent<VerticalLayoutGroup>().childControlWidth =false;
                msgs.transform.GetChild(0).transform.GetComponent<Image>().color = blue;
            }
            else
            {
                GameObject msgs = Instantiate(UIManager.Instance.leftPrivateMsgs, UIManager.Instance.publicChatParent.transform);
                msgs.transform.GetChild(0).transform.GetChild(0).transform.GetComponent<Text>().text = "<b><color=black><size=16>" + senders[i] + "</size></color></b>" + ": " + messages[i];
                msgs.transform.GetChild(0).transform.GetComponent<Image>().color =grey;
            }
           // chatDisplay.text += "\n" + "<b><color=black><size=16>" + senders[i] + "</size></color></b>" + ": "+ messages[i];
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName) {
    }

    public void OnSubscribed(string[] channels, bool[] results) {
        Debug.Log("joined chanel");
    }

    public void OnUnsubscribed(string[] channels) {
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) {
    }

    public void OnUserSubscribed(string channel, string user) {
    }

    public void OnUserUnsubscribed(string channel, string user) {
    }
    #endregion callback
}
