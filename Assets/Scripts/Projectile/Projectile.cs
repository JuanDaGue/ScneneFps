using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField][Tooltip("Seconds that determines object's auto-destruction time. Goes from 0 to 10 seconds.\n" +
        "Default Value = 1.")]
    [Range(0f, 10f)] private float timeToAutoDestroy = 1f;

    private Collider _collider;

    private void OnValidate()
    {
        _collider = GetComponent<Collider>();

        _collider.isTrigger = true;
    }

    private void Awake()
    {
        Destroy(gameObject, timeToAutoDestroy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Contains("Dummy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
