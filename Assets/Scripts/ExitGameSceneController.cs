using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ExitGameSceneController : MonoBehaviour
{
    public float exitDelay = 4;
    public Text ExitGameText;

    private void Awake()
    {
        SetExitGameButtonText((int)exitDelay);
        Time.timeScale = 1; // ensure game is not paused anymore
    }
    private void Start()
    {
        StartCoroutine(ExitRoutine(exitDelay));
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            ExitGame();
        }
    }

    public IEnumerator ExitRoutine(float delay)
    {
        float remaining = delay;
        while(remaining > 0)
        {
            remaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            SetExitGameButtonText((int)remaining);
        }

        ExitGameText.text = $"Bye!";
        yield return new WaitForSeconds(1);
        ExitGame();
    }

    private void SetExitGameButtonText(int remainingTime)
    {
        ExitGameText.text = $"Exit Game ({remainingTime})";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
