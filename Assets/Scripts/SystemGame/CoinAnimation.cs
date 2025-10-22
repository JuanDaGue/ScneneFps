using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    [Header("Movement Settings")]
    public float amplitude = 0.5f;    // Height of the movement
    public float frequency = 1f;      // Speed of the movement
    public float rotationSpeed = 180f;// Rotation speed in degrees per second

    [Header("Effects")]
    public ParticleSystem collectEffect;
    public AudioClip collectSound;

    private Vector3 startPosition;
    private bool isCollected = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!isCollected)
        {
            // Vertical oscillation using sine wave
            float verticalOffset = Mathf.Sin(Time.time * Mathf.PI * frequency) * amplitude;
            transform.position = startPosition + Vector3.up * verticalOffset;

            // Rotate coin
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            CollectCoin();
        }
    }

    void CollectCoin()
    {
        isCollected = true;
        
        // Play effects
        if (collectEffect != null)
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        
        if (collectSound != null)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);

        // Disable coin
        if(gameObject){
                if(GetComponent<Renderer>()){
                    GetComponent<Renderer>().enabled = false;
                    GetComponent<Collider>().enabled = false;
                }
                
                // Destroy after 1 second
                Destroy(gameObject, 1f);
        }
    }
}