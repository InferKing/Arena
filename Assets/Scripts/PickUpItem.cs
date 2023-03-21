using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PickUpItem : MonoBehaviour
{
    public static Action<PickUpItem> PickedUp;
    [SerializeField] private GameObject _place;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _force;
    private bool _isPickup = false;
    private GameObject _item = null;
    private Rigidbody _rbItem;

    private void Update()
    {
        if (_isPickup && _item != null)
        {
            _item.transform.position = _place.transform.position;
            _item.transform.rotation = transform.rotation;
            return;
        }
        Collider[] colls = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (Collider coll in colls)
        {
            if (coll.gameObject.CompareTag("Item") && !_isPickup && !coll.gameObject.GetComponent<Item>().isThrowed &&
                !coll.gameObject.GetComponent<Item>().pickedUp)
            {
                coll.gameObject.GetComponent<Item>().pickedUp = true;
                _isPickup = true;
                coll.gameObject.transform.position = _place.transform.position;
                _item = coll.gameObject;
                _rbItem = _item.GetComponent<Rigidbody>();
                _rbItem.useGravity = false;
                PickedUp?.Invoke(this);
                break;
            }
        }
    }
    public void ThrowItem()
    {
        if (_isPickup && _item != null)
        {
            StartCoroutine(Delay());
            _rbItem.useGravity = true;
            Vector3 v = (_item.transform.position - transform.position) * _force;
            v.y = 0;
            _rbItem.AddForce(v, ForceMode.Impulse);
            _item.gameObject.GetComponent<Item>().UpdateStatus();
            _item = null;
            _rbItem = null;
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        _isPickup = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Item"))
        {
            Item item = collision.collider.gameObject.GetComponent<Item>();
            if (item.isThrowed)
            {
                _rigidbody.AddForce((transform.position - collision.transform.position) * 20, ForceMode.Impulse);
            }
        }
    }
}
