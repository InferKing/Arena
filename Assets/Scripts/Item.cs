using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    public bool isThrowed, isOnEarth = false, pickedUp = false;
    private Coroutine _coroutine;
    private void Start()
    {
        UpdateStatus();
    }
    public void UpdateStatus()
    {
        isThrowed = true;
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(CheckStay());
    }
    private IEnumerator CheckStay()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        yield return new WaitUntil(() => _rb.velocity == Vector3.zero || isOnEarth);
        isThrowed = false;
        pickedUp = false;
    }
    private void OnTriggerStay(Collider other)
    {
        _rb.velocity = Vector3.zero;
        gameObject.transform.position = ItemSource.Instance.GetRandomPlace();
        gameObject.transform.rotation = Quaternion.identity;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Respawn"))
        {
            isOnEarth = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Respawn"))
        {
            isOnEarth = false;
        }
    }
}
