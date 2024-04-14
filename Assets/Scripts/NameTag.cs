using UnityEngine;
using Photon.Pun;

public class NameTag : MonoBehaviourPun
{
    public TMPro.TextMeshProUGUI canvasText; // Reference to the canvas TextMeshProUGUI component

    private void Start()
    {
        if(canvasText == null)
            canvasText = GetComponentInChildren<TMPro.TextMeshProUGUI>(); // Get the TextMeshProUGUI component in the children
        canvasText.text = GetComponent<PhotonView>().Controller.NickName;
    }

   
}
