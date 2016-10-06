using UnityEngine;
using System.Collections;

public class PlatformExtender : MonoBehaviour {

    private bool isMoving;
    private bool IsOut = false;


    [SerializeField]
    [Tooltip("How far this extends")]
    private float distance = 5;
    [SerializeField]
    [Tooltip("Type Right for right left and Forward for back forward movement")]
    private string direction;

    private Vector3 directionVector;

    [SerializeField]
    [Tooltip("How fast this extends")]
    private float pushOutTime = 0.2f;


    private Vector3 currenPos;
    private Vector3 startPos;
    private Vector3 extPos;

    [SerializeField]
    private AudioClip audioClip;

    private AudioSource audioPlayer;

    // Use this for initialization
    void Start () {
        startPos = transform.position;
        IsOut = false;

        audioPlayer = GetComponent<AudioSource>();

        switch(direction)
        {
            case "Right":
                directionVector = transform.right;
                break;
            case "Forward":
                directionVector = transform.forward;
                break;
        }

        extPos = transform.position + directionVector * distance;
    }
	
	// Update is called once per frame
	void Update () {


        currenPos = transform.position;
	}

    public void Extend()
    {
        Move(startPos, extPos, pushOutTime);
        IsOut = true;
        audioPlayer.PlayOneShot(audioClip);
    }

    public void Substract()
    {
        Move(currenPos, startPos, pushOutTime);
        IsOut = false;
    }

    /// <summary>
    /// Coroutine for the movement of the object
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    /// <returns></returns>
    private IEnumerator CoroutineMove(Vector3 from, Vector3 to, float timeInSeconds)
    {
        isMoving = true;
        float t = 0;

        while (t < 1)
        {
            transform.position = Vector3.Lerp(from, to, t);

            t += Time.deltaTime / timeInSeconds;

            yield return null;
        }


        transform.position = Vector3.Lerp(from, to, t);
        isMoving = false;
        
    }

    /// <summary>
    /// Coroutine for the movement of the object
    /// </summary>
    /// <param name="from">The vector used as start position</param>
    /// <param name="to">The vector we want to move to</param>
    /// <param name="timeInSeconds">The time we want the move action to take</param>
    private void Move(Vector3 from, Vector3 to, float timeInSeconds)
    {

        StartCoroutine(CoroutineMove(from, to, timeInSeconds));

    }
}
