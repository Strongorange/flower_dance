using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public int score;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            Destroy(this.gameObject);
        }
    }
}
