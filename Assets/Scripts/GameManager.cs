using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UI UI;
    public Player Player;

    public int TimePerGame = 100;

    private bool _gameOver;

    public bool GameOver
    {
        get { return _gameOver; }
        set
        {
            _gameOver = value;
            UI.GameOver.SetActive(_gameOver);
            UI.YouWon.SetActive(false);
        }
    }

    public bool Win
    {
        set
        {
            _gameOver = value;
            UI.YouWon.SetActive(value);
            UI.GameOver.SetActive(false);
        }
    }

    private int _score;
    private float _time;

    public string InitialLevel;
    private string _currentLevel;
    private int _currentVictims;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        InitGame(InitialLevel);
    }

    void Start()
    {
        if (UI == null)
        {
            UI = GameObject.Find("UI").GetComponent<UI>();
        }
    }

    // Initializes the game for each level.
    void InitGame(string levelName)
    {
        _time = TimePerGame;
        _score = 0;
        _currentVictims = 0;
        Player.ResetPlayer();

        var uiLoaded = false;
        var nextSceneLoaded = false;
        if (_currentLevel != null)
        {
            SceneManager.UnloadSceneAsync(_currentLevel);
        }

        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name == "UI")
            {
                uiLoaded = true;
            }
            if (SceneManager.GetSceneAt(i).name == levelName)
            {
                nextSceneLoaded = true;
            }
        }
        if (!uiLoaded)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
        if (!(nextSceneLoaded && _currentLevel == null))
        {
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
            
        }
        _currentLevel = levelName;
        Debug.Log(SceneManager.GetActiveScene().name);
//        SceneManager.LoadScene("TestLevel_Stefan", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameOver)
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                _time = 0;
                Player.Die("You ran out of time");
            }
        }

        Debug.Log(_score + "/" + _currentVictims);
        if (_score >= _currentVictims)
        {
            Win = true;
        }

        UI.Time = (int) _time;
        UI.Score = _score;
    }


    public void RegisterVictim()
    {
        _currentVictims++;
    }

    public void AddScore(int score)
    {
        _score += score;
        Player._soundPlayer.Score();

    }

    public void Restart()
    {
        GameOver = false;
        // TODO unload scene
        InitGame(_currentLevel);
    }
}