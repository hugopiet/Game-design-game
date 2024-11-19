using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicButton : MonoBehaviour
{
    private int levelIndex; // Stores the level index passed by the InfoBubbleManager
    public SceneController sceneController; // Reference to the 

    // This method is called by InfoBubbleManager to set the level index
    public void SetLevelIndex(int index)
    {
        levelIndex = index;
    }

    // This method is assigned to the button's OnClick in InfoBubbleManager
    public void LoadSpecificScene()
    {
        Debug.Log($"Loading level: {levelIndex}");
        sceneController.LoadSpecificLevel(levelIndex);
    }
}
