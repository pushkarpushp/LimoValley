using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Redirect : MonoBehaviour
{
    public string url;

    public GameObject RedirectBtn;

    private Transform player; // Reference to the player's transform.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            RedirectBtn.SetActive(true);
            RedirectBtn.GetComponent<Button>().onClick.AddListener(RedirectLink);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            player = null;
            RedirectBtn.GetComponent<Button>().onClick.RemoveListener(RedirectLink);
            RedirectBtn.SetActive(false);
        }
    }

    private void Update()
    {
        if (player != null && Input.GetKeyDown(KeyCode.F))
        {
            RedirectLink();
        }
    }
    public void RedirectLink()
    {
        if (player == null) return;
        else
        {
            Application.OpenURL(url);
        }
    }
}
