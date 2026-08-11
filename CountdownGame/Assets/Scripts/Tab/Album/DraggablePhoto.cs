using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DraggablePhoto : MonoBehaviour
{
    public PhotoData Photo;
    [SerializeField] private TMP_Text photoText;
    
    public void SetPhoto(PhotoData photo)
    {
        Photo = photo;
        photoText.text = photo.name;
    }
}