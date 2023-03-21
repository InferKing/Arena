using UnityEngine;
using System;
public class MainController : MonoBehaviour
{
    [SerializeField] private GameObject[] _persons;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _virtualCamera;
    public static Action GameStarted;
    private void Awake()
    {
        GameObject player = Instantiate(_persons[Player.Instance.data.indexPicked]);
        player.transform.position = new Vector3(0.3f, 0.4f, -2.5f);
        _virtualCamera.Follow = player.transform;
        _virtualCamera.LookAt = player.transform;
        ItemSource.Instance.Init();
        UnitsPos.Instance.Init();
        
    }
}
