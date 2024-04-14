using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen:Singleton<LoadingScreen>
{
    public Slider slider;
    public Text progressText;
    public GameObject Loading;
    public void LoadScene(int no)
    {
        Loading.SetActive(true);
        StartCoroutine(LoadLevelAsync(no));
    }
    IEnumerator LoadLevelAsync(int no)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(no);
        while (asyncOperation.isDone != true)
        {
            slider.value = asyncOperation.progress;
            progressText.text = "LoadingScreen : " + slider.value * 100 + "%";
            yield return new WaitForEndOfFrame();
        }
    }
    public void SliderVal()
    {
        Loading.SetActive(true); 
        StartCoroutine(IncVal());
    }
    IEnumerator IncVal()
    {
        float a = 0;
        while (a <= 10)
        {
            yield return new WaitForEndOfFrame();
            a += 0.1f;
            slider.value = a;
            progressText.text = "LoadingScreen : " + slider.value * 100 + "%";
        }
    }
}
