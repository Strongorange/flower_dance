using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public enum ObstacleType
    {
        Spike,
        Thorn,
        Grave
    }

    public int score;
    public bool isGameOver;

    public ObstacleType obstacle;

    void Awake()
    {
        switch (this.obstacle)
        {
            case ObstacleType.Spike:
                this.score = -100;
                break;
            case ObstacleType.Thorn:
                this.score = -300;
                break;
            case ObstacleType.Grave:
                this.score = -999999;
                this.isGameOver = true;
                break;
            default:
                this.score = -100;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
