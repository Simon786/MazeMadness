using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelUI : MonoBehaviour
{
    public GameObject endLevelCanvas;
    public Text levelCounterText;

    void Start()
    {
        endLevelCanvas.SetActive(false);
    }

    public void ShowEndLevelOptions()
    {
        endLevelCanvas.SetActive(true);
        PauseGame();
        UpdateLevelCounter();
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    private void UpdateLevelCounter()
    {
        if (levelCounterText != null)
        {
            levelCounterText.text = "Level: " + LevelManager.Instance.CurrentLevel.ToString();
        }
    }

    public void NextLevel()
    {
        LevelManager.Instance.NextLevel(); // Increment the level and load the next one
        ResumeGame();
        endLevelCanvas.SetActive(false);
        GameState.IncreaseMazeSize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {   
        GameState.ResetMazeSize();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        ResumeGame();
        endLevelCanvas.SetActive(false);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }
}
