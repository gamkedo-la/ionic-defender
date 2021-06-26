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
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject uiHUD;
    [SerializeField] private GameIntro gameIntro;


    [Header("Debug")]
    [SerializeField] private bool skipMainMenu;

    private bool gameStarted = false;
    private bool gamePaused = false;

    private void Awake()
    {
        instance = this;
        //OnGamePausedChanged += HandleGamePaused;
        OnGameStartedChanged += HandleGameStarted;

        if(false == skipMainMenu)
        {
            gameIntro.SetupGameIntro();
            uiMenu.SetActive(true);
            mainMenu.SetActive(true);
            uiHUD.SetActive(false);

        }
        else
        {
            uiMenu.SetActive(false);
            uiHUD.SetActive(true);
        }

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

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            GamePaused = !GamePaused;
            Debug.Log("Toggling Game Paused");
        }
    }

    public void HandleGameStarted(bool gameStartedState)
    {
        if(gameStartedState)
        {
            uiMenu.SetActive(false);
            uiHUD.SetActive(true);
        }
    }

    private void HandleGamePaused(bool isPaused)
    {
        mainMenu.SetActive(false);
        
        Debug.Log($"Setting GamePaused [{isPaused}]");
        if(isPaused)
        {
            uiMenu.SetActive(true);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            uiMenu.SetActive(false);
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
