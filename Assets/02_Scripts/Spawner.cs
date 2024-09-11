using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float initialMaxInterval; // 시작 시 최대 스폰 간격
    public float minInterval; // 스폰 간격의 최저 값
    public float maxInterval;
    public float reductionAmount; // 줄어드는 간격
    public float reductionInterval; // 간격이 줄어드는 주기 (초)

    public float spawnInterval; // 현재 스폰 간격
    public float spawnTimer;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

    void Awake()
    {
        this.mainCamera = Camera.main;
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.spawnInterval = Random.Range(minInterval, initialMaxInterval);
    }

    void Update()
    {
        spawnTimer += Time.deltaTime;

        float gameTime = GameManager.instance.gameTime;
        // 게임 경과 시간에 따라 스폰 간격 줄이기
        float reduction = Mathf.Floor(gameTime / reductionInterval) * reductionAmount;
        this.maxInterval = Mathf.Max(minInterval, initialMaxInterval - reduction);

        if (spawnTimer >= spawnInterval)
        {
            Spawn();
            spawnTimer = 0;

            this.spawnInterval = Random.Range(minInterval, maxInterval);
        }
    }

    void Spawn()
    {
        Vector2 spawnPosition = GetRandomPositionInZone();
        int flowerCount = GameManager.instance.pool.prefabs.Length;
        int flowerIndex = Random.Range(0, flowerCount);

        Debug.Log($"Flower Index is : {flowerIndex}");

        GameObject flower = GameManager.instance.pool.Get(flowerIndex);
        flower.transform.position = spawnPosition;
    }

    Vector2 GetRandomPositionInZone()
    {
        // 카메라의 뷰포트를 기준으로 화면의 크기를 얻기
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z)
        );

        float randomX = Random.Range(-screenBounds.x, screenBounds.x);

        return new Vector2(randomX, screenBounds.y); // 화면 상단에서 떨어지는 위치
    }
}
