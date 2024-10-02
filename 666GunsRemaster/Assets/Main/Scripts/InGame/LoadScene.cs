using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    static string nextScene;

    [SerializeField] 
    Image progressBar;
    public static void LoadGameScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        //内风凭 角青
        StartCoroutine(LoadSceneProcess());
    }

    IEnumerator LoadSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float time = 0f;
        while (!op.isDone)
        {
            yield return null;

            //其捞农 肺爹 柳青
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                time += Time.unscaledDeltaTime;

                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, time);

                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
