using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RandomName : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    private List<string> _names = new List<string>() {
        "����",
        "������",
        "������",
        "������",
        "������",
        "����",
        "����",
        "����",
        "���������",
        "������",
        "������",
        "������",
        "�������",
        "�����",
        "�����",
        "����",
        "������",
        "����������",
        "�������",
        "�����",
        "����",
        "������",
        "�����",
        "������",
        "�����",
        "����"
    };
    private void Start()
    {
        _name.text = _names[Random.Range(0, _names.Count)];
    }

}
