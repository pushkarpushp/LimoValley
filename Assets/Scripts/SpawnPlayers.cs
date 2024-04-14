using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using StarterAssets;
using UnityEngine.InputSystem;
using Photon.Pun.Demo.SlotRacer.Utils;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public InputActionAsset starterAsset;
    public GameObject MobileCanvas;
    public GameObject[] playerPrefab;

    public Transform[] boatPostions;
    public Transform boatNextPos;
    public GameObject boatBtn;
    public GameObject boatingBtn;
    public float maxX;
    public float maxZ;


    public void Start()
    {
        //Cursor.visible = true;
        //Screen.lockCursor = false;
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        GameObject prefab = playerPrefab[selectedCharacter];

        Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(-maxX, maxX), transform.position.y, transform.position.z + Random.Range(-maxZ, maxZ));
        GameObject player = PhotonNetwork.Instantiate(prefab.name, randomPosition, Quaternion.identity);
        player.GetComponent<NetPlayer>().boatBtn = boatBtn;
#if UNITY_ANDROID
        MobileCanvas.GetComponent<UICanvasControllerInput>().starterAssetsInputs = player.GetComponent<StarterAssetsInputs>();
        MobileCanvas.GetComponent<MobileDisableAutoSwitchControls>().playerInput = player.GetComponent<PlayerInput>();
#endif

        // Cursor.lockState = CursorLockMode.Locked;
        if (PhotonNetwork.IsMasterClient)
        {
            foreach(var t in boatPostions)
            {
                PhotonNetwork.InstantiateRoomObject("Boat", t.position, t.rotation);


            }

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


}
