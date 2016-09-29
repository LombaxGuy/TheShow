using UnityEngine;
using System.Collections;

public class JumpTrigger : MonoBehaviour
{
    [SerializeField]
    private IntroSequence introSequence;

    void OnTriggerEnter(Collider other)
    {
        Transform player = other.GetComponent<Collider>().transform;

        if (player.parent != null)
        {
            if (player.parent.tag == "Player")
            {
                introSequence.passedJump = true;
                gameObject.SetActive(false);
            }
        }
    }
}
