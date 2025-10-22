using UnityEngine;

public class PointSystem : MonoBehaviour
{
    public int points = 0; // Points for collecting coins
    public int xp = 0;     // XP for destroying elements

    void OnTriggerEnter(Collider other)
    {
        // Check if the object is of type "Coin"
        if (other.CompareTag("Coin"))
        {
            points += 10; // Add points for collecting a coin
            Destroy(other.gameObject); // Remove the coin from the scene
            Debug.Log("Points: " + points);
        }
    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     
    //     if (collision.gameObject.CompareTag("Destroy"))
    //     {
    //         xp += 20;
    //         Destroy(collision.gameObject); 
    //         Debug.Log("XP: " + xp);
    //     }
    // }
}
