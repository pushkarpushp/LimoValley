using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using Photon.Pun;
public class VoiceManager : MonoBehaviour
{
    public Recorder recorder;
    public GameObject muteImg;
    public GameObject speakerMuteImg;
    bool isMicEnabled = true;
    bool isSpeakerEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleMic()
    {
        isMicEnabled = !isMicEnabled;
        recorder.TransmitEnabled = isMicEnabled;
        muteImg.SetActive(!isMicEnabled);
    }
    public void ToggleSpeark()
    {
        isSpeakerEnabled = !isSpeakerEnabled;
        speakerMuteImg.SetActive(!isSpeakerEnabled);
        Speaker[] speakers = FindObjectsOfType<Speaker>();
        for (int i = 0; i < speakers.Length; i++)
        {
            if(isSpeakerEnabled)
                speakers[i].GetComponent<AudioSource>().volume = 1;
            else speakers[i].GetComponent<AudioSource>().volume = 0;
        }
    }
}
