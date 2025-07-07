using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("파이프 생성 설정")]
    public GameObject pipePairPrefab; // 파이프 프리팹
    public float spawnInterval = 2f; // 파이프 생성 간격
    public float xSpawnPosition = 10f; // 파이프 생성 위치 (x 좌표)
    [Header("파이프 갭 설정")]
    public float minGapHeight = 2f; // 파이프 사이의 최소 간격
    public float maxGapHeight = 4f; // 파이프 사이의 최대 간격
    [Header("속도 증가 설정")]
    public float pipeMoveSpeed = 2f;
    public float speedUpInterval = 5f;
    public float speedUpAmount = 1.5f; // 속도 증가량
    public float maxSpeed = 8f; // 최대 속도
    [Header("생성 간격 설정")]
    public float minSpawnInterval = 0.8f; // 최소 생성 간격
    private float lastSpeedUpTime = 0f; // 마지막 속도 증가 시간
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpeedUpTime > speedUpInterval && pipeMoveSpeed < maxSpeed)
        {
            pipeMoveSpeed = Mathf.Min(pipeMoveSpeed + speedUpAmount, maxSpeed);
            lastSpeedUpTime = Time.time;// 마지막 속도 증가 시간 업데이트
            spawnInterval = Mathf.Max(spawnInterval - 0.9f, minSpawnInterval); // 생성 간격 감소
            CancelInvoke(nameof(SpawnPipes));
            InvokeRepeating(nameof(SpawnPipes), spawnInterval, spawnInterval); // 새로운 생성 간격으로 반복 호

        }
    }

    void SpawnPipes()
    {
        // 파이프 생성 위치 계산
        float gap = Random.Range(minGapHeight, maxGapHeight);
        float halfGap = gap / 2f;
        float camHalfHeight = Camera.main.orthographicSize;
        Transform pipeSprite = pipePairPrefab.transform.Find("PipeTop");
        float pipeHalfHeight = 0f;
        if (pipeSprite != null && pipeSprite.GetComponent<SpriteRenderer>() != null)
        {
            pipeHalfHeight = pipeSprite.GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        }
        float maxCenterY = camHalfHeight - halfGap - pipeHalfHeight;
        float minCenterY = -camHalfHeight + halfGap + pipeHalfHeight;
        float centerY = Random.Range(minCenterY, maxCenterY);
        Debug.Log($"파이프 중앙 Y 위치:{centerY}");

        GameObject pipePair = Instantiate(pipePairPrefab, new Vector3(xSpawnPosition, 0f, 0f), Quaternion.identity);
        Transform topPipe = pipePair.transform.Find("PipeTop");
        Transform bottomPipe = pipePair.transform.Find("PipeBottom");
        if (topPipe != null && bottomPipe != null)
        {
            topPipe.position = new Vector3(0, centerY + halfGap + pipeHalfHeight, 0f);
            bottomPipe.position = new Vector3(0, centerY - halfGap - pipeHalfHeight, 0f);
        }

    }
}
