using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuPanel; // Drag your MenuPanel here in the Inspector.

    public void ToggleMenu()
    {
        // Toggle the panel's active state
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
