using UnityEngine;

public class PushAndGrab : MonoBehaviour
{
    [Header("Push Settings")]
    public LayerMask pushLayers;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;

    [Header("Grab Settings")]
    public bool canGrab;
    public string grabTag = "BLOCK"; // Objects with this tag can be grabbed
    public Transform grabPoint; // The point where the object will be held
    private Rigidbody grabbedObject;

    private Animator anim;

    public bool isGrabbing;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // Handle releasing the grabbed object
        if (canGrab && grabbedObject != null && Input.GetKeyDown(KeyCode.E))
        {
           
            ReleaseObject();
        }

        // Handle grabbing animation
        if (canGrab && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isGrabbing", true);
        }
        if (canGrab && Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("isGrabbing", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("OnCollisionEnter");
        if (canPush) PushRigidBodies(collision);

        // Handle grabbing an object
        if (canGrab && grabbedObject == null && collision.collider.CompareTag(grabTag) && Input.GetKeyDown(KeyCode.E))
        {   
            print("ReleaseObject");
            GrabObject(collision.rigidbody);
        }
    }

    private void PushRigidBodies(Collision collision)
    {
        Rigidbody body = collision.rigidbody;
        if (body == null || body.isKinematic) return;

        // Check layer
        var bodyLayerMask = 1 << body.gameObject.layer;
        if ((bodyLayerMask & pushLayers.value) == 0) return;

        // Ignore objects below
        if (collision.contacts[0].normal.y < -0.3f) return;

        // Push direction
        Vector3 pushDir = collision.relativeVelocity.normalized;

        // Apply push force
        body.AddForce(pushDir * strength, ForceMode.Impulse);
    }

    private void GrabObject(Rigidbody body)
    {
        // Ensure the body exists
        if (body == null) return;

        grabbedObject = body;
        grabbedObject.isKinematic = true; // Disable physics while grabbed
        grabbedObject.transform.position = grabPoint.position;
        grabbedObject.transform.parent = grabPoint;
        isGrabbing = true;
        anim.SetBool("isGrabbing", true);
        Debug.Log($"Grabbed {grabbedObject.name}");
    }

    private void ReleaseObject()
    {
        if (grabbedObject == null) return;

        grabbedObject.isKinematic = false; // Re-enable physics
        grabbedObject.transform.parent = null;
        grabbedObject = null;
        isGrabbing = false;
        anim.SetBool("isGrabbing", false);
        Debug.Log("Released object");
    }
}
