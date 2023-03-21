using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _text;
    [SerializeField] private GameObject _box;
    [SerializeField] private TransitionScenes _scenes;
    private bool _isPlaying = false, _isPlayingAttack = false, _isGet = false;
    private void OnEnable()
    {
        PlayerController.PlayerMoved += SetBool;
        PlayerController.PlayerAttacked += SetBoolAttack;
        PickUpItem.PickedUp += GetItem;
    }
    private void OnDisable()
    {
        PlayerController.PlayerMoved -= SetBool;
        PlayerController.PlayerAttacked -= SetBoolAttack;
        PickUpItem.PickedUp -= GetItem;
    }
    private void Start()
    {
        MainController.GameStarted?.Invoke();
        StartCoroutine(Tutorials());
    }
    private IEnumerator Tutorials()
    {
        _text.text = "Привет!";
        yield return new WaitForSeconds(1f);
        _text.text = "Сейчас мы научимся двигаться";
        yield return new WaitForSeconds(2f);
        _text.text = "WASD - управление персонажем";
        _isPlaying = false;
        yield return new WaitUntil(() => _isPlaying);
        _text.text = "Отлично!";
        yield return new WaitForSeconds(1f);
        _text.text = "Теперь подбери ящик";
        GameObject go = Instantiate(_box);
        go.transform.position = new Vector3(0, 2, 0);
        yield return new WaitUntil(() => _isGet);
        _text.text = "Круто!";
        yield return new WaitForSeconds(1f);
        _text.text = "Теперь нажми ЛКМ, чтобы бросить ящик";
        _isPlayingAttack = false;
        yield return new WaitUntil(() => _isPlayingAttack);
        _text.text = "Прекрасно!";
        yield return new WaitForSeconds(1f);
        _text.text = "Обучение завершено.";
        yield return new WaitForSeconds(2f);
        _text.text = "Теперь перейдем на реальную арену!";
        Player.Instance.data.getTutorial = true;
        Player.Instance.Save();
        yield return new WaitForSeconds(2f);
        _scenes.SetScene(2);

    }
    private void SetBool()
    {
        _isPlaying = true;
    }
    private void SetBoolAttack()
    {
        _isPlayingAttack = true;
    }
    private void GetItem(PickUpItem pick)
    {
        _isGet = true;
    }
}
