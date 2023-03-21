using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitStatus
{ 
    Stay,
    GoTo,
    Attack,
    Death
}

public class Enemy : BaseUnit, IMovable
{
    [SerializeField] private PickUpItem _pickUp;
    private GameObject target;
    private void OnEnable()
    {
        PickUpItem.PickedUp += GetItem;
        MainController.GameStarted += OnStart;
    }
    private void OnDisable()
    {
        PickUpItem.PickedUp -= GetItem;
        MainController.GameStarted -= OnStart;
    }
    private void OnStart()
    {
        StartCoroutine(StartLive());
    }
    private IEnumerator StartLive()
    {
        while (status != UnitStatus.Death)
        {
            switch (status)
            {
                case UnitStatus.Stay:
                    anim.SetInteger("Walk", 0);
                    float distance = 100f;
                    foreach (var item in ItemSource.Instance.Items)
                    {
                        if (!item.isThrowed && distance > (transform.position - item.gameObject.transform.position).magnitude)
                        {
                            distance = (transform.position - item.gameObject.transform.position).magnitude;
                            target = item.gameObject;
                            if (status != UnitStatus.Attack) status = UnitStatus.GoTo;
                        }
                    }
                    break;
                case UnitStatus.GoTo:
                    Move();
                    break;
                case UnitStatus.Attack:
                    anim.SetInteger("Walk", 0);
                    target = UnitsPos.Instance.GetNearUnitGO(gameObject);
                    yield return StartCoroutine(Delay(0.8f));
                    _pickUp.ThrowItem();
                    status = UnitStatus.Stay;
                    break;
            }
            yield return null;
        }
        UIController.EnemyKilled?.Invoke();
    }
    private IEnumerator Delay(float f)
    {
        for (float time = 0; time < f/2; time += Time.deltaTime)
        {
            Vector3 movement = (target.transform.position - transform.position).normalized;
            movement.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    360 * Time.deltaTime);
            rb.MoveRotation(targetRotation);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        anim.SetTrigger("jump");
        for (float time = f/2; time < f; time += Time.deltaTime)
        {
            Vector3 movement = (target.transform.position - transform.position).normalized;
            movement.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    360 * Time.deltaTime);
            rb.MoveRotation(targetRotation);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    public void Move()
    {
        anim.SetInteger("Walk", 1);
        Vector3 movement = (target.transform.position - transform.position).normalized;
        movement.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.deltaTime);
        rb.MovePosition(rb.position + movement * 0.7f * Time.deltaTime);
        rb.MoveRotation(targetRotation);
    }
    private void GetItem(PickUpItem item)
    {
        if (item != _pickUp) return;
        status = UnitStatus.Attack;
    }
}
