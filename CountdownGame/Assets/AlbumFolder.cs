using SoundControl;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlbumFolder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] private Image folderImage;
    [SerializeField] private AlbumTab albumTab;
    [SerializeField] private TMP_Text albumText;


    public AlbumCategory category;


    public void SetFolder(AlbumCategory chosencategory)
    {
        GameObject AlbumTab = GameObject.FindGameObjectWithTag("AlbumTab");
        albumTab = AlbumTab.GetComponent<AlbumTab>();

        albumText.text = chosencategory.ToString();

        category = chosencategory;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
            return;

        GameObject droppedObject = eventData.pointerDrag;

        if (!droppedObject.CompareTag("Photo"))
        {
            Debug.Log("Carry on mate");
            return;
        }

        Debug.Log("Photo detected!!!");
        DraggablePhoto photoUI = droppedObject.GetComponent<DraggablePhoto>();

        if (photoUI != null && photoUI.Photo.category == category)
        {
            SoundEffectManager.Play("sfx_correct");
            Debug.Log("Good Job!");
            albumTab.photosLeft--;
            Destroy(droppedObject);
        }
        else
        {
            SoundEffectManager.Play("sfx_wrong");
            albumTab.wrongPlaces++;
            albumTab.photosLeft--;
            Destroy(droppedObject);
            Debug.Log("Wrong folder, baka!");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Photo"))
        {
            folderImage.color = Color.blue;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        folderImage.color = Color.gray;//This would be white when I have actual assets
    }
}    
