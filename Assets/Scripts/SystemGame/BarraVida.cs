
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BarraVida : MonoBehaviour
{
    public Image rellenoBarra;
    public PlayerManager playerManager;
    //private PlayerController playerController; 
    private float vidaMaxima;
    void Start()
    {
      //playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
      
      vidaMaxima = playerManager.life;
    }

    void Update()
    {
      if(playerManager.life>=0){
        //Debug.Log(playerManager.life / vidaMaxima);

          rellenoBarra.fillAmount = (playerManager.life / vidaMaxima);
        }
      else {
        playerManager.ALive=false;
        //Debug.Log(playerManager.life / vidaMaxima);
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}

    }
}
