using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : StatefullMonobehaviour
{
    private enum GameState
    {
        INITIAL,
        MENU,
        CONTROLS,
        INGAME,
        HIGHSCORE,
        MIDSCORE,
        LOWSCORE,
        QUIT
    };

    private static Game Instance;

    protected override void DoStart()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public static Game GetInstance()
    {
        return Instance;
    }

    protected override void DoUpdate()
    {
        //handle the state
        HandleGameState();
    }

    private void HandleGameState()
    {
        GameState gameState = (GameState)Enum.Parse(typeof(GameState), _state, true);

        switch (gameState)
        {
            case GameState.INITIAL:
                ToMenu();
                break;

            case GameState.MENU:
                if (Input.GetKeyDown(KeyCode.Return))
                    ToControls();

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToQuit();

                break;

            case GameState.CONTROLS:

                if (Input.GetKeyDown(KeyCode.Return))
                    ToGame();

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToMenu();

                break;

            case GameState.INGAME:

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToMenu();

                break;

            case GameState.HIGHSCORE:
                if (Input.GetKeyDown(KeyCode.Return))
                    ToMenu();

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToMenu();

                break;

            case GameState.MIDSCORE:
                if (Input.GetKeyDown(KeyCode.Return))
                    ToMenu();

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToMenu();

                break;

            case GameState.LOWSCORE:
                if (Input.GetKeyDown(KeyCode.Return))
                    ToMenu();

                if (Input.GetKeyDown(KeyCode.Escape))
                    ToMenu();

                break;

            case GameState.QUIT:

                break;
        }
    }

    protected override string GetInitialState()
    {
        return GameState.INITIAL.ToString();
    }

    void ToMenu()
    {
        SetState(GameState.MENU.ToString());
        SceneManager.LoadScene("Menu");
        //RoundManager.Instance.ResetRound();
        ScoreManager.Instance.ResetScore();
    }

    void ToGame()
    {
        SetState(GameState.INGAME.ToString());
        SceneManager.LoadScene("Game");
    }

    void ToControls()
    {
        SetState(GameState.CONTROLS.ToString());
        SceneManager.LoadScene("Controls");
    }
    public void ToHighScore()
    {
        SetState(GameState.HIGHSCORE.ToString());
        SceneManager.LoadScene("HighScore");
    }

    public void ToMidScore()
    {
        SetState(GameState.MIDSCORE.ToString());
        SceneManager.LoadScene("MidScore");
    }

    public void ToLowScore()
    {
        SetState(GameState.LOWSCORE.ToString());
        SceneManager.LoadScene("LowScore");
    }

    void ToQuit()
    {
        SetState(GameState.QUIT.ToString());
        Application.Quit();
    }


}