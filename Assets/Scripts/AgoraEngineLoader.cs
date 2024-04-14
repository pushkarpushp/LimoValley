//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using agora_gaming_rtc;
//using agora_utilities;

//public class AgoraEngineLoader : MonoBehaviour
//{
//    private IRtcEngine mRtcEngine { get; set; }

//    public void loadEngine(string appId)
//    {
//        // start sdk
//        Debug.Log("initializeEngine");

//        if (mRtcEngine != null)
//        {
//            Debug.Log("Engine exists. Please unload it first!");
//            return;
//        }

//        // init engine
//        mRtcEngine = IRtcEngine.GetEngine(appId);

//        // enable log
//        mRtcEngine.SetLogFilter(LOG_FILTER.DEBUG | LOG_FILTER.INFO | LOG_FILTER.WARNING | LOG_FILTER.ERROR | LOG_FILTER.CRITICAL);
//    }

//    // unload agora engine
//    public void unloadEngine()
//    {
//        Debug.Log("calling unloadEngine");

//        // delete
//        if (mRtcEngine != null)
//        {
//            IRtcEngine.Destroy();  // Place this call in ApplicationQuit
//            mRtcEngine = null;
//        }
//    }

 
//}
