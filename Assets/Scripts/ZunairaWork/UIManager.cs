using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Vimeo.Player;

public class UIManager : Singleton<UIManager>
{
    
    public Text ModeText, MessageNotify;
    public GameObject
        OptionPanel,
        ControlPanel,
        VoiceChatPanel,
        EmojiPanel,
        TextChatPanel,
        GlobalChatPanel,
        MapPanel,
        InstructionPanel,
        CloseEmojiButton,
        CloseControlButton,
        OpenVideoButton,
        CloseVideoButton,
        CloseOptionButton,
        CLoseTextChatButton,
        UnmuteButtonChat,
        MicParent,
        VoiceChatParent,
        VoiceChatInstance,
        chneltest,
        PersonalTextChatPanel,
        PersonalTextChatParent,
        PersonalTextChatInstance,
        AttendeePanel,
        DocumentPanel,
        AttendeeInstance,
        AttendeeParent,
        MuteAllVoiceChatButton,
        NoticePanel,
        ButtonForAttendy,
        PlayButtonVimeo, PauseButtonVimeo,
        FullScreenCrossButton,
        FullScreenPlayButton,
        FullScreenPauseButon,
        FullScreenVideo,
        VimeoCanvasPanelRef,
        TheaterVideoRef;
    public InputField
        searchBarChat,
        searchBarVoiceChat,
        searchBarAttende;
    public Transform privatechatparent;
    public Transform publicChatParent;
    public GameObject 
        leftPrivateMsgs,
        rightPrivateMsg;
    public Transform PrivateChatInstantiateParent;
   // public VimeoPlayer vimeoPlayer;
    public bool
       isInteract =true;
    public GameObject _MuteButtonRight, _UnMuteButtonRight;

    public List<GameObject> MsgsCLone = new List<GameObject>();
    //public GameObject
    private PhotonView photonView;
    public AudioClip messageNotifyClip;

    [Header("===== for Video Access =====")]

    public GameObject SettingPanel;
    public GameObject SettingButton, TheaterVideo;
    public Toggle isFullScreenPlay;
    bool isOk; 
    public GameObject RefreshVideo;
    private void Start()
    {
       photonView = GetComponent<PhotonView>();

        Debug.Log(" This is start ");

        StartCoroutine(CheckForInternet());
    }
    public void PLayViodeo(bool isTrue, bool isfullScreen)
    {
        Debug.Log(" This is PLayViodeo ");

        if (PlayerPrefs.GetString("UserRole").Contains("cohost") || PlayerPrefs.GetString("UserRole").Contains("host"))
        {
            FullScreenCrossButton.SetActive(true);
            FullScreenPlayButton.SetActive(true);
            FullScreenPauseButon.SetActive(true);
        }
        if (isfullScreen)
        {
            FullScreenVideo.SetActive(true); FullScreenVideo.GetComponent<RawImage>().enabled = true;
        }
        else
        { 
            TheaterVideo.SetActive(true);
            TheaterVideoRef.transform.GetChild(0).gameObject.SetActive(true);

        }

        Debug.Log(" isTrue: " + isTrue);
        Debug.Log(" isfullScreen: " + isfullScreen);
        if (isTrue)
        {
            Debug.Log(" in true play ");

            if (isfullScreen)
                VimeoCanvasPanelRef.GetComponent<VimeoPlayer>().Play();
            else
            {
                if ((PhotonNetwork.LocalPlayer.IsLocal)&&(
                    GamePlayManager.Instance.Player.GetComponent<PlayerSelection>().isVIdeoPlaying 
                   // &&(PlayerPrefs.GetString("UserRole").Contains("cohost") ||
                     // PlayerPrefs.GetString("UserRole").Contains("host"))
                      ))
                {
                    TheaterVideoRef.GetComponent<VimeoPlayer>().Play();
                    FullScreenVideo.GetComponent<RawImage>().enabled = false;
                    FullScreenVideo.SetActive(true);

                }

            }
        }
        else 
        {
            VimeoCanvasPanelRef.GetComponent<VimeoPlayer>().Pause();
            TheaterVideoRef.GetComponent<VimeoPlayer>().Pause();
        }
    }

    public void CLoseforHostOnly()
    {
        VimeoCanvasPanelRef.GetComponent<VimeoPlayer>().Pause();
        TheaterVideoRef.GetComponent<VimeoPlayer>().Pause();
        TheaterVideoRef.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void CloseVimeo()
    {
        VimeoCanvasPanelRef.GetComponent<VimeoPlayer>().Pause();
        TheaterVideoRef.GetComponent<VimeoPlayer>().Pause();

        print("sjhdgfhjsdf");
        FullScreenCrossButton.SetActive(false);
        FullScreenPlayButton.SetActive(false);
        FullScreenPauseButon.SetActive(false);
        FullScreenVideo.SetActive(false);

        TheaterVideoRef.transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && !isInteract)
        {
            EscapeKey();
            InteractUI();
        }
        else
        if (Input.GetKeyDown(KeyCode.Tab) && isInteract)
        {
            EscapeKey();
            FollowCam();
        }
    }

    private void FixedUpdate()
    {
        if (!IsPointerOverUIElement()  && Input.GetMouseButtonDown(0))
        {
            Debug.Log("in If clicked ");
            ClosePreviousPanelsAndButtons();
        }
       /* else
        {
            Debug.Log("in else clicked");
        }*/
    }
  public void NotfyMsgFucntiion()
    {
        for (int i = 0; i < UIManager.Instance.MsgsCLone.Count; i++)
        {
            UIManager.Instance.PrivateChatInstantiateParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < UIManager.Instance.MsgsCLone.Count; i++)
        {
            if (UIManager.Instance.MsgsCLone[i].GetComponent<PrivateChatMessenger>().targetName == UIManager.Instance.MessageNotify.text)
            {
                UIManager.Instance.MsgsCLone[i].gameObject.SetActive(true);
                break;
            }
        }
    }

    #region VideoSettingBtn
    public void OnVideoSettingChangeToggle()
    {
        // isFullScreenPlay = !isFullScreenPlay;
        Debug.Log("IS ON :"+isFullScreenPlay.isOn);
        if ((photonView.IsMine) &&
                (PlayerPrefs.GetString("UserRole").Contains("cohost") ||
                PlayerPrefs.GetString("UserRole").Contains("host")))
        {
            photonView.RPC("ChangeVideoToggle", RpcTarget.All, isFullScreenPlay.isOn);
        }
    }

    [PunRPC]
    void ChangeVideoToggle(bool isvideofull)
    {
        isFullScreenPlay.isOn = isvideofull;
    }
    #endregion
    #region SearchBar
    public void SearchName(string valueIn,InputField givenText,Transform Parent)
    {
        givenText.text = valueIn;
        for(int i = 0; i < Parent.transform.childCount; i++)
        {
            if (Parent.transform.GetChild(i).transform.GetChild(0).GetComponent<Text>().text.Contains(valueIn))
            {
                Parent.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Parent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public void TypeValueChangedChat(string valueIn)
    {
        SearchName(valueIn, searchBarChat, PersonalTextChatParent.transform);
    }
    public void TypeValueChangedVoice(string valueIn)
    {
        SearchName(valueIn, searchBarVoiceChat, VoiceChatParent.transform);
    }
    public void TypeValueChangedAttende(string valueIn)
    {
        SearchName(valueIn, searchBarAttende,AttendeeParent.transform);
    }
    #endregion SearchBar
    #region Mouse
    public void OnMouseDown()
    {
        Debug.Log("hereeeeeeee" + IsPointerOverUIElement());
        if (!IsPointerOverUIElement())
        {
            ClosePreviousPanelsAndButtons();
        }
    }
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }
        return false;
    }
    ///Gets all event systen raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
    #endregion Mouse
    #region Button
    public void OpenEmojiPanel()
    {
        ClosePreviousPanelsAndButtons();
        EmojiPanel.SetActive(true);
        CloseEmojiButton.SetActive(true);

    }
    public void CloseEmojiPanel()
    {
        EmojiPanel.SetActive(false);
        CloseEmojiButton.SetActive(false);
    }
    public void OpenOptionalPanel()
    {
        ClosePreviousPanelsAndButtons();
        OptionPanel.SetActive(true);
        CloseOptionButton.SetActive(true);
    }
    public void CloseOptionalPanel()
    {
        OptionPanel.SetActive(false);
        CloseOptionButton.SetActive(false);
    }
    public void OpenControlPanel()
    {
        ClosePreviousPanelsAndButtons();
        ControlPanel.SetActive(true);
        CloseControlButton.SetActive(true);
    }
    public void CloseControlPanel()
    {
        ControlPanel.SetActive(false);
        CloseControlButton.SetActive(false);
    }
    public void OpenVoiceChatPanel()
    {
        ClosePreviousPanelsAndButtons();
        VoiceChatPanel.SetActive(true);
    }
    public void CloseVoiceChatPanel()
    {
        VoiceChatPanel.SetActive(false);
        
    }
    public void OpenTextChatPanel()
    {
        ClosePreviousPanelsAndButtons();
        TextChatPanel.SetActive(true);
        CLoseTextChatButton.SetActive(true);
    }
    public void CloseTextChatPanel()
    {
        TextChatPanel.SetActive(false);
        CLoseTextChatButton.SetActive(false);
       // CloseGlobalChatPanel();
    }
    public void OpenGlobalChatPanel()
    {
        GlobalChatPanel.SetActive(true);
    }
    public void CloseGlobalChatPanel()
    {
        GlobalChatPanel.SetActive(false);
    }
    
    
    public void MuteAllChat()
    {
        SetChild(MicParent, false);
        UnmuteButtonChat.SetActive(true);
    }
    public void UnMuteChat()
    {
        SetChild(MicParent, true);
        UnmuteButtonChat.SetActive(false);
    }
    public void OpenMap()
    {
        MapPanel.SetActive(true);
    }
    public void CloseMap()
    {
        MapPanel.SetActive(false);
    }
    public void OpenVideoPanel()
    {
        ClosePreviousPanelsAndButtons();
        CloseVideoButton.SetActive(true);
    }
    public void CloseVideoPanel()
    {
        ClosePreviousPanelsAndButtons();
        CloseVideoButton.SetActive(false);
    }
    public void HandleAttendiPanel(bool isEnable)
    {
        AttendeePanel.SetActive(isEnable);
    }
    public void HandleDocumentPanel(bool isEnable)
    {
        DocumentPanel.SetActive(isEnable);
    }
    public void OpenSettingPanel(bool isEnable)
    {
        SettingPanel.SetActive(isEnable);
    }
    public void CloseSettingPanel()
    {
        ClosePreviousPanelsAndButtons();
        SettingPanel.SetActive(false);
    }
    public void DownloadDocumentPanel(bool isEnable)
    {
        Application.OpenURL("https://seett.eventcombo.com/eventiverse/pdfs/FundamentalsOfProgramming.pdf");
    }
    public void ClosePersonalTextPanel()
    {
        PersonalTextChatPanel.SetActive(false);
    }
    public void FollowCam()
    {
        isInteract = false;
        Cursor.lockState = CursorLockMode.Locked;
        GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
        GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorLocked = true;
        GamePlayManager.Instance.Player.GetComponent<PlayerInput>().enabled = true;
        ModeText.text = "Walk Mode";
    }

    public void InteractUI()
    {
        isInteract = true;
        Cursor.lockState = CursorLockMode.None;
        if (GamePlayManager.Instance.Player.GetComponent<PhotonView>().IsMine)
        {
            GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorInputForLook = false;
            GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            GamePlayManager.Instance.Player.GetComponent<PlayerInput>().enabled = false;
            ModeText.text = "Interactive Mode";
        }
    }
    public void EscapeKey()
    {
        Cursor.lockState = CursorLockMode.None;
        if (GamePlayManager.Instance.Player.GetComponent<PhotonView>().IsMine)
        {
            GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorInputForLook = false;
            GamePlayManager.Instance.Player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            GamePlayManager.Instance.Player.GetComponent<PlayerInput>().enabled = true;
            ClosePreviousPanelsAndButtons();
        }
    }

    #endregion Button
    #region CommonButton
    private void ClosePreviousPanelsAndButtons()
    {
        TextChatPanel.SetActive(false);
        OptionPanel.SetActive(false);
        ControlPanel.SetActive(false);
        VoiceChatPanel.SetActive(false);
        EmojiPanel.SetActive(false);
        TextChatPanel.SetActive(false);
        GlobalChatPanel.SetActive(false);
        CloseEmojiButton.SetActive(false);
        CloseControlButton.SetActive(false);
        CloseOptionButton.SetActive(false);
        CLoseTextChatButton.SetActive(false);
        CloseVideoButton.SetActive(false);
        DocumentPanel.SetActive(false);
        CloseMap();
        AttendeePanel.SetActive(false);

        SettingPanel.SetActive(false);


        for (int i = 0; i < MsgsCLone.Count; i++)
        {
            MsgsCLone[i].SetActive(false);
        }
        PersonalTextChatPanel.SetActive(false);

    }
    private void SetChild(GameObject parent, bool status)
    {
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (status)
            {
                parent.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(true);
            }
            else
            {
                parent.transform.GetChild(i).transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }
    public void HideGameObject()
    {
        InstructionPanel.SetActive(false);
    }
    #endregion CommonButton
    #region VoiceChatManagerButton
    public void MuteMic()
    {
      //  VoiceChatManager.Instance.Mute();
    }
    public void UnmuteMic()
    {
      //  VoiceChatManager.Instance.UnMute();
    }
    #endregion VoiceChatManagerButton
    #region Coroutines
    IEnumerator CheckForInternet()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            SetNoticeText("Your internet connection is unstable. Please refresh.");
            // SetGameInfoText("Internet disconnected, trying to reconnect.");
            StartCoroutine(CheckForInternet());
        }
        else
        {
            Time.timeScale = 1;
            bool canConnect = PhotonNetwork.ReconnectAndRejoin();
            //Debug.Log($"CheckForInternet : {canConnect}");

            if (!canConnect)
            {
                if (!PhotonNetwork.IsConnected)
                {
                    PhotonNetwork.ConnectUsingSettings();
                }
            }

        }
    }

    public TMPro.TMP_Text NoticeTxt;
    void SetNoticeText(string msg)
    {
        NoticeTxt.text = msg;
        NoticePanel.SetActive(true);
    }
    #endregion Coroutines
}

