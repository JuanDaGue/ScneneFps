using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BarraTiempo : MonoBehaviour
{
    public Image rellenoTiempo;
    private float tiempoMaximo = 300f; // 5 minutos en segundos
    public float tiempoRestante;

    void Start()
    {
        tiempoRestante = tiempoMaximo;
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            rellenoTiempo.fillAmount = tiempoRestante / tiempoMaximo;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void ResetTiempo(float nuevoTiempo)
    {
        tiempoRestante = nuevoTiempo;
        rellenoTiempo.fillAmount = 1f; // La barra vuelve a llenarse
    }

}
