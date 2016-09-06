using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private bool invertedMouseControls = false;

    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float horizontalMouseSensitivity = 5f;

    [SerializeField]
    [Range(0.1f, 5.0f)]
    private float verticalMouseSensitivity = 5f;

    [SerializeField]
    [Tooltip("The amount of degrees the player can look up.")]
    private float maxVerticalRotation = 80;

    [SerializeField]
    [Tooltip("The amount of degrees the player can look down.")]
    private float minVerticalRotation = -80;

    private Camera playerCamera;

    private float xAngles = 0;

    #region Events and EventHandlers
    void OnEnable()
    {
        // Subscribes to events
        EventManager.OnPlayerDeath += OnPlayerDeath;
        EventManager.OnPlayerRespawn += OnPlayerRespawn;
    }

    void OnDestroy()
    {
        // Unsubscribes from events
        EventManager.OnPlayerDeath -= OnPlayerDeath;
        EventManager.OnPlayerRespawn -= OnPlayerRespawn;
    }

    private void OnPlayerDeath()
    {
        this.enabled = false;
    }

    private void OnPlayerRespawn()
    {
        this.enabled = true;
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

#if (!DEBUG)
        Cursor.visible = false;
#endif

        xAngles = playerCamera.transform.rotation.eulerAngles.x;
    }

    private void HandleMouseInput()
    {
        // Getting mouse axis. "horizontal" is for rotation the player transform. "vertical" is for rotating the camera.
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        // If the controls are not inverted the vertical axis is multiplied with -1...
        if (!invertedMouseControls)
        {
            vertical *= -1;
        }
        //... Otherwise the horizontal axis is multiplied with -1.
        else
        {
            horizontal *= -1;
        }

        // Rotates the player horizontaly
        transform.Rotate(0, horizontalMouseSensitivity * horizontal, 0);

        // Adds to the xAngles
        xAngles += vertical * verticalMouseSensitivity;

        xAngles = Mathf.Clamp(xAngles, minVerticalRotation, maxVerticalRotation);

        playerCamera.transform.rotation = Quaternion.Euler(xAngles, playerCamera.transform.rotation.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.GetPauseState())
        {
            HandleMouseInput();
        }
    }
}
