using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitsPos : MonoBehaviour
{
    [SerializeField] private BaseUnit[] _units;
    public static UnitsPos Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
    }
    public void Init()
    {
        _units = FindObjectsOfType<BaseUnit>();
    }
    public GameObject GetRandomUnitGO() => _units[Random.Range(0, _units.Length)].gameObject;
    public GameObject GetNearUnitGO(GameObject unit)
    {
        float distance = 1000f;
        GameObject temp = null;
        foreach (BaseUnit item in _units)
        {
            if (distance > (item.gameObject.transform.position-unit.transform.position).magnitude && unit != item.gameObject)
            {
                distance = (item.gameObject.transform.position - unit.transform.position).magnitude;
                temp = item.gameObject;
            }
        }
        return temp;
    }
}
