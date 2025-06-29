using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CameraCapture : MonoBehaviour
{
    public Camera captureCamera; // Assign the camera in Inspector
    public RawImage previewImage; // Optional: to show on UI
    public Image savedSpriteImage; // Optional: UI Image to show sprite
    public int width = 512;
    public int height = 512;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            CaptureCameraView();
        }
    }

    public void CaptureCameraView()
    {
        // Set up RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 24);
        captureCamera.targetTexture = rt;
        Texture2D screenShot = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Render the camera
        captureCamera.Render();

        // Read pixels from the RenderTexture
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenShot.Apply();

        // Clean up
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Show in UI (optional)
        if (previewImage != null)
            previewImage.texture = screenShot;

        // Convert to Sprite if needed
        if (savedSpriteImage != null)
        {
            Sprite capturedSprite = Sprite.Create(
                screenShot,
                new Rect(0, 0, screenShot.width, screenShot.height),
                new Vector2(0.5f, 0.5f));
            savedSpriteImage.sprite = capturedSprite;
        }

        // Save to PNG (optional)
        byte[] bytes = screenShot.EncodeToPNG();
        File.WriteAllBytes(Application.persistentDataPath + "/SavedLevelPreview.png", bytes);

        Debug.Log("Saved preview to: " + Application.persistentDataPath + "/SavedLevelPreview.png");
    }
}
