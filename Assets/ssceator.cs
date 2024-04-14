using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ssceator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.A))
        {
            ScreenCapture.CaptureScreenshot(Application.streamingAssetsPath + "/" + System.DateTime.Now.ToString("hh-mm-ss") + ".png");
        }
    }
}
