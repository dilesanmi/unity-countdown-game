using TMPro;
using UnityEngine;

public class NotificationPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descText;

    public void SetText(string title, string desc)
    {
        titleText.text = title;
        descText.text = desc;
    }

    public void CloseNotification()
    {
        Debug.Log("Yeah ik im lowk in the way mb hb");
        Destroy(gameObject);
    }
}

