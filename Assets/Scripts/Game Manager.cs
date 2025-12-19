using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float gameTime;
    public bool gameActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        gameActive = true;
    }

    void Update()
    {
        if (gameActive)
        {
            gameTime += Time.deltaTime;
            UIController.Instance.UpdateTimer(gameTime);
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }
    }

    public void GameOver()
{
    gameActive = false;

    // Lấy điểm hiện tại
    int currentPoint = PlayerController.Instance.currentPoint;

    // Lấy best point đã lưu
    int bestPoint = PlayerPrefs.GetInt("BestPoint", 0);

    // Nếu điểm hiện tại cao hơn → lưu lại
    if (currentPoint > bestPoint)
    {
        PlayerPrefs.SetInt("BestPoint", currentPoint);
        PlayerPrefs.Save();
        bestPoint = currentPoint;
    }

    // Update UI
    UIController.Instance.UpdatePointText(currentPoint);
    UIController.Instance.UpdateBestPointText();

    // Hiện màn hình Game Over (giữ nguyên logic cũ)
    StartCoroutine(ShowGameOverScreen());
}


    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);
        UIController.Instance.gameOverPanel.SetActive(true);
        AudioController.Instance.PlayModifiedSound(AudioController.Instance.gameOver);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Pause()
    {
        if(UIController.Instance.pausePanel.activeSelf == false && UIController.Instance.gameOverPanel.activeSelf == false)
        {
            UIController.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioController.Instance.PlaySound(AudioController.Instance.pause);
        }
        else
        {
            UIController.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            AudioController.Instance.PlaySound(AudioController.Instance.unpause);

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    
}
