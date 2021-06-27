using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Action<bool> OnGameStartedChanged;
    public Action<bool> OnGamePausedChanged;
    public Action OnPlayerDie;
    public static bool GameStarted
    {
        get => Instance.gameStarted;
        set
        {
            Instance.gameStarted = value;
            Instance.OnGameStartedChanged?.Invoke(Instance.gameStarted);
        }
    }
    public static bool GamePaused
    {
        get => Instance.gamePaused;
        set
        {
          Instance.gamePaused = value;
          Instance.OnGamePausedChanged?.Invoke(Instance.gamePaused);
        }
    }

    [Header("Configuration")]
    [SerializeField] private GameObject uiMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject uiHUD;
    [SerializeField] private GameIntro gameIntro;


    [Header("Debug")]
    [SerializeField] private bool skipMainMenu;

    private bool gameStarted = false;
    private bool gamePaused = false;

    private void Awake()
    {
        Instance = this;
        
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
    }

    private void Start()
    {
        OnGamePausedChanged += HandleGamePaused;
        OnGameStartedChanged += HandleGameStarted;
        OnPlayerDie += GameOver;

        if(Instance == null)
        {
            Instance = this;
        }

        if(skipMainMenu)
        {
            GameStarted = true;
        }
    }

    public void Restart()
    {
        gameStarted = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        SoundFXManager.PlayOneShot(SoundFxKey.GameOver);
        uiMenu.SetActive(true);
        gameOverMenu.SetActive(true);
        mainMenu.SetActive(false);
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
