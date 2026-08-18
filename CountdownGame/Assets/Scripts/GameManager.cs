using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float timer = 0;

    int hours, minutes;
    private int startHour = 9;

    private int lastClockUpdate = -1;

    private float secondsPerHour = 30;//So a 9-5 would be 240 seconds aka 4 minutes

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
        int quarter = Mathf.FloorToInt(timer / (secondsPerHour / 4));

        if (quarter != lastClockUpdate)
        {
            lastClockUpdate = quarter;
            UpdateClock();
        }

        timer += Time.deltaTime;

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

    void UpdateClock()
    {
        timeText.text = ConvertTimerToClock();
    }

    public string ConvertTimerToClock() {

        float totalMinutesInGame = (timer / secondsPerHour) * 60f;

        hours = startHour + Mathf.FloorToInt(totalMinutesInGame / 60f);
        minutes = Mathf.FloorToInt(totalMinutesInGame % 60f);

        return $"{hours}:{minutes:00}";
    }

}
