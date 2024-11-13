using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePosition : MonoBehaviour
{
    public List<GameObject> posList = new List<GameObject>();
    public Text timer;

    private int currentPosIndex = 0;

    void Awake()
    {

    }

    void Start()
    {
        //timer = GetComponent<Text>();

        // 첫 번째 위치만 활성화하고 나머지는 비활성화
        //posList[0].SetActive(true);

        foreach (GameObject obj in posList)
        {
            Debug.Log($"{obj.name} 초기 상태: {obj.activeSelf}");
        }

        for (int i = 1; i < posList.Count; i++)
        {
            posList[i].SetActive(false); // 나머지 위치 비활성화
        }
        currentPosIndex = 0;
    }

    void Update()
    {
        float timeRemaining = GameManager.instance.GetTimer() % 60f;

        if (timeRemaining <= 6 && timeRemaining > 0)
        {
            SetPosition((int)(6 - timeRemaining));
        }
        else if (timeRemaining == 0)
        {
            DeactivateAllPositions();
        }
    }

    private void SetPosition(int index)
    {
        if (index != currentPosIndex && index < posList.Count)
        {
            posList[currentPosIndex].SetActive(false);
            posList[index].SetActive(true);
            currentPosIndex = index;
        }
    }

    private void DeactivateAllPositions()
    {
        foreach (GameObject pos in posList)
        {
            pos.SetActive(false);
        }
        currentPosIndex = 0;
    }
}
