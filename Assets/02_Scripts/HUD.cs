using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Time,
        Score
    }

    public InfoType type;

    Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
    }

    void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Time:
                this.myText.text = $"시간 : {Util.FormatTime(GameManager.instance.gameTime)}";
                break;
            case InfoType.Score:
                int score = GameManager.instance.score;
                this.myText.text = $"점수 : {Util.FormatIntToReadableString(score)}";
                break;
        }
    }
}
