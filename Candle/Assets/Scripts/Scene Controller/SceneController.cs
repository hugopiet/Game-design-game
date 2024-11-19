using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneController : MonoBehaviour
{
    // public string sceneName; // Optional: If you want to specify the scene name manually
    public Animator transition; // Animator for the transition
    public float transitionTime = 10f; // Time for the transition effect

    // Public method to load the next level
    public void LoadNextLevel()
    {
        Debug.Log("loadNextLevel");
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    // Coroutine to handle the transition and scene loading
    private IEnumerator LoadLevel(int levelIndex)
    {
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
        yield return new WaitForSeconds(transitionTime);

        // Load the next scene
        SceneManager.LoadScene(levelIndex);
    }
}
