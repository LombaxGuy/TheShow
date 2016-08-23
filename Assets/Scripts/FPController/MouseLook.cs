using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

    [SerializeField]
    [Range(0.1f, 5.0f)]
    float sensitivity = 5f;

    [SerializeField]
    float minimumX = -360f;
    [SerializeField]
    float maximumX = 360f;

    [SerializeField]
    float minimumY = -60f;
    [SerializeField]
    float maximumY = 60f;

    //The X and Y rotations are saved in lists in order to get the average rotations.
    //How many frames should we use to calculate the average?
    [SerializeField]
    float frameCounter = 20f;

    float rotationX = 0f;
    float rotationY = 0f;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0f;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0f;

    Quaternion originalRotation;
    Quaternion originalCamRotation;

    Camera cam;

	// Use this for initialization
	void Start ()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        cam = GetComponentInChildren<Camera>();

        //Freezes the rotation of the players rigidbody.
        if (rb)
            rb.freezeRotation = true;

        //Saves the original rotation of the player and camera.
        originalRotation = transform.localRotation;
        originalCamRotation = cam.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Unlock the cursor and make it visible when you hit escape.
        if(Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //Lock if left right mouse is clicked, the cursor isn't locked and the lockCursor bool is set to true.
        if(Input.GetKey(KeyCode.Mouse0) && Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        //Enable mouseLook when the cursor is locked.
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            rotAverageY = 0f;
            rotAverageX = 0f;

            //Adds the X and Y movements of the mouse multiplied by the sensitivity.
            rotationY += Input.GetAxis("Mouse Y") * sensitivity;
            rotationX += Input.GetAxis("Mouse X") * sensitivity;

            //Adds these rotations to lists.
            rotArrayY.Add(rotationY);
            rotArrayX.Add(rotationX);

            //Removes the first elements in the lists if the list.Count is larger than the frameCounter variable. 
            if (rotArrayY.Count >= frameCounter)
                rotArrayY.RemoveAt(0);

            if (rotArrayX.Count >= frameCounter)
                rotArrayX.RemoveAt(0);

            //Adds all the rotations in the lists together.
            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }

            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }

            //Gets the average rotation by dividing the sum by the amount of list elements.
            rotAverageY /= rotArrayY.Count;
            rotAverageX /= rotArrayX.Count;

            //Clamps the angle.
            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            //Converts the rotation from float to Quaternion.
            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            //Sets the player's Y rotation based on the averageX rotation.
            transform.localRotation = originalRotation * xQuaternion;

            //Sets the camera's X rotation based on the averageY rotation.
            cam.transform.localRotation = originalCamRotation * yQuaternion;
        }
	}

    /// <summary>
    /// Clamps an angle between desired minimum and maximum values.
    /// </summary>
    /// <param name="angle">Angle to clamp.</param>
    /// <param name="min">Minimum rotation.</param>
    /// <param name="max">Maximum rotation.</param>
    /// <returns>The clamped angle.</returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;


        if((angle >= -360f) && (angle <= 360f))
        {
            if (angle < -360f)
                angle += 360f;

            if (angle > 360f)
                angle -= 360f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
