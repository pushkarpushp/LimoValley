using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICharacterSelectionScreen :Singleton<UICharacterSelectionScreen>
{
    //Screen to display
    public GameObject
        JoinRoomScreen,
        MissingNameScreen,
        SelctionButtonPanel, 
        SelectButton
        ;
    public InputField userName;
    void Start() 
    {
       // Back();
    }
    #region Functions
    public void AvatarScreen() // Register button
    {
        JoinRoomScreen.SetActive(true);
        SelectButton.SetActive(false);
        SelctionButtonPanel.SetActive(false);
    }
    public void Back() // Register button
    {
        JoinRoomScreen.SetActive(false);
        SelectButton.SetActive(true);
        SelctionButtonPanel.SetActive(true);
    }
    public void EnableMissingScreen()
    {
        MissingNameScreen.SetActive(true);
    }
    public void DisableMissingScreen()
    {
        MissingNameScreen.SetActive(false);
    }
    public void OKButtonMissingScreen()
    {
        DisableMissingScreen();
    }
    public void DisableRoomScreen()
    {
        JoinRoomScreen.SetActive(false);
    }
    #endregion Functions
}
