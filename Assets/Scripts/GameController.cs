using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameController : MonoBehaviour
{
    public static Action<bool> OnGameStartedChanged;
    public static Action<bool> OnGamePausedChanged;
    [SerializeField] private GameObject pauseMenu;
    public bool GameStarted
    {
        get => gameStarted;
        set
        {
            gameStarted = value;
            OnGameStartedChanged?.Invoke(gameStarted);
        }
    }
    public bool GamePaused
    {
        get => gamePaused;
        set
        {
          gamePaused = value;
          OnGamePausedChanged?.Invoke(gamePaused);
        }
    }

    private bool gameStarted = false;
    private bool gamePaused = false;

    private void Awake()
    {
        OnGamePausedChanged += HandleGamePaused;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GamePaused = !GamePaused;
        }
    }

    private void HandleGamePaused(bool isPaused)
    {
        if(isPaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }
}
