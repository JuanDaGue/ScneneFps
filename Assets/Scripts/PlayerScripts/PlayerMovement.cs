using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 200.0f;
    public float speed = 5.0f;

    private Rigidbody rb;
    private PlayerManager player;
    private float x, y;
    private bool hitwall;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerManager>();
    }

    void FixedUpdate()
    {
        if (hitwall && !player.isGrounded)
        {
            Debug.Log("Hit Update");
            // When hitting a wall, let gravity pull the player down
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, -5f, rb.linearVelocity.z);
        }
        else
        {
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            // Handle rotation
            transform.Rotate(0, x * rotationSpeed * Time.deltaTime, 0);
            speed = player.isSprinting ? player.sprintSpeed : player.speed;

            // Movement logic
            Vector3 moveDirection = transform.forward * y;
            rb.linearVelocity = new Vector3(moveDirection.x * speed, rb.linearVelocity.y, moveDirection.z * speed);

            if (player.isGrabbing)
            {
                speed = 2.5f;
                rotationSpeed = 100.0f;
            }
            else
            {
                speed = 5.0f;
                rotationSpeed = 200.0f;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(!player.isGrounded){
        hitwall = true;
        Debug.Log("Hit wall");

        // Optionally, set the player's Y velocity to 0 before simulating a fall
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, -5f, rb.linearVelocity.z);

        // Reset hitwall after 1 second
        Invoke(nameof(resetHitwall), 1f);
        }
    }

    void resetHitwall()
    {
        hitwall = false;
    }
}