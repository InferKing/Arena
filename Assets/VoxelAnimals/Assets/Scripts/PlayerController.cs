using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : BaseUnit, IMovable
{
    [SerializeField] private PickUpItem _pickUp;
    public static Action PlayerMoved;
    public static Action PlayerAttacked;
    private void OnEnable()
    {
        MainController.GameStarted += OnStart;
    }
    private void OnDisable()
    {
        MainController.GameStarted -= OnStart;
    }
    private void OnStart()
    {
        StartCoroutine(StartLive());
    }
    private IEnumerator StartLive()
    {
        while (status is not UnitStatus.Death)
        {
            Move();
            yield return null;
        }
        UIController.PlayerKilled?.Invoke();
    }

    public void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero)
        {
            PlayerMoved?.Invoke();
            transform.Rotate(0,movement.x*1.5f,0);
            anim.SetInteger("Walk", 1);
        }
        else {
            anim.SetInteger("Walk", 0);
        }

        transform.Translate(new Vector3(0,0,moveVertical) * movementSpeed * Time.deltaTime, Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            PlayerAttacked?.Invoke();
            _pickUp.ThrowItem();
        }
    }
}