using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gamaManager;

    // UI 텍스트 요소
    public Text timeText;
    public Text scoreText;
    public Text totalScoreText;

    void Update()
    {
        if (!gamaManager.isGameOver)
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        timeText.text = "시간: " + Util.FormatIntToReadableString(this.gamaManager.score);
        scoreText.text = "획득한 점수: " + Util.FormatIntToReadableString(this.gamaManager.score);
    }

    public void DisplayGameOverUI(float finalScore)
    {
        totalScoreText.text =
            "최종점수 : " + Util.FormatIntToReadableString(Mathf.FloorToInt(finalScore));
    }
}
