using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttacksManager : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float AttackValue;
    [SerializeField] private float AnimationTime = 7.5f;

    [Header("Assign matching counts")]
    public List<GameObject> AtacksVfx = new List<GameObject>();
    public List<Transform> TransformAtacksVfx = new List<Transform>();

    private List<GameObject> AtacksVfx2 = new List<GameObject>();
    private int AttackIndex = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        AttackValue = 0f;

        // Ensure we don't try to access a transform that doesn't exist
        int count = Mathf.Min(AtacksVfx.Count, TransformAtacksVfx.Count);
        for (int i = 0; i < count; i++)
        {
            if (AtacksVfx[i] == null || TransformAtacksVfx[i] == null)
            {
                AtacksVfx2.Add(null);
                continue;
            }

            var vfx = Instantiate(AtacksVfx[i], TransformAtacksVfx[i].position, TransformAtacksVfx[i].rotation);
            vfx.SetActive(false);
            vfx.transform.SetParent(TransformAtacksVfx[i], worldPositionStays: true);
            AtacksVfx2.Add(vfx);
        }

        // If lists had different sizes, fill remaining AtacksVfx2 entries with nulls to keep indexing safe
        for (int i = count; i < AtacksVfx.Count; i++)
        {
            AtacksVfx2.Add(null);
        }

        // Ensure AttackIndex is in a valid range
        AttackIndex = Mathf.Clamp(AttackIndex, 0, Mathf.Max(0, AtacksVfx2.Count - 1));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TriggerAttack(0, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TriggerAttack(1, 1.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TriggerAttack(2, 2.5f);
        }
    }

    void TriggerAttack(int index, float attackValue)
    {
        AttackValue = attackValue;
        AttackIndex = Mathf.Clamp(index, 0, Mathf.Max(0, AtacksVfx2.Count - 1));
        anim.SetFloat("Attack", AttackValue);
        anim.SetTrigger("Attack");
        StartCoroutine(AttackAnimation(AttackIndex));
    }

    void AttackEnd()
    {
        AttackValue = 0f;
        anim.SetFloat("Attack", AttackValue);
    }

    IEnumerator AttackAnimation(int index)
    {
        if (index < 0 || index >= AtacksVfx2.Count)
        {
            yield break;
        }

        var vfx = AtacksVfx2[index];
        if (vfx == null)
        {
            yield break;
        }

        vfx.SetActive(true);
        yield return new WaitForSeconds(AnimationTime);
        AttackValue = 0f;
        anim.SetFloat("Attack", AttackValue);
        vfx.SetActive(false);
    }
}