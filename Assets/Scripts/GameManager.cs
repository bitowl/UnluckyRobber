using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    protected UI UI;
    public Player Player1;
    public Player Player2;
    [SerializeField]
    protected bool _coop;

    public bool Coop
    {
        get { return _coop; }
    }
    public int TimePerGame = 100;

    private bool _gameOver;

    public bool GameOver
    {
        get { return _gameOver; }
        set
        {
            _gameOver = value;
            if (Coop)
            {
                CoopEnd(value);
            }
            else
            {
                UI.GameOver.SetActive(_gameOver);
                UI.YouWon.SetActive(false);
            }
        }
    }

    public bool Win
    {
        set
        {
            _gameOver = value;
            if (Coop)
            {
                CoopEnd(value);
            }
            else
            {
                UI.YouWon.SetActive(value);
                UI.GameOver.SetActive(false);

            }
        }
    }

    private int _score1;
    private int _score2;
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

      //  DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitGame(InitialLevel);

    }

    // Initializes the game for each level.
    void InitGame(string levelName)
    {


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

        _time = TimePerGame;
        _score1 = 0;
        _score2 = 0;
        _currentVictims = 0;

        Debug.Log(SceneManager.GetActiveScene().name);
//        SceneManager.LoadScene("TestLevel_Stefan", LoadSceneMode.Additive);
    }

    public void StartLevelAfterLoad()
    {

        Player1.ResetPlayer();
        if (Coop)
        {
            Player2.ResetPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UI == null)
        {
            var ui = GameObject.Find("UI");
            if (ui != null)
            {
                UI = ui.GetComponent<UI>();
            }
        }

        if (!_gameOver)
        {
            _time -= Time.deltaTime;
            if (_time < 0)
            {
                _time = 0;
                if (Coop)
                {
                    GameOver = true;
                } else { 
                    Player1.Die("You ran out of time");
                }
                // TODO: player2 (?)
            }
        }

       // Debug.Log(_score + "/" + _currentVictims);
        if (_score1 + _score2 >= _currentVictims && _currentVictims != 0)
        {
            Win = true;
        }

        if (UI)
        {
            UI.Time = (int)_time;
            UI.Score1 = _score1;
            UI.Score2 = _score2;
        }


        // quit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("startscreen");
        }
    }


    public void RegisterVictim()
    {
        _currentVictims++;
    }

    public void AddScore(Victim victim, int score)
    {
        if (victim.Thrower == Player1)
        {
            AddScore1(score);
        } else if (victim.Thrower == Player2)
        {
            AddScore2(score);
        }
        Destroy(victim); // don't count the same victim twice
    }

    public void AddScore1(int score)
    {
        _score1 += score;
        Player1._soundPlayer.Score();
    }

    public void AddScore2(int score)
    {
        _score2 += score;
        Player2._soundPlayer.Score();
    }

    public void Restart()
    {
        GameOver = false;
        InitGame(_currentLevel);
    }

    private void CoopEnd(bool value)
    {
        if (value)
        {

            if (_score1 > _score2)
            {
                UI.BlueWins.SetActive(true);
            }
            else if (_score1 < _score2)
            {
                UI.RedWins.SetActive(true);
            }
            else
            {
                UI.GameOver.SetActive(true);
            }
        }
        else
        {
            UI.BlueWins.SetActive(false);
            UI.RedWins.SetActive(false);
            UI.GameOver.SetActive(false);
        }
    }
}