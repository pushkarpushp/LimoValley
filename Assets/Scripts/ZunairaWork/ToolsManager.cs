using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vimeo.Player;

public class ToolsManager : MonoBehaviourPunCallbacks
{
    public GameObject[] Emoji;
    public Color blue;
    public Color green;
    private void Start()
    {
        photonView.RPC("ShowTextChatPlayers", RpcTarget.All);
    }   
    public void DisplayFullScreenVideo(bool val)
    {
        photonView.RPC("PLayViodeo", RpcTarget.AllBuffered, val);
    }
    #region VoiceChat
    [PunRPC]
    private void ShowVoiceChatPlayer()
    {
        //Destroy all gameOjects first to start new names instantiate. 
        for (int i = 0; i < UIManager.Instance.VoiceChatParent.transform.childCount; i++)
        {
            Destroy(UIManager.Instance.VoiceChatParent.transform.GetChild(i).gameObject);
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount >1) { UIManager.Instance.MuteAllVoiceChatButton.SetActive(false); } 
        else { UIManager.Instance.MuteAllVoiceChatButton.SetActive(true); }
        //instantiate new name 
        foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (!p.IsLocal)
            {
                GameObject voice = Instantiate(UIManager.Instance.VoiceChatInstance, UIManager.Instance.VoiceChatParent.transform);
                voice.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            }
        }
    }
      [PunRPC]
     private void ShowTextChatPlayers()

 

    {
        Debug.Log("show public chat plat player nsme RPC");
        //Destroy all gameOjects first to start new names instantiate. 
        for (int i = 0; i < UIManager.Instance.PersonalTextChatParent.transform.childCount; i++)
        {
            Destroy(UIManager.Instance.PersonalTextChatParent.transform.GetChild(i).gameObject);
        }



     //   instantiate new name
            foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (!p.IsLocal)
            {
                GameObject chat = Instantiate(UIManager.Instance.PersonalTextChatInstance, UIManager.Instance.PersonalTextChatParent.transform);
                chat.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            }
        }
    }
    private void DestroyPlayerName(string name)
    {
        Debug.Log("Name of Player is "+name);
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
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("from OnPlayerLeftRoom" + otherPlayer.NickName);
       DestroyPlayerName(otherPlayer.NickName);
        if (otherPlayer.IsLocal)
        { UnityEngine.SceneManagement.SceneManager.LoadScene("creation"); }
       // photonView.RPC("DestroyPlayerName", RpcTarget.Others, otherPlayer.NickName);

    }

    public override void OnLeftRoom()
    {


        Debug.Log("player left room");
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        UIManager.Instance.NoticePanel.SetActive(true);
        PlayerPrefs.SetInt("Loading",1);

    }
    //after player left 

    public void OnBackMenuClick()
    {
        UIManager.Instance.NoticePanel.transform.GetChild(0).GetChild(0).transform.GetComponent<TMPro.TMP_Text>().text = "Are you sure you want to leave the event and go back to the main screen?\n Your character will be lost";
        UIManager.Instance.NoticePanel.SetActive(true);

    }
    public void OnOKClick()
    {
        PhotonNetwork.LeaveRoom();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);

        AssetBundle.UnloadAllAssetBundles(true);

        // UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }

    public void onCancelClick()
    {
        UIManager.Instance.NoticePanel.SetActive(false);

    }
    #endregion VoiceChat
    #region Emoji
    public void DisplayEmoji(int num)
    {
        photonView.RPC("ShowEmoji", RpcTarget.All,GamePlayManager.Instance.Id,num);
    }
    [PunRPC]
    private void ShowEmoji(int id, int num)
    {
    StartCoroutine(EmojiTime(id,num));
    }
    IEnumerator EmojiTime(int id, int num)
    {
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in Players)
        {
            if (p.GetComponent<PhotonView>().ViewID == id)
            {
                p.GetComponent<PlayerController>().EmojiImage.gameObject.SetActive(true);
                p.GetComponent<PlayerController>().EmojiImage.GetComponent<Image>().sprite = Emoji[num].GetComponent<Image>().sprite;
                yield return new WaitForSeconds(3);
                p.GetComponent<PlayerController>().EmojiImage.gameObject.SetActive(false);
            }
        }
    }
    #endregion Emoji
    #region EventTextName
    public TMP_Text[] targetTexts;

    public void UpdateText()
    {
        foreach (TMP_Text t in targetTexts)
        {
            t.text = PlayerPrefs.GetString("CompanyName", "EventC");
            ;
        }
    }
    #endregion

  

}