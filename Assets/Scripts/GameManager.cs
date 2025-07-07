using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject GameOverUI;
    public float timer = 0;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timerTextUI;
    public Bird bird;
    void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame
    private void Update()
    {
        if (bird.isLive)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F2");
        }
    }
    public void GameOver()
    {
        Invoke("GameOverUIon", 1.5f);
        timerTextUI.text = "Time: " + timer.ToString("F2");
        timerText.gameObject.SetActive(false);
    }
    public void GameOverUIon()
    {
        GameOverUI.SetActive(true);
        bird.GetComponent<SpriteRenderer>().enabled = false; // Disable the bird's sprite renderer

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
