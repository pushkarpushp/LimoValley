using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizeCharacter : MonoBehaviour
{
    public static FinalizeCharacter ins;
    private void Awake()
    {
        if (ins == null)
            DontDestroyOnLoad(this.gameObject);
        else
        { Debug.Log(this.gameObject.name); }
    }
}
