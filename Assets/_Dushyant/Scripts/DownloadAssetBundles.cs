using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using TMPro;
public class DownloadAssetBundles : MonoBehaviour
{

    [Header("-- Asset Bundle URL --")]
    public string bundleUrl ="";
    public TMP_Text percentageTxt;
    private bool isDownloading = false;
public GameObject LoadingScreen;
    string sceneName;

    public Slider LoadingBar;
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
  public static string sName;
  public  bool ischeckAsset =false;

    void Start()
    {
        // int d = SceneManager.GetActiveScene().buildIndex;
        //Debug.Log(sceneName);
        bundleUrl = Javascripthooks.Instance.serverlink;
//#if UNITY_EDITOR
        ischeckAsset = false;
        sceneName = "Playground";
//#else
  //  ischeckAsset = true;
  //     #endif

        if (ischeckAsset)
        {
            if (bundleUrl != "" )
                StartCoroutine(DownloadAssetBundle());
        }
        else
        {

            if ((SceneManager.GetActiveScene().name.Contains("creation")))
            {
                Debug.Log(sceneName);
                if (LoadingScreen != null)
                    LoadingScreen.SetActive(false);
                sName = sceneName;
            }
            else
            { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
        }
    }

   public IEnumerator DownloadAssetBundle()
    {
        int a = UnityEngine.Random.Range(1,5000);
        uint vOut = Convert.ToUInt32(a);
        Debug.Log("Here .. "+vOut);
      //  string bundleurl = Javascripthooks.Instance.serverlink;
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl, vOut, 0);
        isDownloading = true;
        StartCoroutine(progress(request));
        yield return request.SendWebRequest();
        isDownloading = false;
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
        if (bundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield return null;
        }
        if (bundle.isStreamedSceneAssetBundle)
        {
            string[] scenePaths = bundle.GetAllScenePaths();
            curentAssetBundle = bundle;
            sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);

            if (sceneName.Contains("Playground"))
            {  
                if (LoadingScreen != null)
                    LoadingScreen.SetActive(false);
             sName = sceneName;
            }
            else
                SceneManager.LoadScene(sceneName);


        }
        Debug.Log("Bundle URL Is : "+ bundleUrl);


    }
    public AssetBundle curentAssetBundle;
    IEnumerator progress(UnityWebRequest req)
    {
        yield return new WaitForSeconds(0.1f);
        while (isDownloading)
        {
            percentageTxt.text ="Loading..."+ (req.downloadProgress * 100).ToString("F0") + "%";
          
            if(LoadingBar != null)
            LoadingBar.value = req.downloadProgress;
          
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void loadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

}
