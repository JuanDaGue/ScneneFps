using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour
{
    // Public variables to set in the Inspector
    public GameObject rock;
    public Transform[] rockSpawnPoints;
    public float rockSpawnTime = 30f;
    public float initialVelocityZ = 35f;  
    public LayerMask pushLayers;
    public bool canPush;
    [Range(0.5f, 5f)] public float strength = 1.1f;
    
    void Start()
    {
        // Start the coroutine to spawn rocks
        StartCoroutine(SpawnRocks());
    }

    // Coroutine to spawn rocks
    IEnumerator SpawnRocks()
    {
        // Infinite loop to keep spawning rocks
        while (true)
        {
            // Wait for the specified spawn time
            yield return new WaitForSeconds(rockSpawnTime);

            // Spawn rocks at a random spawn point
            int rockIndex = Random.Range(0, rockSpawnPoints.Length);
            GameObject spawnedRock = Instantiate(rock, rockSpawnPoints[rockIndex].position, rockSpawnPoints[rockIndex].rotation);

            // Add initial velocity to the spawned rock
            Rigidbody rockRigidbody = spawnedRock.GetComponent<Rigidbody>();
            if (rockRigidbody != null)
            {
                rockRigidbody.linearVelocity = new Vector3(0, 0, -initialVelocityZ);
            }
            Destroy(spawnedRock, 10f);
        }
    }

    void OnTriggerEnter(Collider other)
    {   

        //Debug.Log("OnTriggerEnter DestroySpawers");
        if (other.gameObject.name == "DestroySpawers"){
            //print("DestroySpawers");
            Destroy(gameObject);
        } 
    }
}
