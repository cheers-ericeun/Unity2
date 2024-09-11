using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private float _speed;
    private float _range;
    private float _damage;
    private GameObject _instigator;
    private int _team;
    private Vector3 _spawnPosition;

    private void OnValidate()
    {
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
            _rb .useGravity = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            //enable rigidbody interpolation for somoother visual movement
            _rb.interpolation = RigidbodyInterpolation.Interpolate;
        }


        if(TryGetComponent(out CapsuleCollider capsuleCollider))
        {
            capsuleCollider.isTrigger = true;
        }


    }
    public void Launch(float speed, float range, float damage ,DamageType damageType, GameObject instigator, int team)
    {
        _speed = speed;
        _range = range;
        _damage = damage;
        _instigator = instigator;
        _team = team;

        _rb.velocity = transform.forward * speed;
        _spawnPosition = transform.position;
    }
    private void Update()
    {
        float distanceTravled = Vector3.Distance(transform.position, _spawnPosition);
        if(distanceTravled > _range)
        {
            Cleanup();
        }
    }

    private void Cleanup()
    {
        Destroy(gameObject);
        //might do something else like explotions, hense new method
    }
}
