using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterMovement;

public class CustomCharacterMovement : CharacterMovement3D

{
    public void Teleport(Vector3 location)
    {
       // transform.position = location;

        Rigidbody.position = location;
        NavMeshAgent.Warp(location);

    }
}
