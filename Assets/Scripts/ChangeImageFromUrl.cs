

    using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class ChangeImageFromUrl : MonoBehaviour
{
    public string imageUrl;
    public RawImage[] targetImages;

    private void Start()
    {
        imageUrl = Javascripthooks.Instance.ImageURL;
        StartCoroutine(LoadImages());
        Debug.Log("imageUrl : " + imageUrl);

    }

    IEnumerator LoadImages()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            foreach (RawImage image in targetImages)
            {
                image.texture = texture;
            }
        }
        else
        {
            Debug.Log("Error loading image: " + request.error);
        }
    }
}


