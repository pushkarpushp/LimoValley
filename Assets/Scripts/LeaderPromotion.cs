using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LeaderPromotion : MonoBehaviourPunCallbacks
{
    public bool isLeader = false;
    public GameObject player;
    public GameObject playerListPanel;
    public GameObject playerListItemPrefab;
    public Text playerStatusText;
    public string playerStatus = "Player";

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            isLeader = true;
        }
    }

    public void PromotePlayer(PhotonView playerToPromote)
    {
        if (isLeader)
        {
            playerToPromote.RPC("BecomeSpeaker", RpcTarget.All);
        }
    }

    public void DemotePlayer(PhotonView playerToDemote)
    {
        if (isLeader)
        {
            playerToDemote.RPC("StopBeingSpeaker", RpcTarget.All);
        }
    }

    [PunRPC]
    public void BecomeSpeaker()
    {
        // Code to make the player a speaker
        playerStatus = "Speaker";
        playerStatusText.text = playerStatus;
    }

    [PunRPC]
    public void StopBeingSpeaker()
    {
        // Code to make the player stop being a speaker
        playerStatus = "Player";
        playerStatusText.text = playerStatus;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GameObject newPlayerListItem = Instantiate(playerListItemPrefab, playerListPanel.transform);
        Text playerNameText = newPlayerListItem.transform.Find("PlayerNameText").GetComponent<Text>();
        playerNameText.text = newPlayer.NickName;

        Text playerStatusText = newPlayerListItem.transform.Find("PlayerStatusText").GetComponent<Text>();
        playerStatusText.text = newPlayer.CustomProperties["status"].ToString();

        Button promoteButton = newPlayerListItem.transform.Find("PromoteButton").GetComponent<Button>();
        PhotonView newPlayerView = newPlayerListItem.GetComponent<PhotonView>();
        promoteButton.onClick.AddListener(() => PromotePlayer(newPlayerView));
    }
}
