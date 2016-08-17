using UnityEngine;
using System.Collections;

public class ConveyorBelt : MonoBehaviour {


    enum Direction { FORWARD, BACK, LEFT, RIGHT, UP, DOWN}

    enum ConveyorBeltMode { SIMPLE, ADVANCED}

    [Header("Change the direction")]
    [SerializeField]
    private Direction dir = Direction.RIGHT;
    [Header("Change conveyor mode")]
    [SerializeField]
    private ConveyorBeltMode CBM = ConveyorBeltMode.SIMPLE;

    //Hvis det er spilleren der skal aktivere lortet, så bevæger gameobjects sig ikke. Fordi de er åbentbart faldet i søvn. Find en måde og vække op rigidbodies på conveyor belt.
    //AddForce er en ting der kan "vække" et rigidbody op.
    [Header("Should Player start it?")]
    [Tooltip("Player is the one starting the Conveyor Belt, if it is true!")]
    [SerializeField]
    private bool shouldPlayerStartIt = false;

    private bool startConveyorBelt = false;

	// Use this for initialization
	void Start ()
    {
        if(shouldPlayerStartIt == false)
        {
            startConveyorBelt = true;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

	
	}



    void OnCollisionEnter(Collision collision)
    {
        if(shouldPlayerStartIt == true)
        {
            if(collision.transform.tag == "Player")
            {
                startConveyorBelt = true;
            }
        }
    }

    /// <summary>
    /// Make all gameobjects with a rigidbody move a direction
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionStay(Collision collision)
    {

        if(startConveyorBelt == true)
        {
            switch (CBM)
            {
                case ConveyorBeltMode.SIMPLE:
                    ConveyorBeltMovementSimple(collision);
                    break;
                case ConveyorBeltMode.ADVANCED:
                    break;
                default:
                    break;
            }
        }
        
    }

    private void ConveyorBeltMovementSimple(Collision collision)
    {

        switch (dir)
        {
            case Direction.FORWARD:
                collision.rigidbody.MovePosition(collision.transform.position + transform.forward * Time.deltaTime * 2);
                break;
            case Direction.BACK:
                collision.rigidbody.MovePosition(collision.transform.position + -transform.forward * Time.deltaTime * 2);
                break;
            case Direction.LEFT:
                collision.rigidbody.MovePosition(collision.transform.position + -transform.right * Time.deltaTime * 2);
                break;
            case Direction.RIGHT:
                collision.rigidbody.MovePosition(collision.transform.position + transform.right * Time.deltaTime * 2);
                break;
            case Direction.UP:
                collision.rigidbody.MovePosition(collision.transform.position + transform.up * Time.deltaTime * 2);
                break;
            case Direction.DOWN:
                collision.rigidbody.MovePosition(collision.transform.position + -transform.up * Time.deltaTime * 2);
                break;
            default:
                break;
        }
    }

    private void ConveyourBeltMovementAdvanced()
    {

    }

    }
