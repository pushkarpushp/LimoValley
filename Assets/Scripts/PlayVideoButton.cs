using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideoButton : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Button;

    private void Start()
    {
        

        // Get the Button component from the same GameObject
        Button button = GetComponent<Button>();

        // Add a listener to the button to call the PlayVideo method when it's clicked
        button.onClick.AddListener(PlayVideo);
    }

    private void PlayVideo()
    {
        // Play the video
        videoPlayer.Play();
        Button.SetActive(false);
    }
}
