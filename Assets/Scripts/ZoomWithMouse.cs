using UnityEngine;
using Cinemachine;


public class ZoomWithMouse : MonoBehaviour
{
     private Vector3 originalPosition;



    void Start()
    {
        originalPosition = transform.position;
    }


    public CinemachineVirtualCamera virtualCamera;
    public float minRadius = 10f;
    public float maxRadius = 50f;


    void Update()
    {

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            float currentRadius = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition;
            float newRadius = currentRadius + scroll * 10;
            newRadius = Mathf.Clamp(newRadius, minRadius, maxRadius);
            virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = newRadius;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = originalPosition;
        }
    }
}
