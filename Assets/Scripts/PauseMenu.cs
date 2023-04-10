using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    private bool optionsActive = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void OptionsMenu()
    {
        if (!optionsActive)
        {
            optionsMenuUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            optionsActive = true;
        } else
        {
            optionsMenuUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            optionsActive = false;
        }
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
