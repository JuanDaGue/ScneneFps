using UnityEngine;

public class pickUpweapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Currentweapon;
    public GameObject weaponPickup;

    public GameObject faterWeapon;
    void Start()
    {
        weaponPickup.SetActive(false);
        Currentweapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ohter name"+other.gameObject.name);
        if (Currentweapon != null)
        {
                
        if (other.CompareTag("Weapon"))
        {
                if (other.GetComponent<WeaponPickup>())
                {
                    other.GetComponent<WeaponPickup>().OnPickup(transform);
                    weaponPickup.SetActive(true);
                    Currentweapon.SetActive(false);
            }

        }
        }
    }
}
