using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class NetPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    public GameObject boatBtn;
    public PhotonView view;
    public Boat boat;
    int boatSeat = -1;
    public enum PlayerState
    {
        InBoat,
        NearBoat,
        OutBoat
    }
    public PlayerState state = PlayerState.OutBoat;
    public PlayerState prevState = PlayerState.OutBoat;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        if(boatBtn != null)
        {
            //boatBtn = FindObjectOfType<SpawnPlayers>().boatBtn;
            boatBtn.GetComponent<Button>().onClick.AddListener(() =>
            {
                ToggleSitting();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (view.IsMine)
        {
            if(state == PlayerState.OutBoat)
            {
                if(boatBtn != null)
                    boatBtn.SetActive(false);
            }
            else
            {
                if (boatBtn != null)
                    boatBtn.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                ToggleSitting();

            }

        }
        if(state != PlayerState.OutBoat && boat != null && Vector3.Distance(transform.position, boat.transform.position) > 7f)
        {
            state = PlayerState.OutBoat;
            boat = null;
        }
        if(prevState == PlayerState.NearBoat && state == PlayerState.InBoat)
        {
            Sit();
        }
        else if(prevState == PlayerState.InBoat && state == PlayerState.NearBoat)
        {
            Stand();
        }

        prevState = state;
    }
    public void ToggleSitting()
    {
        view.RPC("MakeSit", RpcTarget.All);
    }
    
    [PunRPC]
    public void MakeSit()
    {
        if (state == PlayerState.NearBoat) state = PlayerState.InBoat;
        else if (state == PlayerState.InBoat) state = PlayerState.NearBoat;
    }

    public void Sit()
    {
        bool isSeatAvailable = false;
        for(int i = 0; i < boat.occupiedSeats.Length; i++)
        {
            if (!boat.occupiedSeats[i])
            {
                isSeatAvailable = true;
                boatSeat = i;
                break;
            }
        }
        Debug.Log(boatSeat);
        if (isSeatAvailable)
            boat.Sit(boatSeat, transform);
        else
            state = PlayerState.NearBoat;
    }
    public void Stand()
    {
        boat.Stand(boatSeat, transform);
    }

    [PunRPC]
    public void SayHI()
    {
        Debug.Log("Hello from the other side");
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        /*
        if (stream.IsWriting)
        {
            //if (!view.IsMine) return;
            stream.SendNext(state);
            stream.SendNext(prevState);
            //stream.SendNext(boat);
        }
        else
        {
            state = (PlayerState)stream.ReceiveNext();
            prevState = (PlayerState)stream.ReceiveNext();
            //boat = (Boat)stream.ReceiveNext();
        }
        */
    }
}
