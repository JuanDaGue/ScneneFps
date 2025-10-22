using UnityEngine;
public class PlayerHealth : MonoBehaviour
{
public float health = 100f;


    public AudioClip HealthSound;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
            playerManager.life=health;
            Destroy(gameObject);
        }
    }
}