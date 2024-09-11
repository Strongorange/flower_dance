using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int speed;
    Rigidbody2D rigid;
    public Vector2 inputVec;
    SpriteRenderer spriteRenderer;
    private bool isTouchActive = false; // 터치중 여부

    void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        Vector2 newPosition = rigid.position + nextVec;

        //화면 경계 계산
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(
            new Vector2(Screen.width, Screen.height)
        );
        float xLimit = screenBounds.x;
        float yLimit = screenBounds.y;

        newPosition.x = Mathf.Clamp(newPosition.x, -xLimit, xLimit);
        newPosition.y = Mathf.Clamp(newPosition.y, -yLimit, yLimit);

        rigid.MovePosition(newPosition);

        if (inputVec.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.primaryTouch.press.isPressed)
            {
                isTouchActive = true;
                Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                if (touchPosition.x < Screen.width / 2)
                {
                    inputVec = new Vector2(-1, 0);
                }
                else
                {
                    inputVec = new Vector2(1, 0);
                }
            }
            else
            {
                isTouchActive = false;
                inputVec = Vector2.zero; // 터치가 비활성화된 경우 이동 중지
            }
        }
    }

    // void OnMove(InputValue input)
    // {
    //     if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
    //     {
    //         isTouchActive = true;
    //         // 화면 오른쪽 왼쪽 터치 판단
    //         Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
    //         if (touchPosition.x < Screen.width / 2)
    //         {
    //             inputVec = new Vector2(-1, 0);
    //         }
    //         else
    //         {
    //             inputVec = new Vector2(1, 0);
    //         }
    //     }
    //     else
    //     // 키보드 입력 처리
    //     {
    //         // 손가락 떼지면 터치중 상태 false;
    //         isTouchActive = false;

    //         // 좌우 움직임만 받기위해 inputVec을 입력의 X 축만 사용
    //         Vector2 rawInput = input.Get<Vector2>();
    //         this.inputVec = new Vector2(rawInput.x, 0);
    //         // 상하 좌우 입력을 모두 받으려면 아래 주석만 사용
    //         // this.inputVec = input.Get<Vector2>();
    //     }
    // }

    void OnMove(InputValue input)
    {
        if (Touchscreen.current == null || !isTouchActive)
        {
            // 키보드 입력 처리
            Vector2 rawInput = input.Get<Vector2>();
            this.inputVec = new Vector2(rawInput.x, 0);
            // 상하 좌우 입력을 모두 받으려면 아래 주석만 사용
            // this.inputVec = input.Get<Vector2>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            Debug.Log("꽃과 충돌함!");
            other.gameObject.SetActive(false);
            // 꽃의 점수 판별해 점수 추가.
            int score = other.GetComponent<Flower>().score;
            Debug.Log($"꽃 점수 : {score}");
            GameManager.instance.GetPoint(score);
            // TODO : 사운드 추가
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("스파이크와 충돌!");
            int currentScore = GameManager.instance.score;
            int obstacleScore = other.gameObject.GetComponent<Obstacle>().score;
            int nextScore = Mathf.Max(0, currentScore - Mathf.Abs(obstacleScore));
            GameManager.instance.score = nextScore;
            other.gameObject.SetActive(false);
            // TODO : 사운드 추가
        }
        else if (other.gameObject.CompareTag("GameOverGrave"))
        {
            Debug.Log("게임 오버 무덤 접촉");
            GameManager.instance.GameOver();
        }
    }
}
