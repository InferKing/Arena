using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSource : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject _item;
    private List<Vector3> _places;
    public static ItemSource Instance { get; private set; }
    public List<Item> Items { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(this);
    }
    public void Init()
    {
        _playerController = FindObjectOfType<PlayerController>();
        Items = new List<Item>();
        _places = new List<Vector3>();
        for (int i = 0; i < 9; i++)
        {
            _places.Add(new Vector3(i % 3 - 1, 3, i / 3 - 1));
            GameObject go = Instantiate(_item);
            go.transform.position = _places[i];
            go.transform.rotation = Quaternion.identity;
            Items.Add(go.GetComponent<Item>());
        }
    }
    public Vector3 GetRandomPlace() => _places[Random.Range(0, _places.Count)];
    public Vector3 GetPlayerPos() => _playerController.gameObject.transform.position;
}
