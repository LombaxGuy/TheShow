using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    enum MoveMode { PLAYERTOUCHONLY, ENDLESS, PLAYERTOUCHEXTRA, PLAYERTOUCHSPECIAL}

    //GameObject Y scale need to be 1, since it will mess til Player controller 
    [SerializeField]
    private float speed = 2;
    [Tooltip("When the Player is not on the platform(jumping or falling), it will make a stop for a time if true")]
    [SerializeField]
    private bool smallBreak = false;
    [SerializeField]
    private float distance = 1;
    [SerializeField]
    private bool locationOneFirst = true;

    [SerializeField]
    private float timeDelay = 1;

    [Header("Target Positions")]
    [SerializeField]
    private GameObject targetPositionOne;

    [SerializeField]
    private GameObject targetPositionTwo;

    private bool isMovingWithPlayer = false;

    private bool playerGoldenTouch = false;

    private bool secondPlatformReached = false;

    private float counter = 0;
    [Tooltip("How the Platform will move. Endlessis for platform moving on its own. PlayerTouchOnly is for platform move when player on it and goes back when player not on it. PlayerTouchExtra is for move when player touch and continue to move location B and then location A and it will stop.")]
    [SerializeField]
    private MoveMode moveMode;

    private void Update()
    {

        switch (moveMode)
        {
            case MoveMode.PLAYERTOUCHONLY:
                PlayerTouchOnly();
                break;
            case MoveMode.ENDLESS:
                EndlessLoop();
                break;
            case MoveMode.PLAYERTOUCHEXTRA:
                PlayerTouchExtra();
                break;
            case MoveMode.PLAYERTOUCHSPECIAL:
                PlayerTouchSpecial();
                break;
            default:
                break;
        }

        if (counter >= 0)
        {
            counter -= Time.deltaTime;
        }
    }


    private void EndlessLoop()
    {
        if (Counter() == true)
        {
            GameObjectMove();
        }
        
    }

    private void PlayerTouchOnly()
    {
        if(Counter() == true)
        {
            if (isMovingWithPlayer == true)
            {
                GameObjectMoveOnPlayerTouch();
            }
            else
            {
                GameObjectMoveBack();
            }
        }
    }

    private void PlayerTouchExtra()
    {
        if(Counter() == true)
        {
            if(playerGoldenTouch == true)
            {
                if (locationOneFirst == false && secondPlatformReached == true)
                {
                    playerGoldenTouch = false;
                    secondPlatformReached = false;
                }
                else
                {
                    if(locationOneFirst == true)
                    {
                        secondPlatformReached = true;
                    }

                    GameObjectMove();
                }

                
            }
        }
    }

    private void PlayerTouchSpecial()
    {
        if(Counter() == true)
        {
            if (playerGoldenTouch == true)
            {
                if (locationOneFirst == true && isMovingWithPlayer == false)
                {
                    playerGoldenTouch = false;
                    secondPlatformReached = false;
                }
                else
                {
                    if (locationOneFirst == true)
                    {
                        secondPlatformReached = true;
                    }
                    if(secondPlatformReached == false)
                    {
                        GameObjectMove();
                    }
                    

                }


            }else
            {
                if(locationOneFirst == true)
                {
                    GameObjectMove();
                }
                
            }

            
        }
    }

    private bool Counter()
    {
        if(counter <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Transform other = collision.collider.transform;

        if (other.parent != null)
        {
            if (other.parent.tag == "Player")
            {
                if (other.parent.GetComponent<FPSController>().OnGround)
                {
                    other.parent.transform.parent = transform;

                    SetTime(0);

                    isMovingWithPlayer = true;

                    playerGoldenTouch = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Transform other = collision.collider.transform;

        if (other.parent != null)
        {
            if (other.parent.tag == "Player")
            {
                other.parent.transform.parent = null;

                SetTime(timeDelay);

                isMovingWithPlayer = false;
            }
        }
    }

    /// <summary>
    /// If move gameobject only when player touch, Then it will move to the distination and then stop moving.
    /// </summary>
    private void GameObjectMoveOnPlayerTouch()
    {

        if (Distance(false) > distance)
        {
            Move(false);
        }
    }


    private void SetTime(float time)
    {
        if(smallBreak == true)
        {
            counter = time;
        }       
    }

    /// <summary>
    /// If the player is not touching it, it will go back to start location
    /// </summary>
    private void GameObjectMoveBack()
    {
        if (Distance(true) > distance)
        {
            Move(true);
        }
    }


    /// <summary>
    /// Move the gameObject
    /// </summary>
    /// <param name="direction"></param>
    private void Move(bool direction)
    {
        if (direction == true)
        {
            transform.Translate((targetPositionOne.transform.position - transform.position).normalized * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate((targetPositionTwo.transform.position - transform.position).normalized * Time.deltaTime * speed);
        }
    }


    /// <summary>
    /// Gets the distance between this gameObject and on of the location gameobjects, and return it as float.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private float Distance(bool direction)
    {
        float distanceBetween = 0;

        if (direction == true)
        {
            distanceBetween = Vector3.Distance(targetPositionOne.transform.position, transform.position);
        }
        else
        {
            distanceBetween = Vector3.Distance(targetPositionTwo.transform.position, transform.position);
        }

        return distanceBetween;
    }


    /// <summary>
    /// Will make the GameObject move towards empty GameObject
    /// </summary>
    private void GameObjectMove()
    {

        if (locationOneFirst == true)
        {
            Move(true);
            if (Distance(true) < distance)
            {
                locationOneFirst = false;
                SetTime(timeDelay);
            }
        }
        else
        {
            Move(false);
            if (Distance(false) < distance)
            {
                locationOneFirst = true;
                SetTime(timeDelay);
            }
        }
    }
}
