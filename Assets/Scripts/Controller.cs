using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(CustomCharacterMovement))]
public abstract class Controller : MonoBehaviour
{
    //"field:" is necessary to serialize proties properly(background compilling issue)
    [field: SerializeField] public CustomCharacterMovement Movement { get; private set; }
    [field: SerializeField,InlineButton(nameof(FindWeapons),"Find")] public WeaponBaseClass[] Weapons { get; private set; }
    protected virtual void OnValidate()
    {
        Movement = GetComponent<CustomCharacterMovement>();
    }

    private void FindWeapons()
    {
        Weapons = GetComponentsInChildren<WeaponBaseClass>();
    }




}
