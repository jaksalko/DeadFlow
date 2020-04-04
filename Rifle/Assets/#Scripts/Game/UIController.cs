using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject ingame;
    [SerializeField]
    private GameObject endgame;

    public Image fadePlane;

    [SerializeField]
    public HealthBar hpslider;//prefab


   


    public void Ui_Setting(bool isgame)
    {
        ingame.SetActive(isgame);
        endgame.SetActive(!isgame);
        if(!isgame)
        {
            StartCoroutine(Fade(Color.clear, Color.black, 1));
        }
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    public void ClearStage()
    {

    }

    // UI Input
    public void StartNewGame()
    { 
        SceneManager.LoadScene("Game");
    }

    public void ClickNextStage()
    {

    }

}
