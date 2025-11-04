using System.Collections;
using UnityEngine;

public class MoveKatana : MonoBehaviour
{
    [Header("Referencia al objeto que se moverá")]
    public Transform katanaObject; // Asignar desde el inspector

    [Header("Configuración de movimiento")]
    public float moveDistance = 2f;
    public float moveDuration = 1f;

    private float originY;
    private float targetY;
    private Vector3 fixedXZ;
    private Coroutine moveCoroutine;

    void OnEnable()
    {
        if (katanaObject == null)
        {
            Debug.LogWarning("No se asignó katanaObject en el inspector.");
            return;
        }

        // Guardamos la posición actual como destino
        targetY = katanaObject.position.y;
        originY = targetY - moveDistance;

        // Guardamos X y Z para mantenerlos fijos
        fixedXZ = new Vector3(katanaObject.position.x, 0f, katanaObject.position.z);

        // Colocamos el objeto en la posición inicial (solo Y cambia)
        katanaObject.position = new Vector3(fixedXZ.x, originY, fixedXZ.z);

        // Iniciamos la animación hacia arriba
        moveCoroutine = StartCoroutine(MoveKatanaToTarget());
    }

    void OnDisable()
    {
        if (katanaObject != null)
        {
            // Detenemos la animación si está corriendo
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
                moveCoroutine = null;
            }

            // Restauramos la posición original
            katanaObject.position = new Vector3(fixedXZ.x, targetY, fixedXZ.z);
        }
    }

    IEnumerator MoveKatanaToTarget()
    {
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            float currentY = Mathf.Lerp(originY, targetY, t);

            katanaObject.position = new Vector3(fixedXZ.x, currentY, fixedXZ.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Aseguramos que llegue exactamente al destino
        katanaObject.position = new Vector3(fixedXZ.x, targetY, fixedXZ.z);
    }
}