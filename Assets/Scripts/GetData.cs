using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
using Random = UnityEngine.Random;
/// <summary>
/// note: audio video setting
/// 
/// </summary>
public class GetData : MonoBehaviour
{
    private string EventId;
    private string AccessToken;
    private string Session;

  
    public string SetApi;
    public TextAsset textJson;
    public TMP_Text warning;

    public static long sessionId;

    public GameObject TimerButton;

   

    //GetUrl data
    public string FirstNameUrl, LastnameUrl, FullNameUrl, RoleURl;

    [Serializable]

    public class Api
    {
        public string CurrentApi;
    }

   
    
    void Start()
    {
   // PlayerPrefs.SetString("UserRole", "host");
       //   PlayerPrefs.SetString("UserRole", "");
        //Caching.ClearCache();
        //Caching.ClearAllCachedVersions("creation");
        //Caching.ClearAllCachedVersions("playground");

        Api NewApi = JsonUtility.FromJson<Api>(textJson.text);
        SetApi = Javascripthooks.Instance.baseURL + NewApi.CurrentApi;

#if !UNITY_EDITOR
        string Weblink = Application.absoluteURL;
#else
        string Weblink = "https://seett.eventcombo.com/eventiverse/31188/13e4cbc6-6363-4622-8627-052d389eb064";
        //string Weblink = "https://seett.eventcombo.com/eventiverse/31188/0e5dfedd-5f14-4b3e-966a-25a965669f7c%22";
        //  "https://seett.eventcombo.com/eventiverse/30987/4f65103e-07f0-4f0c-aec4-0bfe7e8193a4";
        SetApi = "https://seett.eventcombo.com" + NewApi.CurrentApi;
#endif

        string[] splitArray = Weblink.Split(char.Parse("/"));
        //foreach (string s in splitArray)
        //{
        //    Debug.LogError(s);
        // //   print(s);

        //}
        for (int i = 0; i < splitArray.Length; i++)
        {
            EventId = splitArray[4];
            AccessToken = splitArray[5];
           
        }
    // Session = Guid.NewGuid().ToString();
     Session = Javascripthooks.Instance.Number.text;
        NewData();
        //getUniqueUser(Weblink);

    }
    public void NewData()
    {
        StartCoroutine (Login(EventId, AccessToken, Session));
        Debug.Log(sessionId);
    }



    public bool isValidtoken;
    public IEnumerator Login( string EventId, string AccessToken, string Session)
    {
        //  string url = "https://seett.eventcombo.com/ng-api/fireworks/VirtualEvent/GetVirtualEventUserDetail";
        string url =  SetApi;
        string apiURl = string.Format("{0}?eventId={1}&accessToken={2}&browserSessionId={3}", url, EventId, AccessToken, Session);
        Debug.LogError(url);
        Debug.Log(url);
        Debug.Log("===========");
        Debug.Log(apiURl);
        Debug.Log("===========");

        UnityWebRequest www = UnityWebRequest.Get(apiURl);
            yield return www.SendWebRequest();

        root res = JsonUtility.FromJson<root>(www.downloadHandler.text);
        Debug.Log(www.downloadHandler.text);
        if (www.error != null)
            {
                Debug.Log(www.error);
            FindObjectOfType<DownloadAssetBundles>().percentageTxt.text = "The link is already in use by another attendee.";


        }
        else if (res.StatusCode == 200 )
        {
            Debug.Log(www.downloadHandler.text);
               
            if (res.StatusCode==200)
            {
                if (res.Data.TicketOrderId != null)
                {
                    Debug.Log("pass");

                    FullNameUrl = res.Data.FullName;
                     FirstNameUrl= res.Data.FirstName;
                    LastnameUrl = res.Data.LastName;
                    RoleURl = res.Data.UserRole;
                    PlayerPrefs.SetString("UserName",  res.Data.FullName.ToString());
                   PlayerPrefs.SetString("UserRole", res.Data.UserRole.ToString());
                    PlayerPrefs.SetString("UserFirstName", res.Data.FirstName.ToString());
                    PlayerPrefs.SetString("UserLastName", res.Data.LastName.ToString());
                    PlayerPrefs.SetInt("virtualEventUserId", res.Data.VirtualEventUserId);
                    print(PlayerPrefs.GetString("UserName"));
                    print(PlayerPrefs.GetString("UserRole"));
                    Debug.Log("yes it is OK to load");

                    if(RoleURl == "host"|| RoleURl == "cohost")
                    {
                        TimerButton.SetActive(true);
                    }
                    else { TimerButton.SetActive(false); }

                }

                
            }

            else if(res.StatusCode == 400)
            {
                warning.text = "The link is already in use by another attendee.";
            }


           }
        
    }

    private const string UNIQUE_ID_KEY = "unique_id_";
    private const string USER_ENTERED_KEY = "user_entered_";
    void getUniqueUser(string url)
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

            Debug.Log("YES ITS UNIQUE.");
            NewData();
        }
 // If the user is allowed to enter, continue with the rest of your code here
 }
}

[Serializable]
public class root
{
    public string Version;
    public int StatusCode;
    public main Data;
}

    [Serializable]
public class main    
{


    public bool IsOnline, IsEmailEnterRequired;
    public int VirtualEventUserId, Phone;
    public string UserId, UserRole, FirstName, LastName, FullName, ProfileImageUrl, Email, EventAccessToken, LastLoginIpAddress, TicketOrderId,
                    Title, Description, UserProfileImage, UserProfileImageName, Password;

    public VirtualEvent VirtualEvent;
   
}

[Serializable]
public class VirtualEvent
{
    public int EventId, VirtualEventId, OrganizerId, TimeZoneId;
    public bool EnableBreakoutRooms, EnableRoomJumping, EnableSilentAuction;
    public string OrganizerName, ChannelName, EventTitle, EventUrl, EventStartDateUtc, EventEndDateUtc, Image, BoardBackgroundColor, BoardTextColor;

    public Rooms[] Rooms;

}

[Serializable]
public class Rooms
{
    public int VirtualEventRoomId, EventId;
    public string RoomName, RoomType, Description, MediaType, UserId, StreamIdVideoSource, VideoUrl, CreatedDateUtc,
                  UpdatedDateUtc, RoomImage, VirtualEventScheduleId, GrandSponsorId, GrandSponsorUrl,
                   GrandSponsorImageId, ParentId, GrandSponsorName, BoardBackgroundColor, BoardTextColor,
                    BackgroundImage, WhoCanJoin, TicketIds, DisplayOrder;
    public bool AllowGroupChat, AllowHandouts, AllowPrivateChat, AllowPollsAndSurvey, AllowGuestInvite,
                AllowOneToOneNetworking, AllowRecording, ShowBoothWhenTicketNotMatched, ShowBackgroundImage,
                EnableAttendeeView, ShowSpeakers, ShowSponsors, ShowSchedules, IsAllowedToJoinRoom;


}


