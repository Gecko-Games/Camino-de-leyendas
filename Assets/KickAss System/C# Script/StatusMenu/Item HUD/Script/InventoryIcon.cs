using UnityEngine;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour
{
    Image image;

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
    }

    public void ChangeColor(Color col)
    {
        image.color = col;
    }
}
