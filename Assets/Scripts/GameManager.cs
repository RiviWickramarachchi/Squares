using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text scoreValueText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject timerAndScore;
    public GameState gameState;
    public static event Action SendSignalToSpawner;
    public static event Action DestroySquaresOnGameEnd;
    private float timeVal;
    private int score;
     private void Awake() {
        Assert.IsNotNull(timerText);
        Assert.IsNotNull(scoreText);
        Assert.IsNotNull(timerAndScore);
        Assert.IsNotNull(scoreValueText);
        SquareBehavior.OnSquareDestroyed += AddTimeAndScore;
    }
    void Start()
    {
        InitializeGameValues();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState == GameState.Game) {
            UpdateTimer();
        }
    }

    private void UpdateTimer() {
        timeVal -= 1 * Time.deltaTime;
        if(timeVal <= 0) {
            SendSignalToSpawner?.Invoke();
            DestroySquaresOnGameEnd?.Invoke();
            gameState = GameState.End;
            timerAndScore.SetActive(false);
            gameOverPanel.SetActive(true);
            scoreValueText.text = score.ToString();
        }
        if (timeVal < 10f) {
                timerText.text = (timeVal.ToString("0.0"));
            } else {
                timerText.text = (timeVal.ToString("0"));
            }

    }

    public void InitializeGameValues() {
        timeVal = 15f;
        score = 0;
        scoreText.text = score.ToString();
        gameOverPanel.SetActive(false);
        timerAndScore.SetActive(true);
        gameState = GameState.Game;
    }

    public void  AddTimeAndScore() {
        timeVal += 5f;
        score += 5;
        scoreText.text = score.ToString();

    }

    void OnDisable() {
        SquareBehavior.OnSquareDestroyed -= AddTimeAndScore;
    }
}

public enum GameState {
    Begin,
    Game,
    End
}
