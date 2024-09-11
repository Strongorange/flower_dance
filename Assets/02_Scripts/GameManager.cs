using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime; // 게임 경과시간
    public int score; // 꽃 먹어서 얻은 점수
    public float finalScore; // 점수 * 살아남은 시간
    public PoolManager pool;

    public bool isGameOver;
    public GameObject gameOverUI;

    void Awake()
    {
        instance = this;
        isGameOver = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            return;
        }
        this.gameTime += Time.deltaTime;
    }

    public void GameOver()
    {
        // 쓰레기 먹어서 게임 끝
        this.isGameOver = true;
        this.finalScore = score * gameTime;
        // 시간 리셋
        Time.timeScale = 0;
        // 게임 종료 UI 표시
    }

    public void GameStart()
    {
        // 게임 시작
        this.isGameOver = false;
        this.gameTime = 0;
        this.score = 0;
        Time.timeScale = 1;

        // 게임 종료 UI 숨기기
    }

    public void GetPoint(int score)
    {
        this.score += score;
    }
}
