using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime; // 게임 경과시간
    public int score; // 꽃 먹어서 얻은 점수
    public float finalScore; // 점수 * 살아남은 시간

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        this.gameTime += Time.deltaTime;
    }

    void GameOver()
    {
        // 쓰레기 먹어서 게임 끝
        // 시간 리셋
        // 게임 종료 UI 표시
    }

    void GameStart()
    {
        // 게임 시작
    }

    public void GetPoint(int score)
    {
        this.score += score;
    }
}
