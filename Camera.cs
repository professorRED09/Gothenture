using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera : MonoBehaviour, IObserver
{
    [Header("Reference")]
    [SerializeField] Subject playerInteract;
    [SerializeField] Subject dialogueManager;
    [SerializeField] Subject borderStart;
    [SerializeField] Subject borderLeft;
    CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject player;

    [Header("Settings")]
    public float zoomSpeed = 2f;  // Speed of zooming
    public float minZoom = 3f;    // Minimum zoom level
    public float maxZoom = 10f;   // Maximum zoom level

    private float targetZoom;     // Target zoom value for smooth transition
    private bool isZooming = false;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera == null)
        {
            Debug.LogError("Cinemachine Virtual Camera is not assigned!");
            return;
        }

        targetZoom = virtualCamera.m_Lens.OrthographicSize;

        //virtualCamera.Follow = null;
    }

    void Update()
    {
        // Zoom in when the player presses the button
        //if (Input.GetKeyDown(KeyCode.Z))
        //{
        //    isZooming = true;
        //    targetZoom = Mathf.Max(minZoom, targetZoom - zoomSpeed);
        //}

        // Zoom out when the player presses the button
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    isZooming = true;
        //    targetZoom = Mathf.Min(maxZoom, targetZoom + zoomSpeed);
        //}

        // Smoothly adjust the camera zoom
        if (isZooming)
        {
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(
                virtualCamera.m_Lens.OrthographicSize,
                targetZoom,
                Time.deltaTime * zoomSpeed
            );

            if (Mathf.Abs(virtualCamera.m_Lens.OrthographicSize - targetZoom) < 0.01f)
            {
                isZooming = false;
            }
        }
    }

    void ToggleCamTrack()
    {
        if (virtualCamera.Follow == null)
        {
            virtualCamera.Follow = player.transform; 
        }
        else
        {
            virtualCamera.Follow = null;
        }

        //virtualCamera.Follow = player.transform;
        //virtualCamera.enabled = true;
    }

    public void OnNotify(PlayerAction action)
    {
        switch (action)
        {
            case (PlayerAction.Talk):
                print("Talk with NPC");
                isZooming = true;
                targetZoom = Mathf.Max(minZoom, targetZoom - zoomSpeed);
                return;

            case (PlayerAction.Leave):
                print("leave after the talk");
                isZooming = true;
                targetZoom = Mathf.Max(minZoom, targetZoom + zoomSpeed);
                return;

            case (PlayerAction.ToggleCamTrack):
                ToggleCamTrack();
                return;

            //case (PlayerAction.Enter):
            //    virtualCamera.enabled = true;
            //    return;

            //case (PlayerAction.Out):
            //    virtualCamera.enabled = false;
            //    return;

            default:
                return;
        }
    }

    void OnEnable()
    {
        playerInteract.AddObserver(this);
        dialogueManager.AddObserver(this);
        borderStart.AddObserver(this);
        borderLeft.AddObserver(this);
    }

    void OnDisable()
    {
        playerInteract.RemoveObserver(this);
        dialogueManager.RemoveObserver(this);
        borderStart.RemoveObserver(this);
        borderLeft.RemoveObserver(this);
    }
}

