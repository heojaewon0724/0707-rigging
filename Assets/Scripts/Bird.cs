using Unity.Mathematics;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpForce = 3f; // Force applied when the bird jumps
    public float rotateSpeed = 3f; // Speed of rotation when the bird jumps
    private Rigidbody2D rd;
    private Animator animator;

    public bool isLive = true; // Flag to check if the bird is alive
    private bool isDeadRotating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive)
        {
            if (isDeadRotating)
            {
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, -90f),
                    Time.deltaTime * rotateSpeed
                );
            }
            return;

        }

        if (Input.GetMouseButtonDown(0))
        {
            rd.linearVelocity = Vector2.up * jumpForce; // Apply upward force to the bird
        }
        float vy = rd.linearVelocity.y;
        float targetAngle = vy > 0.1f ? 45f : (vy < -0.1f ? -45f : 0f);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            Time.deltaTime * rotateSpeed
        );

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLive) return;

        // 땅이나 파이프에 부딪히면 게임 오버 처리
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe"))
        {
            isLive = false;
            isDeadRotating = true; // 회전 시작
            Debug.Log("Game Over");
            animator.SetBool("Dead", true); // 죽음 애니메이션 실행
            GameManager.Instance.GameOver(); // 게임 매니저에서 게임 오버 알림
        }
    }
}
