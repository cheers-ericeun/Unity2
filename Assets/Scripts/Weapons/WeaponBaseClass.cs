using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBaseClass : MonoBehaviour
{
    [field: SerializeField, BoxGroup("Weapon")] public float Damage { get; private set; } = 5f;
    [field: SerializeField, BoxGroup("Weapon")] public float Range { get; private set; } = 5f;              //actual max range
    [field: SerializeField, BoxGroup("Weapon")] public float EffectiveRange { get; private set; } = 4f;     //target range for AI
    [field: SerializeField, BoxGroup("Weapon"), SuffixLabel("attaccks/s",true)] public float AttackRate { get; private set; } = 2f;         
    [field: SerializeField, BoxGroup("Weapon")] public float Duration { get; private set; } = 1f;           //how long the attack takes
    [field: SerializeField, BoxGroup("Weapon")] public DamageType DamageType { get; private set; } = DamageType.Normal;

    private float _lastAttackTime;

    public bool TryAttack(Vector3 aimPosition, GameObject instigator, int team)
    {
        float nextAttackTime = _lastAttackTime + 1f / AttackRate;
        if (Time.time > nextAttackTime)
        {
            Attack(aimPosition, instigator, team); 
            return true;
        }
        return false;
    }

    protected virtual void Attack(Vector3 aimPosition, GameObject instigator, int team)
    {

    }

}
