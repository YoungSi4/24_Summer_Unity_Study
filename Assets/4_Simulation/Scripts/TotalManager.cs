using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour
{
    // 토탈매니저 인스턴스를 생성
    public static TotalManager instance;

    public Image fadeScreen;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        // 배경음악 같은 거 조절 등등 다양한 용도로 사용.
        // 페이드인 페이드아웃 같은 효과도 가능
    }

    public void MoveScene(int id)
    {
        StartCoroutine(MoveSceneWithFade(id));
        // loadscene

    }

    public void MoveScene(string sceneName)
    {
        StartCoroutine(MoveSceneWithFade(sceneName));
    }

    private IEnumerator MoveSceneWithFade(int id)
    {
        yield return StartCoroutine(FadeScreen(true));
        SceneManager.LoadScene(1);
        yield return StartCoroutine(FadeScreen(false));
    }

    private IEnumerator MoveSceneWithFade(string sceneName)
    {
        yield return StartCoroutine(FadeScreen(true));
        //loadscene
        yield return StartCoroutine(FadeScreen(false));
    }

    private IEnumerator FadeScreen(bool fadeout) // true -> 검정색이 점점 되어가게
    {
        var fadeTimer = 0f;
        const float fadeDuration = 1f;

        var initialValue = fadeout ? 0f : 1f;
        var fadeDir = fadeout ? 1f : -1f;

        while (fadeTimer < fadeDuration)
        {
            yield return null; // 딱 1프레임 대기. Update문과 동일하게 작동
            fadeTimer += Time.deltaTime;

            var color = fadeScreen.color;
            initialValue += fadeDir * Time.deltaTime;
            color.a = initialValue;

            fadeScreen.color = color;
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit();

        #endif  
    }
    
}
