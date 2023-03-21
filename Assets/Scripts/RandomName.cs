using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RandomName : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    private List<string> _names = new List<string>() {
        "Леон",
        "Фредди",
        "Михаил",
        "Эдуард",
        "Валера",
        "Соня",
        "Вася",
        "Олег",
        "Александр",
        "Даниил",
        "Елисей",
        "Анжела",
        "Наталья",
        "Мария",
        "Елена",
        "Джон",
        "Исмаил",
        "Александра",
        "Хасбула",
        "Хабиб",
        "Хади",
        "Хагани",
        "Абдул",
        "Азамат",
        "Батыр",
        "Кира"
    };
    private void Start()
    {
        _name.text = _names[Random.Range(0, _names.Count)];
    }

}
