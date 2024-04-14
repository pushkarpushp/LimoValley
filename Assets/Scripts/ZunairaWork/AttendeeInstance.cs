using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class AttendeeInstance : MonoBehaviour
{
    private void Start()
    {
        foreach (Player p in PhotonNetwork.PlayerList)
        {
                Debug.Log((int)(p.CustomProperties["PlayerProfile"])+" "+p.NickName);
        }
    }
    #region UserProfile
    public void ShowUserProfile()
    {
        int value = 0;
        string PlayerName = gameObject.transform.GetChild(0).GetComponent<Text>().text;
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            if (p.NickName == PlayerName)
            {
                Debug.Log(PlayerName);
                Debug.Log((int)(p.CustomProperties["PlayerProfile"]));
                value = (int)(p.CustomProperties["PlayerProfile"]);
            }
        }
        Debug.Log(" in Tools : " + value);
        StartCoroutine(Get_Profile(value));
        // StartCoroutine(Get_Profile(PlayerPrefs.GetInt("virtualEventUserId")));
    }

    public IEnumerator Get_Profile(int selectedPlayerEventId)
    {
        string apiUrl = "https://seett.eventcombo.com/public/profile/" + encoding(selectedPlayerEventId.ToString());
        Debug.Log("DUS : " + apiUrl);
        Application.OpenURL(apiUrl);
        UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(apiUrl);
        yield return www.SendWebRequest();
        if (www.error == null)
            Debug.Log(www.downloadHandler.text);
        else
            Debug.Log(www.error);

    }

    public string encoding(string toEncode)
    {
        byte[] bytes = System.Text.Encoding.GetEncoding(28591).GetBytes(toEncode);
        string toReturn = System.Convert.ToBase64String(bytes);

        Debug.Log(toReturn + " .. toReturn");
        return toReturn;
    }


    #endregion
}
