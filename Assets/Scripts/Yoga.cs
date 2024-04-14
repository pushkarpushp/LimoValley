using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Yoga : MonoBehaviour
{
    Animator playerAnim = null;
    Avatar playerAvatar = null;
    public GameObject yogaBtn;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            PhotonView view = other.transform.GetComponent<PhotonView>();
            if(view != null && view.IsMine)
            {
                playerAnim = other.transform.GetComponent<Animator>();
                playerAvatar = playerAnim.avatar;
                yogaBtn.SetActive(true);
                yogaBtn.GetComponent<Button>().onClick.AddListener(DoYoga);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player") && playerAnim != null)
        {
            PhotonView view = other.transform.GetComponent<PhotonView>();
            if (view != null && view.IsMine)
            {
                playerAnim.avatar = playerAvatar;
                playerAnim = null;
                playerAvatar = null;
                yogaBtn.GetComponent<Button>().onClick.RemoveListener(DoYoga);
                yogaBtn.SetActive(false);
            }
        }
    }

    public void DoYoga()
    {
        if(playerAnim != null)
        {
            int i = Random.Range(1, 4);
            playerAnim.avatar = null;
            //playerAnim.Play("yoga"+i.ToString());
            playerAnim.SetBool("yoga" + i.ToString(), true);
            StartCoroutine(stopAnim(4f, i));
        }
    }

    IEnumerator stopAnim(float t, int i)
    {
        yield return new WaitForSeconds(t);
        playerAnim.avatar = playerAvatar;
        playerAnim.SetBool("yoga" + i.ToString(), false);

    }


}
