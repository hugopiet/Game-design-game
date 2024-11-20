using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseManager : MonoBehaviour
{
    
    [SerializeField] GameObject pauseMenu; // Start is called before the first frame update
    [SerializeField] SceneController sceneController;
    public void Pause ()
    {
        pauseMenu.SetActive(true);
    }

    // Update is called once per frame
    public void Home ()
    {
        sceneController.LoadSpecificLevel(1);
        
    }

    public void Resume() {
         pauseMenu.SetActive(false);

    }

    public void Restart () {
        sceneController.LoadSpecificLevel(SceneManager.GetActiveScene().buildIndex);
    }
}
