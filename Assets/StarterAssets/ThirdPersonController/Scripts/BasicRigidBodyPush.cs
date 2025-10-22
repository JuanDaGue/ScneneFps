using UnityEngine;

public class BasicRigidBodyPush : MonoBehaviour
{
    [Header("Push Settings")]
    public LayerMask pushLayers;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    public PlayerManager playerManager;

    void Start()
    {

    }
    private void Update()
    {
        // Handle releasing the grabbed object
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (canPush) PushRigidBodies(hit);
        print("OnControllerColliderHit");

    }

    private void PushRigidBodies(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) return;

        // Check layer
        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // Ignore objects below
        if (hit.moveDirection.y < -0.3f) return;

        // Push direction
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

        // Apply push force
        body.AddForce(pushDir * strength, ForceMode.Impulse);
        playerManager.Damage = hit.moveDirection.y;

        Debug.Log("Damage  "+ playerManager.Damage);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        // Handle grabbing the object
               
 
    }

}
