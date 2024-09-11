using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{


    [SerializeField] private Transform _target;
    [SerializeField] private float _attackDistance = 1.5f;


    private void Start()
    {
        //youtube find player

        _target = GameObject.FindWithTag("Player").transform;



    }
    private void Update()
    {
        //chase afet target if exist
        if(_target != null)
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance > _attackDistance) 
            {
                Movement.MoveTo(_target.position);
                Debug.DrawLine(transform.position,_target.position,Color.yellow);
            }
            else
            {
                Movement.Stop();
                Movement.SetLookPosition(_target.position);
                Debug.DrawLine(transform.position,_target.position,Color.magenta);
                
            }
            Movement.MoveTo(_target.position);
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,_attackDistance);
    }



}
