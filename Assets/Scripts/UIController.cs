using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class UIController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _status;
    [SerializeField] private TMP_Text _congrat;
    [SerializeField] private TMP_Text _coins;
    private int _count = 0;
    public static Action EnemyKilled;
    public static Action PlayerKilled;

    private void OnEnable()
    {
        EnemyKilled += UpdateStatus;
        PlayerKilled += ShowLoseUI;
    }
    private void OnDisable()
    {
        EnemyKilled -= UpdateStatus;
        PlayerKilled -= ShowLoseUI;
    }
    private void Start()
    {
        _coins.text = Player.Instance.data.money.ToString();
    }
    private void UpdateStatus()
    {
        Player.Instance.data.money += 1;
        _coins.text = Player.Instance.data.money.ToString();
        _count++;
        _status.text = _count.ToString() + "/7";
        if (_count == 7)
        {
            if (!_animator.GetBool("Happened")) _congrat.text = "Победа!";
            _animator.SetBool("Happened", true);
        }
        Player.Instance.Save();
    }
    private void ShowLoseUI()
    {
        if (!_animator.GetBool("Happened")) _congrat.text = "Поражение!";
        _animator.SetBool("Happened", true);
    }
    public void SetScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
