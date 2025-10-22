using UnityEngine;

public class AdvancedSlipController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 [Header("Physics Material Detection")]
    public PhysicsMaterial slipperyMaterial;
    public float force = 10f;
    

    
    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.sharedMaterial == slipperyMaterial)
            {
                HandleSlip();
                return;
            }
        }
    }

    void HandleSlip()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        
        // Reduce directional control
        rb.linearDamping = 1f*force;
        rb.angularDamping = 0.5f*force;
        
        // Add downward force for "ice skating" effect
        rb.AddForce(Vector3.down * force, ForceMode.Acceleration);
    }
}
