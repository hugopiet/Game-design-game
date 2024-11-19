using UnityEngine;

public class GnistTriggerZone_level_0 : MonoBehaviour
{
    public Gnistfollow_level_0 gnist;
    public SceneController sceneController;

    private void Start()
    {
        Debug.Log("GnistTriggerZone started. Gnist reference: " + (gnist != null ? "Set" : "Not Set"));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger entered by: {other.gameObject.name}, Tag: {other.tag}");
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player tag detected!");
            if (gnist != null)
            {
                gnist.SetFollowState();
                Debug.Log("Follow state set on Gnist");
                sceneController.LoadNextLevel();
            }
            else
            {
                Debug.LogError("Gnist reference not set in GnistTriggerZone!");
            }
        }
    }
}