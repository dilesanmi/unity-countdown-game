using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float timer = 0;

    [SerializeField] private TMP_Text timeText;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject gameWonScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        gameOverScreen.SetActive(false);
        gameWonScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        timeText.text = "Time remaining: " + ((int)timer);

        if (timer >= 120)
        {
            GameOver();
        }
    }


    public void GameOver()
    {
        Debug.Log("You lost!");
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);

    }

    public void GameWon()
    {
        Debug.Log("You win!");
        Time.timeScale = 0f;
        gameWonScreen.SetActive(true);

    }

    public void ResetGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

}
