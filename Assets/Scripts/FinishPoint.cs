using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    public EndLevelUI endLevelUI;

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        // Call this method to show the UI
        endLevelUI.ShowEndLevelOptions();
    }
}
}
