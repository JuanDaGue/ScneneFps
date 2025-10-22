using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Grab Settings")]
    private GameObject grabObject;
    public GameObject hand;
    public bool isGrabbed = false;
    public string grabTag = "BLOCK";
    //private Animator anim;
    public PlayerManager playerManager;
    

    void Start()
    {
        // anim = GetComponent<Animator>();
        grabObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        //print("grabObject  "+ grabObject);
        if (Input.GetMouseButtonUp(0) && grabObject != null)
        {
            if (isGrabbed)
            {
                //print("OnTriggerStay On Trigger");
                isGrabbed = false;
                playerManager.isGrabbing = false;
                playerManager.speed = 5;
                grabObject.GetComponent<Rigidbody>().isKinematic = false;
                grabObject.GetComponent<Rigidbody>().useGravity = true;
                grabObject.transform.parent = null;
                grabObject = null;
                //Debug.Log("Released object");
                // anim.SetBool("isGrabbing", false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {   
        if (other.gameObject.CompareTag(grabTag))
        {   
            //print("OnTriggerStay");
            if (isGrabbed) return;
            if (Input.GetMouseButton(0) && grabObject == null)
            {
                //print("OnTriggerStay On Trigger EEEEEEEEEEEEE"); 
                isGrabbed = true;
                playerManager.isGrabbing = true;
                //playerManager.isSprinting = false;
                playerManager.speed = 2;
                playerManager.rotationSpeed = 100;
                other.GetComponent<Rigidbody>().isKinematic = true;
                //other.GetComponent<Rigidbody>().useGravity = false;

                other.transform.position = hand.transform.position;
                other.gameObject.transform.parent = hand.transform;
                //Debug.Log($"Grabbed {other.gameObject.name}");
                grabObject= other.gameObject;
                // anim.SetBool("isGrabbing", true);
            }
        }
    }

}
