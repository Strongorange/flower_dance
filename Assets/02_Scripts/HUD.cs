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
                this.myText.text = Util.FormatTime(GameManager.instance.gameTime);
                break;
            case InfoType.Score:
                break;
        }
    }
}
