using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int speed;
    Rigidbody2D rigid;
    public Vector2 inputVec;

    void Awake()
    {
        this.rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue input)
    {
        // 좌우 움직임만 받기위해 inputVec을 입력의 X 축만 사용
        Vector2 rawInput = input.Get<Vector2>();
        this.inputVec = new Vector2(rawInput.x, 0);
        // 상하 좌우 입력을 모두 받으려면 아래 주석만 사용
        // this.inputVec = input.Get<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            Debug.Log("꽃과 충돌함!");
            Destroy(other.gameObject);
            // 꽃의 점수 판별해 점수 추가.
            int score = other.GetComponent<Flower>().score;
            Debug.Log($"꽃 점수 : {score}");
            GameManager.instance.GetPoint(score);
            // TODO : 사운드 추가
        }
    }
}
