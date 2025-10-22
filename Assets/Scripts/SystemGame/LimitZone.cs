using UnityEngine;

public class LimitZone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            PlayerManager player= other.GetComponent<PlayerManager>();
            player.ALive = false;
        }
        if (other.gameObject.tag == "BLOCK")
        {
            other.transform.position = new Vector3(0, 0, 0);           
        }
    }
}
