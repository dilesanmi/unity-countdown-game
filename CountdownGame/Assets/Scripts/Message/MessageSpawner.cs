using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Message
{
    public class MessageSpawner : MonoBehaviour
    {
        [SerializeField] private MessageManager messageManager;


        [Header("Timing")]
        [SerializeField] private float minDelay = 5f;
        [SerializeField] private float maxDelay = 10f;

        [Header("Messages")]
        [SerializeField] private List<MessageData> messages;

        private void Start()
        {
            StartCoroutine(MessageRoutine());
        }

        IEnumerator MessageRoutine()
        {
            while (true)
            {
                if (messageManager.currentMessage==null)
                {

                    yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
                    Debug.Log("Message time!!!");

                    MessageData randomMessage =
                        messages[Random.Range(0, messages.Count)];

                    messageManager.GetMessage(randomMessage);
                }
            }
        }
    }
}
