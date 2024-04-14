using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public ServerSettings serverSettings;
    public GameObject creationCanvas;
    public GameObject loadingCanvas;
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        PhotonNetwork.AutomaticallySyncScene = true;
        
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = "1.0";
        Debug.Log("start ..");

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
        Debug.Log("Connected ..");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(".. on joined lobby ");
        // SceneManager.LoadScene("Creation");
        creationCanvas.SetActive(true);
        loadingCanvas.SetActive(false);

     
    }
}
