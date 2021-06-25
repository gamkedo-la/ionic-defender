using System.Collections;
using UnityEngine;

public class GameIntro : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Camera cameraReference;
    [SerializeField] Transform cameraTransform;
    [Header("Game Intro Positions")]
    [SerializeField] Transform introCameraPosition;
    [SerializeField] float introOrthographicSize;

    [Header("Game Started Positions")]
    [SerializeField] Transform gameCameraPosition;
    [SerializeField] float gameOrthographicSize;


    // switching from non orthographic to orthographic was not smooth
    // hence commenting this code out
    //[SerializeField] bool introIsOrthographic;
    //[SerializeField] bool gameIsOrthographic;

    public void SetupGameIntro()
    {
        this.gameObject.SetActive(true);
        // switching from non orthographic to orthographic was not smooth
        // hence commenting this code out
        //cameraReference.orthographic = introIsOrthographic;
        cameraTransform.position = introCameraPosition.position;
        cameraTransform.rotation = introCameraPosition.rotation;
        cameraReference.orthographicSize = introOrthographicSize;
        GameController.OnGameStartedChanged += HandleGameStarted;
    }

    private void HandleGameStarted(bool gameStartedState)
    {
        if(gameStartedState)
        {
            StartCoroutine(MoveCamera(0.25f, 1f));
        }
    }

    private IEnumerator MoveCamera(float startDelay, float duration)
    {
        yield return new WaitForSeconds(startDelay);

        float remaining = duration;
        var startAt = introCameraPosition.position;
        var endAt = gameCameraPosition.position;

        var rotStartAt = introCameraPosition.rotation;
        var rotEndAt = gameCameraPosition.rotation;

        var sizeStartAt = introOrthographicSize;
        var sizeEndAt = gameOrthographicSize;

        // switching from non orthographic to orthographic was not smooth
        // hence commenting this code out
        //cameraReference.orthographic = gameIsOrthographic;

        while(remaining > 0)
        {
            yield return new WaitForEndOfFrame();
            remaining -= Time.deltaTime;

            cameraTransform.position = Vector3.Lerp(startAt, endAt, 1 - remaining);
            cameraTransform.rotation = Quaternion.Lerp(rotStartAt, rotEndAt, 1 - remaining);
            cameraReference.orthographicSize = Mathf.Lerp(sizeStartAt, sizeEndAt, 1 - remaining);
        }

    }
}
