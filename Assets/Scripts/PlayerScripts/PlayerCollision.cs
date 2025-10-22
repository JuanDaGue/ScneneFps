using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator anim;
    public PlayerManager player;

    private void Start()
    {
        // Ensure the Animator is obtained correctly from the Player object
        if (player != null)
        {
            anim = player.GetComponent<Animator>();

            if (anim == null)
            {
                Debug.LogWarning("Animator component not found on the Player object!");
            }
        }
        else
        {
            Debug.LogError("PlayerManager reference is missing!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the object collided with has the tag "Ground"
    //if (other.CompareTag("Ground"))
    //{
        // Check the angle of the surface
        Vector3 normal = Vector3.up; // Default normal
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            normal = hit.normal;
        }
        float angle = Vector3.Angle(Vector3.up, normal);
        //Debug.Log("Angle: " + angle); // Debugging line to check the angle
        //if (angle < 45f) // Adjust the angle threshold as needed
        //{
            //isGrounded = true;
            player.isGrounded = true;
            anim.SetBool("isGrounded", true);
        //}
    //}

    }

    private void OnTriggerExit(Collider other)
    {
        // Handle exiting collision with "Ground"
       // if (other.CompareTag("Ground"))
        //{
            player.isGrounded = false;

            if (anim != null)
            {
                anim.SetBool("isGrounded", false);
            }
        //}
    }
}