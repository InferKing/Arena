using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InGameStart : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }
    private IEnumerator StartDelay()
    {
        _text.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _text.text = "Погнали!";
        MainController.GameStarted?.Invoke();
        yield return new WaitForSeconds(1);
        _text.gameObject.SetActive(false);
    }
}
