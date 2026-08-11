
using UnityEngine;

namespace Message
{
    [System.Serializable]
    public class MessageLine
    {
        [TextArea(1, 3)] // MinLines, MaxLines
        public string text;

        public TextMessageType tyype;
        
    }
}