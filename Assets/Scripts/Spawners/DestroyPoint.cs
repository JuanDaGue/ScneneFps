using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the GameObject entering the trigger has the tag "Destroyable"
        if (other.gameObject.CompareTag("Destroyable"))
        {
            // Destroy the GameObject
            Destroy(other.gameObject);
        }
    }
}
