using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
public class Player : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    public static Player Instance;
    public Data data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadExtern();
        }
        else Destroy(gameObject);
    }
    public void Save()
    {
        string json = JsonConvert.SerializeObject(data);
        SaveExtern(json);
    }
    public void SetInfo(string value)
    {
        data = JsonConvert.DeserializeObject<Data>(value);
    }
}
