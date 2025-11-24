using UnityEngine;

public class RoundEnterSenser : MonoBehaviour
{
    [SerializeField]
    private int roundIndex;

    [SerializeField]
    private GameObject[] roundAbars;

    private void Start()
    {
        foreach (GameObject abar in roundAbars)
        {
            abar.SetActive(false);
        }
    }

    public void SetRoundAbarsOn(bool isActive)
    {
        foreach (GameObject abar in roundAbars)
        {
            abar.SetActive(isActive);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //StageController stageController = FindObjectOfType<StageController>();
            // if (stageController != null)
            // {
            //     stageController.EnterRound(roundIndex);
            //     SetRoundAbarsOn(true);
            //     gameObject.GetComponent<Collider>().enabled = false;
            // }
            // else
            // {
            //     Debug.LogWarning("StageController not found in the scene.");
            // }
        }
    }
}