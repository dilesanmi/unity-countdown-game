using SoundControl;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;

    public void SetText(string title, string desc)
    {
        titleText.text = title;
        descText.text = desc;
    }

}
