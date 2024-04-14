using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;

public class PostData : MonoBehaviour
{
    //public TextMeshProUGUI DataText;
    private string Email;
    private string FirstName;
    private string LastName;
    private string Loginpassword;



    public GameObject email;
    public GameObject firstname;
    public GameObject lastname;
    public GameObject loginpassword;


    public GameObject login;
    public GameObject signup;
    public TMP_Text error;

    public void NewData()
    {
        StartCoroutine(Login(Email, FirstName, LastName, Loginpassword));
    }


    private void Update()
    {
        Email = email.GetComponent<InputField>().text;
        FirstName = firstname.GetComponent<InputField>().text;
        LastName = lastname.GetComponent<InputField>().text;
        Loginpassword = loginpassword.GetComponent<InputField>().text;

    }

    public IEnumerator Login(string Email, string FirstName, string LastName, string Loginpassword)
    {
        WWWForm form = new WWWForm();


        form.AddField("Email", Email);
        form.AddField("FirstName", FirstName);
        form.AddField("LastName", LastName);
        form.AddField("Password", Loginpassword);
        form.AddField("IAgree", "true");


        //  form.AddField("username", "28dec2020-1@yopmail.com");
        //form.AddField("password", "22222222");
        //form.AddField("grant_type", "password");



        UnityWebRequest www = UnityWebRequest.Post("https://www.eventcombo.com/API/v2/Account/Signup", form);
        // www.chunkedTransfer = false;

        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log(www.error);
            error.text = "Email already registered";
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            response2 res = JsonUtility.FromJson<response2>(www.downloadHandler.text);

            // Debug.Log(res.userName);
            // Debug.Log(res.expires_in);
            // if (res.userName = email){}

            if (res.Success == true)
            {
                // SceneManager.LoadScene("Loading");

                Switch();
            }

        }

    }
    public void Switch()
    {
        if (signup.activeSelf)
        {
            signup.SetActive(false);
        }
        else
        {
            signup.SetActive(true);
        }

        if (login.activeSelf)
        {
            login.SetActive(false);
        }
        else
        {
            login.SetActive(true);
        }

    }
    [Serializable]
    public class response2
    {

        public bool Success, Result;

    }
}



