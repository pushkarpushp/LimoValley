using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class splashScreen : MonoBehaviour
{

    public VideoPlayer player;
    public Image SplashImg;
    public GameObject SplashObj;
    // Start is called before the first frame update
    void Awake()
    {
        if(!PlayerPrefs.HasKey("Load"))
        {
            PlayerPrefs.SetInt("Load", 0);
            Debug.Log("Start Video Splash111");
        }
        else
        {
                gameObject.SetActive(false);
            Debug.Log("Start Video Splashsfsdf");
            return;
        }
      
      
        
#if !UNITY_EDITOR
        player.url = Javascripthooks.Instance.videoUrl;
#else
        player.url = "https://seett.eventcombo.com/eventiverse/loading/NexthonSplashVideo.mp4";
#endif
        player.Prepare();
        
        player.loopPointReached += showImg;
        Debug.Log("Start End Video Splash");
    }

    void showImg(VideoPlayer player)
    {
        Debug.Log("Start Video Splash Show Video");
        player.gameObject.SetActive(false);
        SplashImg.gameObject.SetActive(true);
        Invoke("disableObj", 5.0f);
    }

    void disableObj()
    {
        Debug.Log("Splash disabled invoke");
        SplashObj.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
      //  PlayerPrefs.DeleteKey("Load");
    }
}
