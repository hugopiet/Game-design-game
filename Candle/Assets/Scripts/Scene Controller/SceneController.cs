using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneController : MonoBehaviour
{
    // public string sceneName; // Optional: If you want to specify the scene name manually
    public Animator transition; // Animator for the transition
    public float transitionTime = 10f; // Time for the transition effect
    public float transistionTime_withoutGnist = 5f;

    // public TextMeshProUGUI buttonText;  // Reference to the button's text
    // Public method to load the next level
    public void LoadNextLevel(bool gnist)
    {
        Debug.Log("loadNextLevel");
        StartCoroutine(LoadLevel((SceneManager.GetActiveScene().buildIndex + 1), gnist));
    }

    public void LoadSpecificLevel(int levelIndex) {
        bool gnist = false;

        Debug.Log("Loading level with index: " + levelIndex);
        if (levelIndex == 10) {
            levelIndex = 2;
        }
        StartCoroutine(LoadLevel(levelIndex, gnist)); // Load the level passed from the button click
    }

    // Coroutine to handle the transition and scene loading
    private IEnumerator LoadLevel(int levelIndex, bool gnist)

    {
         if (InfoBubbleManager.Instance != null)
        {
            InfoBubbleManager.Instance.HideInfoBubble(true); // Immediate hide
        }
        // Play the transition animation
        if (transition != null)
        {
            Debug.Log("start trigger");
            transition.SetTrigger("start");
        }
        else {
            Debug.Log("no transition found");
        }

        // Wait for the transition to complete
        if (gnist) {
        yield return new WaitForSeconds(transitionTime);
        gnist = false;
        }
        else {
             yield return new WaitForSeconds(transistionTime_withoutGnist);

        }

        // Load the next scene
        SceneManager.LoadScene(levelIndex);
    }
}
