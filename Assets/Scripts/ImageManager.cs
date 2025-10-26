using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public GameObject imagePrefab; // Assign your prefab in the Inspector
    public Transform parentTransform; // The parent where the prefab will be instantiated

    void Start()
    {
        // Instantiate the prefab
        GameObject newImage = Instantiate(imagePrefab, parentTransform);

        // Optionally, set the image source if needed
        Image imgComponent = newImage.GetComponent<Image>();
        if (imgComponent != null)
        {
            imgComponent.sprite = Resources.Load<Sprite>("Assets/Art/Sprite-001.ase"); // Adjust the path as necessary
        }
    }
}

