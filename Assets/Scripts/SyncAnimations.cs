using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SyncAnimations : MonoBehaviour
{
    Animator anim;
    Avatar avatar;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        avatar = anim.avatar;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("yoga1") || anim.GetBool("yoga2") || anim.GetBool("yoga3") || anim.GetBool("yoga0"))
        {
            anim.avatar = null;
        }
        else
        {
            anim.avatar = avatar;
        }
    }

    
}
