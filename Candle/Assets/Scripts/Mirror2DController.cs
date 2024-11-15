using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Mirror2DController : MonoBehaviour
{
    [Header("Required References")]
    public Camera mainCamera;
    public Camera mirrorCamera;
    public Transform player;

    [Header("Mirror Settings")]
    [Range(256, 2048)]
    public int textureResolution = 1024;
    [Range(0f, 1f)]
    public float reflectionQuality = 1f;
    public bool useAntiAliasing = true;
    public LayerMask reflectionMask = -1;

    [Header("Visual Settings")]
    [Range(0f, 1f)]
    public float reflectionIntensity = 1f;
    public Color tintColor = Color.white;
    
    private RenderTexture mirrorTexture;
    private Material mirrorMaterial;
    private int originalPlayerLayer;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        InitializeMirror();
    }

    void InitializeMirror()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -1;
        originalPlayerLayer = player.gameObject.layer;

        if (mirrorCamera)
        {
            int width = Mathf.RoundToInt(textureResolution * reflectionQuality);
            int height = Mathf.RoundToInt(textureResolution * reflectionQuality);
            mirrorTexture = new RenderTexture(width, height, 16);
            mirrorTexture.antiAliasing = useAntiAliasing ? 4 : 1;
            mirrorTexture.name = "MirrorReflection" + GetInstanceID();
            
            mirrorCamera.targetTexture = mirrorTexture;
            
            mirrorMaterial = new Material(Shader.Find("Unlit/Texture"));
            mirrorMaterial.mainTexture = mirrorTexture;
            mirrorMaterial.SetFloat("_Mirrored", 1f); // Enable mirroring in shader if available
            
            spriteRenderer.material = mirrorMaterial;
            SetupMirrorCamera();
        }
        else
        {
            Debug.LogError("Mirror Camera not assigned to Mirror Controller!");
        }
    }

    void SetupMirrorCamera()
{
    if (!LayerExists("Reflection"))
    {
        Debug.LogWarning("Please create a layer named 'Reflection' in your project's Layer settings!");
    }

    mirrorCamera.cullingMask = reflectionMask; // Use the reflectionMask defined in the inspector
    mirrorCamera.clearFlags = CameraClearFlags.SolidColor;
    mirrorCamera.backgroundColor = new Color(0, 0, 0, 0);
    mirrorCamera.orthographic = mainCamera.orthographic;
    mirrorCamera.orthographicSize = mainCamera.orthographicSize;

    // Flip the projection matrix for a mirrored effect
    mirrorCamera.projectionMatrix = mainCamera.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
}

void UpdateMirrorCamera()
{
    if (!mirrorCamera || !mainCamera || !player)
        return;

    // Temporarily move player to reflection layer
    player.gameObject.layer = LayerMask.NameToLayer("Reflection");

    // Calculate mirrored position
    Vector3 mirroredPosition = player.position;
    mirroredPosition.x = 2 * transform.position.x - player.position.x;

    // Position the mirror camera
    mirrorCamera.transform.position = mirroredPosition;
    mirrorCamera.orthographicSize = mainCamera.orthographicSize;
    mirrorCamera.transform.rotation = mainCamera.transform.rotation;

    // Apply visual settings
    mirrorMaterial.color = tintColor * reflectionIntensity;

    // Restore player's original layer
    player.gameObject.layer = originalPlayerLayer;
}


    bool LayerExists(string layerName)
    {
        return LayerMask.NameToLayer(layerName) != -1;
    }

    void LateUpdate()
    {
        if (!mirrorCamera || !mainCamera || !player)
            return;

        UpdateMirrorCamera();
    }



    void OnDisable()
    {
        if (mirrorTexture)
        {
            mirrorTexture.Release();
            Destroy(mirrorTexture);
        }
        if (mirrorMaterial)
        {
            Destroy(mirrorMaterial);
        }
    }
}