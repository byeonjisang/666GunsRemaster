using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeListener : Singleton<SceneChangeListener>
{
    protected override bool IsPersistent => true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"씬 전환 완료됨: {scene.name} ({mode})");
        // 씬 전환 후 초기화할 작업 수행


        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogWarning("씬 전환 후 Player 오브젝트가 존재하지 않습니다.");
        }
        else
        {
            Debug.Log("씬 전환 후 Player 오브젝트가 존재합니다.");

            // PlayerManager 초기화
            PlayerManager.Instance.InitializePlayer(playerObject);
            //WeaponManager.Instance.Initialized();
        }
    }
}