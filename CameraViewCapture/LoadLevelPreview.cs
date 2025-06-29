using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LoadLevelPreview : MonoBehaviour
{
    public Image previewImage; // Assign your UI Image here
    public string fileName = "SavedLevelPreview.png"; // Or pass it dynamically for each level

    void Start()
    {
        //LoadPreviewImage();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            LoadPreviewImage();
        }
    }

    public void LoadPreviewImage()
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(path))
        {
            byte[] imageData = File.ReadAllBytes(path);

            Texture2D texture = new Texture2D(2, 2); // Temporary size; will resize automatically
            texture.LoadImage(imageData); // Load the PNG data
            texture.Apply();

            Sprite loadedSprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            if (previewImage != null)
            {
                previewImage.sprite = loadedSprite;
                previewImage.preserveAspect = true;
            }

            Debug.Log("Loaded preview from: " + path);
        }
        else
        {
            Debug.LogWarning("Preview image not found at: " + path);
        }
    }
}
