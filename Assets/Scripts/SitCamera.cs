using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitCamera : MonoBehaviour
{
    public GameObject player;
    public Camera cam;
    private Transform playerTransform;

    public float distanza = 3.0f;
    public const float Y_ANGOLO_Min = -120.0f;
    public const float Y_ANGOLO_Max = 80.0f;
    public float sensibilit_X = 4.0f;
    public float sensibilit_Y = 1.0f;
    public float correnteX = 0f;
    public float correnteY = 0f;




    // Start is called before the first frame update
    void Start()
    {
       // cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        correnteX += Input.GetAxisRaw("Mouse X") * sensibilit_X;
        correnteY += Input.GetAxisRaw("Mouse Y") * sensibilit_Y;

        correnteY = Mathf.Clamp(correnteY, Y_ANGOLO_Min, Y_ANGOLO_Max);

    }
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distanza);

        Quaternion rotation = Quaternion.Euler(correnteY, correnteX, 0);     //valore rotazione
        cam.transform.position = player.transform.position + rotation * dir;  //posizione camera
        cam.transform.LookAt(player.transform.position);

    }





}

