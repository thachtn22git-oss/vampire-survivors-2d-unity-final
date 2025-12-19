using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject instructionPanel;

    void Start()
    {
        // Trạng thái ban đầu
        mainMenuUI.SetActive(true); 
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void BackToMainMenu()
    {
        instructionPanel.SetActive(false);
        mainMenuUI.SetActive(true);
    }
}
