using UnityEngine;

public class FireSpan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float fireTime=5f;
    public float fireOff = 10f;
    public GameObject fire;
    public float SpawTimeFire = 0;
    public bool FireOnFire = true;
    void Start()
    {
        fire.SetActive(FireOnFire);
    }

    // Update is called once per frame
    void Update()
    {
        SpawTimeFire += Time.deltaTime; 
        //print(SpawTimeFire);
        if (SpawTimeFire>fireTime)
        {
            if (FireOnFire)
            {   
                FireOnFire = false;
                fire.SetActive(FireOnFire);
                SpawTimeFire = 0;
            }
            else
            {
                FireOnFire = true;
                fire.SetActive(FireOnFire);
                SpawTimeFire = 0;
            }
        }
    }
}
