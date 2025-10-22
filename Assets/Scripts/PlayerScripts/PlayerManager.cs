using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Player Manager")]
    public float speed = 5.0f;
    public float sprintSpeed = 8.0f;
    public float rotationSpeed = 200.0f;

    [Header("Player States")]
    public bool isGrounded;
    public bool isSprinting;
    public bool isJumping;
    public bool isGrabbing;
    public bool EnemyHit;

    private Animator anim;
    private Rigidbody rb;
    private float x, y;

    [Header("Life Settings")]
    public float maxLife = 100.0f;
    public float life;
    public float Damage;
    public bool ALive;

    [Header("Audio Settings")]
    public AudioClip landingAudioClip;
    public AudioClip[] footstepAudioClips;
    [Range(0, 1)] public float footstepAudioVolume = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //isGrounded= GetComponent<PlayerCollision>().isGrounded;
        life = maxLife;
        ALive = true;

        if (anim == null)
            Debug.LogWarning("Animator component missing!");
        if (rb == null)
            Debug.LogWarning("Rigidbody component missing!");
    }

    void Update()
    {
        ProcessInput();
        //Debug.Log("Is Grounded: " + isGrounded);
        if (Input.GetKey(KeyCode.LeftShift) && !isGrabbing)
        {
            isSprinting = true;
            speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isGrabbing)
        {
            isSprinting = false;
            speed = 5.0f;
        }
        if(isGrabbing)
        {
            speed = 5.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isGrabbing)
        {
            GetComponent<PlayerJump>().Jump();
        }

        if (EnemyHit)
        {
            anim.SetBool("isHit", true);
            EnemyHit = false;
            Invoke(nameof(DamagePlayer), 0.5f);
        }


        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
        anim.SetBool("isGrabbing", isGrabbing);
        anim.SetBool("ALive", ALive);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void ProcessInput()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
    }

    public void DamagePlayer()
    {
        anim.SetBool("isHit", false);
        Damage += 0.1f;
        anim.SetFloat("Damage", Damage);
        isGrabbing = false;
        isSprinting = false;
    }

}


