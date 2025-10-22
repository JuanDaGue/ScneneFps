using UnityEngine;
using System.Collections.Generic;

public class CheckpointSystem : MonoBehaviour
{
    [Header("Checkpoint Settings")]
    public List<Transform> respawnPoints = new List<Transform>();
    public float respawnTime = 5f;
    public PlayerManager playerManager;
    public int respawnPointIndex = 0;
    private Transform respawnPoint;
    public void Start()
    {
        transform.position = respawnPoints[respawnPointIndex].position;
    }

    public void Respawn()
    {
        respawnPointIndex++;
        if(respawnPointIndex >= respawnPoints.Count)
        {
            respawnPointIndex = 0;
        }
        respawnPoint = respawnPoints[respawnPointIndex];
        playerManager.ALive = true;
        playerManager.life = playerManager.maxLife;
        transform.position = respawnPoint.position;
    }
    
    public void Update(){
        //Debug.Log("CheckpointSystem setup"+ playerManager.ALive);

        if(!playerManager.ALive){
            Invoke("Respawn", respawnTime);
            
        }
    }
}

