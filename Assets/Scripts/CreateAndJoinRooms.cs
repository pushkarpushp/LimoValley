using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public TMP_InputField username;
    public GameObject loadingImage;

    bool isRoomCreationFailed = false;
    void MakeRoom()

    {

        int randomRoomName = Random.Range(0, 50);
        RoomOptions roomOptions =
        new RoomOptions()
        {

            IsVisible = true,
            IsOpen = true,
            MaxPlayers = 20,
            EmptyRoomTtl = 50000,
            PlayerTtl = 5000

        };

        //PhotonNetwork.CreateRoom("0" + randomRoomName.ToString(), roomOptions);
        PhotonNetwork.JoinOrCreateRoom("Room1", roomOptions, TypedLobby.Default);
        
        
    }

    //string Sname;
    //public void GetString(string SceneName)
    //{
    //Sname = SceneName;
    //}
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log( message);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room Created,Waiting For Another player");
        //PhotonNetwork.JoinRoom("0" + randomRoomName);

        // PhotonNetwork.JoinRoom("Room1");
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Room joined Successfully ");
        // LoadingScreen.Instance.SliderVal();
        PhotonNetwork.LoadLevel("DemoScene 1");



    }

    public void FindMatch()
    {
        loadingImage.SetActive(true);
        PhotonNetwork.NickName = username.text;
        Debug.Log(username.text);
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom("Room1");

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Return Code: " + returnCode.ToString() + " " + message);
        MakeRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Return Code: " + returnCode.ToString() + " " + message);
        MakeRoom();
        isRoomCreationFailed = true;
    }


    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause.ToString());
    }

}
