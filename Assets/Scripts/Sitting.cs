using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    public Transform player;
    public Transform player2;
    public bool vehicleActive;
    bool isInTransition;
   public bool IsReady;
    public Transform seatPoint;
    public Transform seatPoint2;
    public Vector3 sittingoffset;
    public Transform exitPoint;

    public GameObject Chair;
   

    [Space]
    public float transitionSpeed = 0.2f;

    private void Start()
    {
        player = this.gameObject.transform;

        player2 = this.transform.Find("Geometry");
    }
    private void LateUpdate()
    {

         if (vehicleActive && isInTransition && IsReady)
         {
             Debug.Log("in exit called ");
             Exit();

         }

         else if (!vehicleActive && isInTransition && IsReady)
         {
             Debug.Log("in Enter called ");

             Enter();
         }

       

        if (Input.GetKeyDown(KeyCode.E))
        {
            isInTransition = true;
            
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chair")
       
        {
            Chair = other.gameObject;
            IsReady = true;
            seatPoint  = other.gameObject.transform;
            exitPoint  = other.transform.Find("CadNew");
            seatPoint2 =     other.transform.Find("Cad");
        }
    }

    

    void Enter()
    {
        // Disable Components

      
        // Set Obj animation to sitting
        player.GetComponentInChildren<Animator>().SetBool("siting", true);
        player.GetComponentInChildren<CamMov>().enabled = true;

        player.GetComponent<ThirdPersonController>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        
        // Move Obj to designated seat
        player.position = Vector3.Lerp(player.position, seatPoint.position + sittingoffset, transitionSpeed);
        player.rotation = Quaternion.Slerp(player.rotation, seatPoint.rotation, transitionSpeed);
       // Chair.SetActive(false);

        StartCoroutine(Adjust());
        // The Reset-Check
        Debug.Log(".." + player2.position + " sittingoffset pos : " + sittingoffset);

        if (player2.position == seatPoint2.position + sittingoffset)
        {
            Debug.Log(".." + player.position + " Seat pos : " + seatPoint.position);
            isInTransition = false; vehicleActive = true ; IsReady = false;   
        }
    }

    IEnumerator Adjust()
    {
        yield return new WaitForSeconds(0.5f);

        player2.position = Vector3.Lerp(player2.position, seatPoint2.position + sittingoffset, transitionSpeed);
        player2.rotation = Quaternion.Slerp(player2.rotation, seatPoint2.rotation, transitionSpeed);

    }

    IEnumerator ReAdjust()
    {
        yield return new WaitForSeconds(0);

        player2.position = Vector3.Lerp(player.position, exitPoint.position, transitionSpeed);
        player2.rotation = Quaternion.Slerp(player.rotation, exitPoint.rotation, transitionSpeed);

        
    }


    void Exit()
    {
        // Move Obj to designated exit point
        player.position = Vector3.Lerp(player.position, exitPoint.position, transitionSpeed);

        // Set Obj animation to idle
        player.GetComponentInChildren<Animator>().SetBool("siting", false);
        Chair.SetActive(true);

        StartCoroutine(ReAdjust());
        // The Reset-Check
        if (player.position == exitPoint.position)
        {
            isInTransition = false; vehicleActive = false; IsReady = false;
            
        }
        // Enable Components
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponentInChildren<CamMov>().enabled = false;
        player.GetComponent<ThirdPersonController>().enabled = true;
       // player.GetComponent<Rigidbody>().useGravity = true;

        



    }
}
       
    
    
