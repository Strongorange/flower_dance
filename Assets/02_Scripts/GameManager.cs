using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime; // 게임 경과시간
    public int score; // 꽃 먹어서 얻은 점수
    public float finalScore; // 점수 * 살아남은 시간
    public PoolManager pool;

    public bool isGameOver;
    public GameObject gameOverUI;

    // UI 텍스트 요소
    public Text timeText;
    public Text scoreText;
    public Text totalScoreText;

    void Awake()
    {
        Application.targetFrameRate = 60;
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
        // 묘비 먹어서 게임 끝
        this.isGameOver = true;
        this.finalScore = score * gameTime;

        // 게임 종료에 표시할 텍스트 업데이트
        timeText.text = "시간: " + Util.FormatTime(gameTime); // FormatTime 함수 사용
        scoreText.text = "획득한 점수: " + Util.FormatIntToReadableString(score);
        totalScoreText.text =
            "최종점수: " + Util.FormatIntToReadableString(Mathf.FloorToInt(finalScore));
        // 게임 종료 UI 표시
        this.gameOverUI.SetActive(true);

        // 시간 멈추기
        Time.timeScale = 0;
    }

    public void GameStart()
    {
        // 게임 시작
        this.isGameOver = false;
        this.gameTime = 0;
        this.score = 0;
        Time.timeScale = 1;

        // 게임 종료 UI 숨기기
        this.gameOverUI.SetActive(false);
    }

    public void GetPoint(int score)
    {
        this.score += score;
    }

    public void SaveGame()
    {
        // DB에 정보 저장
        Debug.Log("저장 함수 실행");
    }
}
