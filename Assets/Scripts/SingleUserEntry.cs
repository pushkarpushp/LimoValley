using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class SingleUserEntry : MonoBehaviour
{
    private const string UNIQUE_ID_KEY = "unique_id_";
    private const string USER_ENTERED_KEY = "user_entered_";
    private string url;
    void Start()
    {
 // Get the current URL
 url = Application.absoluteURL;
 // Check if the user has already entered
 if (PlayerPrefs.HasKey(USER_ENTERED_KEY + url))
        {
 // If they have, check if the unique ID stored in PlayerPrefs matches the current user's unique ID
 string storedUniqueId = PlayerPrefs.GetString(UNIQUE_ID_KEY + url);
            if (storedUniqueId != SystemInfo.deviceUniqueIdentifier)
            {
 // If it doesn't match, the user is not allowed to enter
 Debug.LogError("Only one user is allowed to enter per link.");
                return;
            }
        }
        else
        {
 // If the user hasn't entered before, store their unique ID in PlayerPrefs
 PlayerPrefs.SetString(UNIQUE_ID_KEY + url, SystemInfo.deviceUniqueIdentifier);
            PlayerPrefs.SetInt(USER_ENTERED_KEY + url, 1);
        }
 // If the user is allowed to enter, continue with the rest of your code here
}
}