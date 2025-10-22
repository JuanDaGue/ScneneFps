using UnityEngine;

public class PushHitPlayer : MonoBehaviour
{
    public float pushForce = 10f; // Adjust the force of the push
    public PlayerManager playerManager; // Reference to the PlayerManager

    void Start()
    {
        //playerManager = GameObject.Find("Player")?.GetComponent<PlayerManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the other object is a player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the Rigidbody of the other player
            Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (otherRigidbody != null)
            {
                // Calculate the direction of the push
                Vector3 pushDirection = collision.gameObject.transform.position - transform.position;
                pushDirection.y = 0; // Prevent upward force

                // Apply the push force
                otherRigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.Impulse);
                playerManager.EnemyHit = true;
                playerManager.life-=20;
                playerManager.isGrabbing = false;
                //Debug.Log("Player hit!");
            }
        }
    if(collision.gameObject.CompareTag("BLOCK")){
    
        Rigidbody otherRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            Vector3 pushDirection = collision.gameObject.transform.position - transform.position;
                otherRigidbody.GetComponent<Rigidbody>().isKinematic = false;
                otherRigidbody.GetComponent<Rigidbody>().useGravity = true;
            pushDirection.y = 0; // Prevent upward force
            otherRigidbody.AddForce(pushDirection.normalized * pushForce, ForceMode.    Impulse);
        }
            //playerManager.EnemyHit = true;
            playerManager.life-=2;
            playerManager.isGrabbing = false;
        }   
    }

}
