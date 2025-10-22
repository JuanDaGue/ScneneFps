using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float jumpForce = 8f; // Force applied when jumping

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    public void Jump()
    {
        anim.SetBool("isJumping", true);
        //anim.SetBool("isGrounded", false);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), 0.5f);
    }

    private void ResetJump()
    {
        anim.SetBool("isJumping", false);
    }
    
}