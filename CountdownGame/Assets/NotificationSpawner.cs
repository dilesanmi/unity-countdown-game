using System.Collections;
using UnityEngine;

public class NotificationSpawner : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject UICanvas;

    public float swipeSpeed=0.5f;
    public int notificationDisplayLength = 3;
    [SerializeField] LeanTweenType easeType;


    public void SpawnNotification(string title, string desc)
    {
        StartCoroutine(NotificationCoroutine(title, desc));
    }
    private IEnumerator NotificationCoroutine(string title, string desc)
    {
        //TODO:ALSO DEPENDING ON HOW MANY NOTIFS THERE ARE, THEY SHOULDNT SPAWN ON TOP OF EACH OTHER!
        GameObject curNotifcation= Instantiate(notificationPrefab, UICanvas.transform);
        float initialX = curNotifcation.transform.position.x;
        
        NotificationPanel curNotificationPanel = curNotifcation.GetComponent<NotificationPanel>();
        curNotificationPanel.SetText(title, desc);

        LeanTween.moveX(curNotifcation, 5, swipeSpeed).setEase(easeType);
        yield return new WaitForSeconds(notificationDisplayLength);
        LeanTween.moveX(curNotifcation, initialX, 0.5f).setEase(easeType);
    }
}
