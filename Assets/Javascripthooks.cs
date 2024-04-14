using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Javascripthooks : MonoBehaviour
{
    [SerializeField] public TMP_Text Number;

    [SerializeField] public int Value1;
    [SerializeField] public int Value2;
    [SerializeField] public int Value3;


    [SerializeField] public string baseURL;
    [SerializeField] public string serverlink;
    [SerializeField] public string videoUrl;

    [SerializeField] public string ImageURL;
    [SerializeField] public string CompanyName;


    private static Javascripthooks instance;

    public static Javascripthooks Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Javascripthooks>();
            }
            return instance;
        }
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Video_Link("https://vimeo.com/804187608");
        ServerLink("https://seett.eventcombo.com");
        ImageLink();
#endif

    }
    public void StoreIntValues(int value1, int value2, int value3)
    {
        Value1= value1;
        Value2 = value2;
        Value3 = value3;

        Debug.Log(" values : " + Value1 + "Value2 :" + Value2 + " Value3 : " + Value3);
    }


    public void StoreBrowserSessionId(string number)
    {
        Number.text = number.ToString();
    }

    public void ServerLink(string link)
    {
        baseURL = link;
        serverlink = link + "/eventiverse/assets/playground.unity3d";
        videoUrl = link + "/eventiverse/loading/NexthonSplashVideo.mp4";
    }


    public void ImageLink()
    {
        ImageURL = baseURL + "/eventiverse/pdfs/logo.png";
        Debug.Log("ImageURL : " + ImageURL);

    }

    public void CompanyName_Link(string Company_Name)
    {
        CompanyName = Company_Name;

        Debug.Log("CompanyName : "+ CompanyName);
        PlayerPrefs.SetString("CompanyName", CompanyName);

    }

    public void Video_Link(string VideoUrl)
    {
        string Videolink = VideoUrl;

        Debug.Log("Videolink : " + Videolink);

        PlayerPrefs.SetString("Videolink", Videolink);
    }

}

