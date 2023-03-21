using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartTrans : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Player.Instance.data.getTutorial ? 1 : 3);
    }
}
