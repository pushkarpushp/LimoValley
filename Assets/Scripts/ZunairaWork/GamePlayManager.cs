using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GamePlayManager : Singleton<GamePlayManager>
{
    public GameObject Player;
    public int Id;
    public List<GameObject> chairs = new List<GameObject>();
    

    //public CharacterModals playerOBj;
    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            if (p.GetComponent<PhotonView>().IsMine)
            {
                Player = p;
                break;
            }
        }
    }
}
