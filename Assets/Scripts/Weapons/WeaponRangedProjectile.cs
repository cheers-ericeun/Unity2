using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRangedProjectile : WeaponBaseClass
{
    [SerializeField,BoxGroup("Ranged")] private Projectile _bulletPrefab;
    [SerializeField, BoxGroup("Ranged")] private Transform _muzzle;
    [SerializeField, BoxGroup("Ranged")] private int _shotCount = 6;
    [SerializeField, BoxGroup("Ranged")] private float _inaccuracy = 10f;
    [SerializeField, BoxGroup("Ranged")] private float _projectileSpeed = 30f;

    protected override void Attack(Vector3 aimPosition, GameObject instigator, int team)
    {
        base.Attack(aimPosition, instigator, team);
        Debug.DrawLine(transform.position, aimPosition, Color.magenta, 1f);

        Vector3 spawnPos = _muzzle.position;
        Vector3 aimDir = (aimPosition - spawnPos).normalized; // direction from a to be , is b - a , normalized
        Quaternion spawnRot = Quaternion.LookRotation(aimDir); //lookrotation turns a direction into a rotation

        for (int i = 0; i < _shotCount; i++)
        {
            float inaccX = Random.Range(-_inaccuracy, _inaccuracy);
            float inaccY = Random.Range(-_inaccuracy, _inaccuracy);

            Vector3 leftRightAngle = _muzzle.up * inaccX;
            Vector3 upDownAngle = _muzzle.right * inaccY;
            Quaternion inaccRotation = Quaternion.Euler(leftRightAngle + upDownAngle);

            //combine spawn rotation and inaccuracy rotation, multiply quaterniun to add their rotation

            Quaternion finalRotation = spawnRot * inaccRotation;


            //spawn projectile
            Projectile spawnedProjectile = Instantiate(_bulletPrefab, spawnPos, finalRotation);
            spawnedProjectile.Launch(_projectileSpeed, Range, Damage, DamageType, instigator, team);
        }
    }

}
