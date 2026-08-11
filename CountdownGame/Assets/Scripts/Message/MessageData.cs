using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Message
{
    [CreateAssetMenu(
        fileName = "New Message",
        menuName = "Message/Message Data"
    )]
    public class MessageData : ScriptableObject
    {
        [TextArea(2, 3)]
        public string messageText;

        public TextMessageType type;

        [TextArea(2, 3)]
        public string response;

        
    }
}