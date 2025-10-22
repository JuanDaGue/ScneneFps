using System.Collections;
using UnityEngine;

/// <summary>
/// Esta clase contiene la logica que determina la carga y lanzamiento de una catapulta.
/// </summary>
public class bullet : MonoBehaviour
{
    #region Essentials [DO NOT MODIFY]

    #region Events

    public delegate void CatapultEvent();
    public static event CatapultEvent OnShoot;

    #endregion

    #region Constants

    private const float MAX_CHARGE = 100f;

    private const float CHARGE_SPEED = 80f;
    private const float DISCHARGE_SPEED = 300f;

    #endregion

    #region Variables
    [Header("Parameters")]
    [SerializeField] private float _maxRotation = 90f;

    private Transform _catapultArm;

    private static float _charge;

    private Coroutine _task;

    #endregion

    #region Accessors

    public static float Charge => _charge;

    #endregion

    #endregion
/// <summary>
/// /////////////////////////////////////
/// //  Variables del proyectil
/// /////////////////////////////////////
/// </summary>


// Velocidad a la que se moverá el proyectil , El profesor dice que un numero grande entre 10 -  50 , 
// ya que se mide como si fueran Newtons
public float speed = 20f;

// Componente Rigidbody del proyectil
private Rigidbody rb;

// Valor final de la carga acumulada, es un porcentage entre 0 y  1;
private float FinalCharge;

// Prefab del proyectil que se lanzará (Añadir desde el inspector de unity, de debe crear el prefab y añadir un rigid body) 
public GameObject bullets;

// Punto de generación del proyectil 
//(Añadir desde el inspector de unity, usa el putno que esta en la gerarquia como catapult/lunchArm/Bucket/back/ShootPoint) 
public Transform spawnPoint;


// Llamado una vez por fotograma
private void Update()
{
    // Verificar si se presionó el botón izquierdo del mouse en este fotograma
    if (Input.GetMouseButtonDown(0)){
        StartCharging(); // Iniciar el proceso de carga
    }
    // Verificar si se mantiene presionado el botón izquierdo del mouse
    if (Input.GetMouseButton(0)){
        ChargeCatapult(); // Cargar la catapulta mientras se mantiene presionado el botón
    }
    // Verificar si se soltó el botón izquierdo del mouse en este fotograma
    if (Input.GetMouseButtonUp(0)){
        FinalCharge = Charge; // Guardar el valor final de la carga
        LaunchCatapult(); // Lanzar la catapulta
    }
    #region Utils [NO MODIFICAR]
    UpdateArmRotation(); // Actualizar la rotación del brazo, función de utilidad que no debe modificarse
    #endregion
}

// Disparar un proyectil desde la catapulta
private void ShootProjectile()
{
    // Instanciar (crear) una nueva bala en la posición y rotación del punto de generación
    GameObject bullet = Instantiate(bullets, spawnPoint.position, spawnPoint.rotation);

    // Obtener el componente Rigidbody adjunto a la bala
    Rigidbody rb = bullet.GetComponent<Rigidbody>();

    // Calcular la dirección y fuerza a aplicar a la bala
    // es la suma la dirección de la bala hacia el frente y un pequeño desplazamiento hacia arriba
    Vector3 direction = spawnPoint.forward * (FinalCharge / 100) + spawnPoint.up * 0.4f;

    // Aplicar la fuerza calculada al Rigidbody de la bala para lanzarla
    rb.AddForce(direction * speed, ForceMode.Impulse);
}

    #region Essentials [DO NOT MODIFY]

    private void Awake()
    {
        _catapultArm = transform.Find("LaunchArm");

        OnShoot += ShootProjectile;
    }

    #region Behaviors

    private void StartCharging()
    {
        if (_task != null) StopCoroutine(_task);
    }

    private void ChargeCatapult()
    {
        if (_charge < MAX_CHARGE) _charge += CHARGE_SPEED * Time.deltaTime;
        else if (_charge >= MAX_CHARGE) _charge = MAX_CHARGE;
    }

    private void LaunchCatapult()
    {
        _task = StartCoroutine(Launch());
    }

    #endregion

    #region Utility

    private void UpdateArmRotation()
    {
        if (_catapultArm)
        {
            float rotation = -((_charge / MAX_CHARGE) * _maxRotation);
            _catapultArm.eulerAngles = new Vector3(rotation, 0, 0);
        }
    }

    private IEnumerator Launch()
    {
        while (_charge > 0)
        {
            yield return new WaitForEndOfFrame();

            _charge -= DISCHARGE_SPEED * Time.deltaTime;
        }

        OnShoot?.Invoke();

        yield return null;
    }

    #endregion

    #endregion
}