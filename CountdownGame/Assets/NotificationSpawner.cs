using System.Collections;
using UnityEngine;

namespace Notification
{
    public class NotificationSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject notificationPrefab;
        [SerializeField] private GameObject UICanvas;

        public float swipeSpeed = 0.5f;
        public int notificationDisplayLength = 3;
        [SerializeField] LeanTweenType easeType;

        public static NotificationSpawner Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Spawn(string title, string desc)
        {
            StartCoroutine(NotificationCoroutine(title, desc));
        }

        public static void SpawnNotification(string title, string desc)
        {
            if (Instance == null)
            {
                Debug.LogWarning("NotificationSpawner not initialized.");
                return;
            }

            Instance.Spawn(title, desc);
        }

        private IEnumerator NotificationCoroutine(string title, string desc)
        {
            //TODO:ALSO DEPENDING ON HOW MANY NOTIFS THERE ARE, THEY SHOULDNT SPAWN ON TOP OF EACH OTHER!
            GameObject curNotifcation = Instantiate(notificationPrefab, UICanvas.transform);
            float initialX = curNotifcation.transform.position.x;

            NotificationPanel curNotificationPanel = curNotifcation.GetComponent<NotificationPanel>();
            curNotificationPanel.SetText(title, desc);

            LeanTween.moveX(curNotifcation, 5, swipeSpeed).setEase(easeType);
            yield return new WaitForSeconds(notificationDisplayLength);
            LeanTween.moveX(curNotifcation, initialX, 0.5f).setEase(easeType)
                .setOnComplete(() =>
                {
                    DestroyNotification(curNotifcation);
                });
        }

        void DestroyNotification(GameObject obj)
        {
            Destroy(obj);
        }
    }
}
