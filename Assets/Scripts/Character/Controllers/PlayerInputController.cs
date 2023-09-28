using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : CharacterController
{
    private Camera _camera;
    private void Awake()
    {
        _camera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ���� ��ư�� ������ ���� ó��
            //Debug.Log("down");
        }

        if (Input.GetMouseButton(0))
        {
            // ���콺 ���� ��ư�� ������ �ִ� ������ ó��
            //Debug.Log("press");

        }

        if (Input.GetMouseButtonUp(0))
        {
            // ���콺 ���� ��ư�� �� ���� ó��
            //Debug.Log("up");

        }
    }

    public void OnMove(InputValue value)
    {
        // Debug.Log("OnMove" + value.ToString());
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        if (newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    public void OnChangeActive()
    {
        CallChangeActiveEvent();
    }
    public void OnUseActive( )
    {
        CallUseActiveEvent();
    }
    public void OnThrowWeapon()
    {
        CallThrowWeaponEvent();
    }
    public void OnChangeWeapon()
    {
        CallChangeWeaponEvent();
    }
}