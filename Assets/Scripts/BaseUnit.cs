using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovable
{
    public void Move();
}


public class BaseUnit : MonoBehaviour
{
    protected float movementSpeed = 1;
    protected float jumpForce = 300;
    protected float timeBeforeNextJump = 1.2f;
    protected float canJump = 0f;
    protected Animator anim;
    protected Rigidbody rb;
    protected UnitStatus status = UnitStatus.Stay;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        status = UnitStatus.Death;
    }
    private void OnTriggerStay(Collider other)
    {
        status = UnitStatus.Death;
    }
    private void OnTriggerExit(Collider other)
    {
        status = UnitStatus.Death;
    }
}
