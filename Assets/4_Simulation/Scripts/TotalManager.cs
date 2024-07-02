using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TotalManager : MonoBehaviour
{
    // ��Ż�Ŵ��� �ν��Ͻ��� ����
    public static TotalManager instance;

    public Image fadeScreen;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        // ������� ���� �� ���� ��� �پ��� �뵵�� ���.
        // ���̵��� ���̵�ƿ� ���� ȿ���� ����
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

    private IEnumerator FadeScreen(bool fadeout) // true -> �������� ���� �Ǿ��
    {
        var fadeTimer = 0f;
        const float fadeDuration = 1f;

        var initialValue = fadeout ? 0f : 1f;
        var fadeDir = fadeout ? 1f : -1f;

        while (fadeTimer < fadeDuration)
        {
            yield return null; // �� 1������ ���. Update���� �����ϰ� �۵�
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
