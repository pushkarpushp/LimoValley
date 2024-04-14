using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssestBundleCreate : MonoBehaviour
{

    [MenuItem("Assets/Build AssetBundle")]
    static void ExportResource()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetFiles/", BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }

    [MenuItem("Menu/ClearData")]
    static void ClearData()
    {
        Caching.ClearAllCachedVersions("creation");
        Caching.ClearAllCachedVersions("playground");


    }
}