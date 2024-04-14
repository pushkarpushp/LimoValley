using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vimeo;
using Vimeo.Player;
using Vimeo.SimpleJSON;
using System.Text.RegularExpressions;

public class DynamicVideo : MonoBehaviour
{

    public VimeoPlayer FullScreenplayer, AuditoriamVideo;
    public string url;
    // Start is called before the first frame update

    
    IEnumerator Start()
    {
        Debug.Log(transform.name + " .. DUS ");
        yield return new WaitForSeconds(2f);
        url = PlayerPrefs.GetString("Videolink");
   
        Match match = Regex.Match(url, "(vimeo.com)?(/channels/[^/]+)?/?([0-9]+)");

        if (match.Success)
        {
           string vimeoVideoId = match.Groups[3].Value;

            Debug.Log(vimeoVideoId);
         
            FullScreenplayer.LoadVideo(int.Parse(vimeoVideoId));
            AuditoriamVideo.LoadVideo(int.Parse(vimeoVideoId));
        }
        else
        {
            Debug.LogError("[Vimeo] Invalid Vimeo URL");
        }
    }


    public void PlayVideo()
    {
        FullScreenplayer.Play();
    }
    public void Pause()
    {
        FullScreenplayer.Pause();
    }
}
