using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSelection : MonoBehaviourPunCallbacks
{
    public CharacterModals SelectedCharacter;
    public Texture2D[] eyes, eyebrows;

    private PhotonView photonView;
    public GameObject[] setPlayer;
    public Photon.Realtime.Player[] getPlayers;
    int A, B, C, D, E;
    public List<GameObject> plnumbers = new List<GameObject>();
   
    [PunRPC]
    private void SetPlayersDress()
    {
        getPlayers = PhotonNetwork.PlayerList;
        setPlayer = GameObject.FindGameObjectsWithTag("Player");
        List<int> ids = new List<int>();
        foreach (GameObject p in setPlayer)
        {
            ids.Add(p.GetComponent<PhotonView>().ViewID);
        }
        ids.Sort();//in ascending order
        //sortPlayers on base of their view ids
        List<GameObject> sortedPlayer = new List<GameObject>();
        for (int i = 0; i < setPlayer.Length; i++)
        {
            for (int j = 0; j < setPlayer.Length; j++)
            {
                if (ids[i] == setPlayer[j].GetComponent<PhotonView>().ViewID)
                {
                    sortedPlayer.Add(setPlayer[j]);
                    Debug.Log(setPlayer[j].GetComponent<PhotonView>().ViewID);
                }
            }
        }
        Debug.Log("Sorted count" + sortedPlayer.Count);
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            int k = i;
            CharacterModals cm = sortedPlayer[k].GetComponent<PlayerSelection>().SelectedCharacter;


            if(!sortedPlayer[k].GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("Sorted count K: " + k);

                sortedPlayer[k].GetComponent<CharacterController>().enabled = false;
                sortedPlayer[k].GetComponent<StarterAssets.ThirdPersonController>().enabled = false;
            }

            for (int j = 0; j < cm.TopModals.Length; j++)
            {
                cm.TopModals[j].SetActive(false);

            }
          
            for (int j = 0; j < cm.Bottoms.Length; j++)
            {
                cm.Bottoms[j].SetActive(false);

            }
            for (int j = 0; j < cm.HairStyle.Length; j++)
            {
                cm.HairStyle[j].SetActive(false);

            }
            for (int j = 0; j < cm.Footware.Length; j++)
            {
                cm.Footware[j].SetActive(false);

            }
            for (int j = 0; j < cm.EyeWears.Length; j++)
            {
                cm.EyeWears[j].SetActive(false);

            }

            for (int j = 0; j < cm.FacialHairs.Length; j++)
            {
                cm.FacialHairs[j].SetActive(false);

            }
            Debug.Log("TOP :" + getPlayers[i].CustomProperties["TOP"]);
            Debug.Log("BOTTOM :" + getPlayers[i].CustomProperties["BOTTOM"]);
            Debug.Log("HAIRSTYLE :" + getPlayers[i].CustomProperties["HAIRSTYLE"]);
            Debug.Log("FOOTWARE :" + getPlayers[i].CustomProperties["FOOTWARE"]);
            Debug.Log("EYEWARE :" + getPlayers[i].CustomProperties["EYEWARE"]);
            Debug.Log("MOUTH :" + getPlayers[i].CustomProperties["MOUTH"]);

            if ((int)getPlayers[i].CustomProperties["TOP"] >= 0)
            { Debug.Log("..D.."+(int)getPlayers[i].CustomProperties["TOP"]);
                cm.TopModals[(int)getPlayers[i].CustomProperties["TOP"]].SetActive(true); }

            if ((int)getPlayers[i].CustomProperties["BOTTOM"] >= 0)
                cm.Bottoms[(int)getPlayers[i].CustomProperties["BOTTOM"]].SetActive(true);
            if ((int)getPlayers[i].CustomProperties["HAIRSTYLE"] >= 0)
                cm.HairStyle[(int)getPlayers[i].CustomProperties["HAIRSTYLE"]].SetActive(true);
            if ((int)getPlayers[i].CustomProperties["FOOTWARE"] >= 0)
               cm.Footware[(int)getPlayers[i].CustomProperties["FOOTWARE"]].SetActive(true);
            if ((int)getPlayers[i].CustomProperties["EYEWARE"] >= 0)
            { cm.EyeWears[(int)getPlayers[i].CustomProperties["EYEWARE"]-1].SetActive(true); 
            }
            if (!cm.CharacterName.Contains("Female"))
            {
                if ((int)getPlayers[i].CustomProperties["MOUTH"] >= 0 && cm.FacialHairs.Length != 0)
                    cm.FacialHairs[(int)getPlayers[i].CustomProperties["MOUTH"]].SetActive(true);
            }

            cm.Mouth.SetActive(true);
            cm.Eyes.SetActive(true);
            cm.HeadObj.SetActive(true);
            cm.yebrows.SetActive(true);
            cm.CharModal.SetActive(true);
            cm.Eyes.GetComponent<Renderer>().material.mainTexture = eyes[PlayerPrefs.GetInt("eyes",0)];
            cm.yebrows.GetComponent<Renderer>().material.mainTexture = eyebrows[PlayerPrefs.GetInt("eyebrows", 0)];


            if(cm.TopModals[(int)getPlayers[i].CustomProperties["TOP"]].GetComponent<MeshCollider>() == null)
            cm.TopModals[(int)getPlayers[i].CustomProperties["TOP"]].AddComponent<MeshCollider>();
            if (cm.Bottoms[(int)getPlayers[i].CustomProperties["BOTTOM"]].GetComponent<MeshCollider>() == null)
            cm.Bottoms[(int)getPlayers[i].CustomProperties["BOTTOM"]].AddComponent<MeshCollider>();
            if (cm.HairStyle[(int)getPlayers[i].CustomProperties["HAIRSTYLE"]].GetComponent<MeshCollider>() == null)
            cm.HairStyle[(int)getPlayers[i].CustomProperties["HAIRSTYLE"]].AddComponent<MeshCollider>();
            if (cm.Footware[(int)getPlayers[i].CustomProperties["FOOTWARE"]].GetComponent<MeshCollider>()==null)
            cm.Footware[(int)getPlayers[i].CustomProperties["FOOTWARE"]].AddComponent<MeshCollider>();
           if (cm.Mouth.GetComponent<MeshCollider>() ==null)
            cm.Mouth.AddComponent<MeshCollider>();
           if (cm.HeadObj.GetComponent<MeshCollider>()==null)
            cm.HeadObj.AddComponent<MeshCollider>();
           if (cm.Eyes.GetComponent<MeshCollider>() ==null)
            cm.Eyes.AddComponent<MeshCollider>();
           if (cm.yebrows.GetComponent<MeshCollider>() ==null)
            cm.yebrows.AddComponent<MeshCollider>();


            List<Material> myMaterials = new List<Material>();
            Renderer[] r = cm.CharModal.GetComponentsInChildren<Renderer>();

            for (int i1 = 0; i1 < r.Length; i1++)
            {
                myMaterials = r[i1].materials.ToList();

                for (int j = 0; j < myMaterials.Count; j++)
                {
                    if (myMaterials[j].name.Contains("skintone") || myMaterials[j].name.Contains("SkinTone"))
                    {
                        Debug.Log(" value of RGB : "
                            + (float)getPlayers[i].CustomProperties["R"] + " G:" +
                                                        (float)getPlayers[i].CustomProperties["G"] + " B:" +
                                                        (float)getPlayers[i].CustomProperties["B"]);
                        

                        myMaterials[j].color = new Color((float)getPlayers[i].CustomProperties["R"],
                                                         (float)getPlayers[i].CustomProperties["G"],
                                                         (float)getPlayers[i].CustomProperties["B"]      
                                                         );
                    }
                }
            }
        }
    }


    void Start()
    {
        photonView = GetComponent<PhotonView>();
         if (PhotonNetwork.IsConnected && PhotonNetwork.LocalPlayer.IsLocal)
        {
            Transform[] modal = SelectedCharacter.CharModal.GetComponentsInChildren<Transform>();
            foreach (Transform a in modal)
                a.gameObject.SetActive(false);
        }
        photonView.RPC("SetPlayersDress", RpcTarget.All);
      //  Byn.Unity.Examples.VideoCall.uVideoLayout = 
    }


    #region triggerEnter
    public bool isVIdeoPlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "AuditoriamVideo")
        {
            if ((photonView.IsMine) && 
                (PlayerPrefs.GetString("UserRole").Contains("cohost") || 
                PlayerPrefs.GetString("UserRole").Contains("host")))
            {
                isVIdeoPlaying = !isVIdeoPlaying;
                if (!isVIdeoPlaying)
                {
                    UIManager.Instance.CLoseforHostOnly();
                }
            }
        }
    }

    #endregion
}
