using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdvFull();
    private void Start()
    {
        ShowAdvFull();
    }
}
