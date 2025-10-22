using UnityEngine;

public class playerMov : MonoBehaviour
{
    // VARIABLES
    public float speed = 5f;

    private Rigidbody rb;
    void Awake()
    {
        // Aqui deberias de cargar los componentes necesarios.
        rb = GetComponent<Rigidbody>();
    }

    // FUNCIONES

    /// <summary>
    /// Esta funcion tiene la tarea de encapsular la logica que movera a nuestro jugador dados los valores de un input de
    /// tipo axis. Esta funcion deberia de mover a nuestro jugador en el tiempo teniendo en cuenta un valor de velocidad.
    /// Para que esta funcion pueda poner en marcha su logica es crucial que hagamos referencia a un componente que maneje
    /// fisicas dentro de nuestro objeto.
    /// </summary>
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal,0f,vertical);
        rb.linearVelocity = movement * speed;
    }

    #region Utility [DO NOT MODIFY]

    void Update()
    {
        Move();
    }

    #endregion
}
