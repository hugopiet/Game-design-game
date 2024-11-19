using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBubbleManager : MonoBehaviour
{
    public static InfoBubbleManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject infoBubblePanel;
    public TextMeshProUGUI infoText;
    public Image dimOverlay;
    public Button PlayLevelButton;  // Reference to the button
    public TextMeshProUGUI buttonText;  // Reference to the button's text
    private DynamicButton dynamicButton; // Reference to the button's script

    
    [Header("Animation Settings")]
    public float fadeInDuration = 0.3f;
    public float fadeOutDuration = 0.2f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure UI is hidden at start
        HideInfoBubble(true);

        dynamicButton = PlayLevelButton.GetComponent<DynamicButton>();
    }

    public void ShowInfoBubble(string text, int levelIndex)
    {
        infoBubblePanel.SetActive(true);
        infoText.text = text;
        PlayLevelButton.gameObject.SetActive(true); // Show the button
        buttonText.text = "Play Level " + levelIndex; // Set the button text dynamically
        
        if (levelIndex == 1 || levelIndex == 10)
        {
            dynamicButton.SetLevelIndex(levelIndex);
        }


        // Assign the LoadSpecificScene method dynamically to the OnClick listener
        PlayLevelButton.onClick.RemoveAllListeners();
        PlayLevelButton.onClick.AddListener(dynamicButton.LoadSpecificScene);


        StartCoroutine(FadeIn());

    }

    public void HideInfoBubble(bool immediate = false)
    {
        if (immediate)
        {
            infoBubblePanel.SetActive(false);
            dimOverlay.color = new Color(0, 0, 0, 0);
        }
        else
        {
            StartCoroutine(FadeOut());
        }
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        Color overlayColor = dimOverlay.color;
        overlayColor.a = 0;
        
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 0.7f, elapsedTime / fadeInDuration);
            dimOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        Color startColor = dimOverlay.color;
        
        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startColor.a, 0, elapsedTime / fadeOutDuration);
            dimOverlay.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        
        infoBubblePanel.SetActive(false);
    }
}