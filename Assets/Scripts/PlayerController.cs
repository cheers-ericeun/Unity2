using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//enforce components in this gameobject
//***[RequireComponent(typeof(CustomCharacterMovement))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : Controller
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _aimOffset = 1f;
    private Vector2 _moveInput2D;
    private Vector3 _aimPosition;

    //"field:" is necessary to serialize proties properly(background compilling issue)
    //***[field: SerializeField] public CustomCharacterMovement Movement {  get; private set; }
    //"OnValidate" is *Editor only* function=>gets called when changes made in inspector
    //private void OnValidate()
    //{
    //    Movement = GetComponent<CustomCharacterMovement>(); 
    //}


    public void OnJump()
    {
        Movement.TryJump();
    }

    public void OnMove(InputValue inputValue) 
    {
        _moveInput2D = inputValue.Get<Vector2>();

        //debug visually
        Vector3 moveInput3D = new Vector3(_moveInput2D.x, 0f, _moveInput2D.y);
        Debug.DrawRay(transform.position,moveInput3D * 2f,Color.red,0.5f);
    }

    private void OnTeleport()
    {

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, Mathf.Infinity, _groundMask))
        {
            Vector3 location = hitInfo.point;
            Debug.Log(location);

            Movement.Teleport(location);
        }

    }


    public void OnShoot()
    {
        //assume shot gun is weapon 0
        WeaponBaseClass shotgun = Weapons[0];
        shotgun.TryAttack(_aimPosition, gameObject, 0);
    }


    private void Update()
    {
        //map 2d input to 3d space before moving character
        Vector3 right = Camera.main.transform.right;
        Vector3 up = Vector3.up;
        Vector3 forward = Vector3.Cross(right, up);
        //cross product inpliment
        Vector3 moveInput3D = forward * _moveInput2D.y + right * _moveInput2D.x;

         
        Movement.SetMoveInput(moveInput3D);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, Mathf.Infinity, _groundMask))
        {
            Movement.SetLookPosition(hitInfo.point);


            Vector3 offset = mouseRay.direction * -_aimOffset;
            //find aim position
            _aimPosition = hitInfo.point + offset;
        }
    }

}
