using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static Action<bool> OnGameStartedChanged;
    public static Action<bool> OnGamePausedChanged;
    public static bool GameStarted
    {
        get => instance.gameStarted;
        set
        {
            instance.gameStarted = value;
            OnGameStartedChanged?.Invoke(instance.gameStarted);
        }
    }
    public static bool GamePaused
    {
        get => instance.gamePaused;
        set
        {
          instance.gamePaused = value;
          OnGamePausedChanged?.Invoke(instance.gamePaused);
        }
    }

    [Header("Configuration")]
    [SerializeField] private GameObject pauseMenu;

    [Header("Debug")]
    [SerializeField] private bool skipMainMenu;


    private bool gameStarted = false;
    private bool gamePaused = false;

    private void Awake()
    {
        instance = this;
        OnGamePausedChanged += HandleGamePaused;
    }

    private void Start()
    {
        if(skipMainMenu)
        {
            GameStarted = true;
        }
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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            GameStarted = !GameStarted;
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
