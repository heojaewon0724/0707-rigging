using UnityEngine;

public class PipeMover : MonoBehaviour
{
    public float destoryX = -15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        float moveSpeed = FindAnyObjectByType<PipeSpawner>().pipeMoveSpeed;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destoryX)
        {
            Destroy(gameObject);
        }
    }
}
