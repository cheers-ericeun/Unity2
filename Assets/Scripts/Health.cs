using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;


[HideMonoScript]
public class Health : MonoBehaviour
{


    [SerializeField,BoxGroup("Stats")] private float _current = 100f;
    [SerializeField,BoxGroup("Stats")] private float _max = 100f;

    //properties
    [BoxGroup("Debug"), ShowInInspector] public float percentage => _current / _max;
    [BoxGroup("Debug"), ShowInInspector] public bool IsAlive => _current >= 1f;

    public void Damage(DamageInfo damageInfo)
    {
        if (!IsAlive) return;    // early return - function stops executing here
        if (damageInfo.Amount < 1f) return;

        // reduce health + clamp to avoid bad values
        _current -= damageInfo.Amount;
        _current = Mathf.Clamp(_current, 0f, _max);

        // TODO: add damage event
        // TODO: add damage feedback
        // TODO: handle death

    }

    [ContextMenu("Damage Test 10%"), Button("Damage Test 10%")]
    public void DamageTest()
    {
        DamageInfo damageInfo = new DamageInfo(_max * 0.1f, gameObject, gameObject, gameObject, DamageType.Bug);
        Damage(damageInfo);
    }




}


public class DamageInfo
{
    public DamageInfo(float amount, GameObject victim, GameObject source, GameObject instigator, DamageType damageType )
    {
        Amount = amount;
        Victim = victim;
        Source = source;
        Instigator = instigator;
        DamageType = damageType;
    }

    public float Amount { get; set; }
    public GameObject Victim { get; set; }
    public GameObject Source { get; set; }
    public GameObject Instigator { get; set; }
    public DamageType DamageType { get; set; }
}


public enum DamageType
{
    Normal,
        Fire,
        Ice,
        Lighting,
        Poison,
        Bug,
        Gun,
}
