using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public ParticleSystem sparkBurst;
    public ParticleSystem dustBurst;
    public GameObject ringPrefab;
    public AudioClip pickSound;
    public float pickupMoveDuration = 0.45f;
    AudioSource audioSource;
    public bool picked = false;

    void Awake() => audioSource = GetComponent<AudioSource>();

    public void OnPickup(Transform playerHand)
    {
        if(picked) return;
        picked = true;

        if(sparkBurst) sparkBurst.Play();
        if(dustBurst) dustBurst.Play();

        if(pickSound) audioSource.PlayOneShot(pickSound);

        if(ringPrefab)
        {
            var r = Instantiate(ringPrefab, transform.position, Quaternion.LookRotation(Vector3.up));
            Destroy(r, 1f);
        }

        StartCoroutine(MoveToPlayer(playerHand));
    }

    System.Collections.IEnumerator MoveToPlayer(Transform playerHand)
    {
        var startPos = transform.position;
        var startRot = transform.rotation;
        float t=0f;
        while(t < pickupMoveDuration)
        {
            t += Time.deltaTime;
            float k = t / pickupMoveDuration;
            transform.position = Vector3.Lerp(startPos, playerHand.position, Mathf.SmoothStep(0,1,k));
            transform.localScale = Vector3.one * Mathf.Lerp(1f, 0.01f, k);
            transform.rotation = Quaternion.Slerp(startRot, playerHand.rotation, k);
            yield return null;
        }
        gameObject.SetActive(false); // or add to inventory
    }
}