using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField]
    public Players player;
    [SerializeField]
    public CameraController cameraController;
    [SerializeField]
    private UIController uiController;

    public static UIController UIController
    {
        get => instance.uiController;
    }


    private int gamelevel;
    public static int GameLevel
    {
        get => instance.gamelevel;
    }

    private bool isgame;
    public static bool IsGame
    {
        get => instance.isgame; 
    }

    

    private void Awake()
    {
        Debug.Log("awake");
        FindObjectOfType<Spawner>().OnNewWave += NextLevel;

        FindObjectOfType<Players>().OnDeath += OnGameOver;//eventhandler '+='

        if (instance == null)
        {
            Debug.Log("Single instance is null");
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Single instance is not Single.. Destroy gameobject!");
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);//Dont destroy this singleton gameobject :(
    }

    void OnGameOver()
    {
        isgame = false;
        gamelevel = 1;
        uiController.Ui_Setting(isgame);
    }

    void NextLevel()
    {
        gamelevel++;
        uiController.ClearStage();


       
        
    }

    
}
