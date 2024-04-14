using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Playerlist : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            GameObject attendee = Instantiate(UIManager.Instance.AttendeeInstance);
            attendee.transform.SetParent(UIManager.Instance.AttendeeParent.transform);
            attendee.transform.localScale = Vector3.one;

            attendee.name = p.NickName;
            attendee.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            if (p.NickName == PhotonNetwork.LocalPlayer.NickName)
            {
                attendee.transform.SetSiblingIndex(0);
            }
            else
            {

            }
        }
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("abcdef");
        base.OnJoinedRoom();
        foreach (Transform child in UIManager.Instance.AttendeeParent.transform)
        {
            //if (child.name == otherPlayer.NickName)
          //  {
                Destroy(child.gameObject);
           // }
        }
        GameObject[] playersInGame = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(playersInGame.Length);
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            Debug.Log(p.NickName + " <> nick name : ");

            Debug.Log("Local Player :" + p.NickName);
            GameObject attendee = Instantiate(UIManager.Instance.AttendeeInstance);
            attendee.transform.SetParent(UIManager.Instance.AttendeeParent.transform);
            attendee.name = p.NickName;
            attendee.transform.localScale = Vector3.one;
            attendee.transform.GetChild(0).GetComponent<Text>().text = p.NickName;
            if ((p.IsLocal))
            {
                attendee.transform.SetSiblingIndex(0);


            }
            else
            {


            }
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        this.OnJoinedRoom();

    }

    /*While left player from the Network server */
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        foreach (Transform child in UIManager.Instance.AttendeeParent.transform)
        {
            if (child.name == otherPlayer.NickName)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
