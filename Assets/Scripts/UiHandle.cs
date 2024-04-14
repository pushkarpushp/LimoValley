using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandle : MonoBehaviour
{
    public GameObject Type;
    public GameObject Select;
    public GameObject SelectPanel;
   
   

     void Start() 
    {
        Back();
    }
    public void AvatarScreen() // Register button
    {
        Type.SetActive(true);
        Select.SetActive(false);
        SelectPanel.SetActive(false);
    }

    public void Back() // Register button
    {
        Type.SetActive(false);
        Select.SetActive(true);
        SelectPanel.SetActive(true);
    }



}
