using System.Collections.Generic;
using Message;
using Relationship;
using SoundControl;
using TMPro;
using UnityEngine;


/// <summary>
/// Manages anything related to the message UI and typing messages to your partner
/// </summary>
public class MessageManager : MonoBehaviour
{
    [Header("Management")]
    [SerializeField] private RelationshipManager relationshipManager;
    [SerializeField] private AudioManager audioManager;

    [Header("UI")]
    [SerializeField] private GameObject messageUI;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text greyedOutReply;
    [SerializeField] private TMP_Text messageReply;
    [SerializeField] private GameObject sendButton;
    [SerializeField] private GameObject notificationIcon;
    List<MessageData> messageHistory;

    [Header("Variables")]
    public bool partnerWaiting;
    public bool isOpen;
    bool replyFinished;
    private static int _curCharIndex;
    private static string _currentReply = "";
    public MessageData currentMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMessage = null;

        messageHistory = new List<MessageData>();
        messageReply.text = "";
        greyedOutReply.text = "";
        messageText.text = "";

        messageUI.SetActive(false);
        sendButton.SetActive(false);
        notificationIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (partnerWaiting && !isOpen)
        {
            notificationIcon.SetActive(true);
        }
        else
        {
            notificationIcon.SetActive(false);
        }

        if (isOpen)
        {

            if (partnerWaiting )
            {
                foreach (char c in Input.inputString)
                {
                    TypeReply(c);
                }
            }

            if (replyFinished)
            {
                Debug.Log("Send already!");
                sendButton.SetActive(true);
            }

            if (replyFinished && Input.GetKeyDown(KeyCode.Return))
            {
                SendReply();
            }

        }

        if (currentMessage != null)
        {
            UpdateCurrentMessage(currentMessage);
        }
    }

    public void TogglePhone()
    {   
        if (!isOpen)
        {
            Debug.Log("Message open!");
            messageUI.SetActive(true);
            isOpen = true;
            if (!partnerWaiting)
            {
                relationshipManager.StartLoseAffection(); 
            }
            return;
        }

        if (isOpen)
        {
            Debug.Log("Message closed!");
            messageUI.SetActive(false);
            isOpen = false;
            if (!partnerWaiting)
            {
                relationshipManager.StopLoseAffection();
            }
            else
            {
                relationshipManager.StartLoseAffection();
            }
        }
    }
    
    public void GetMessage(MessageData message)
    {
        if (currentMessage!=null)
        {
            return;
        }
        if (message.type != TextMessageType.Unrelated)
        {
            currentMessage = message;
            _currentReply = message.response;
            relationshipManager.StartLoseAffection();
            SoundEffectManager.Play("sfx_notification");

            UpdateReply("");
            partnerWaiting = true;
        }
    }

    private void TypeReply(char c)
    {
        if (replyFinished)
        {
            return;
        }
        
        //Type reply for each correct char typed
        if (c.Equals(_currentReply[_curCharIndex] )){

            _curCharIndex++;

            if (_curCharIndex >= _currentReply.Length)
            {
                _curCharIndex = _currentReply.Length;
            }

            UpdateReply(_currentReply.Substring(0, _curCharIndex));
        }

        //Check if reply done
        if (_curCharIndex >= currentMessage.response.Length)
        {
            _curCharIndex = currentMessage.response.Length;

            replyFinished = true;
            TurnReplyGreen();
            return;
        }



    }

    private void ResetMessages()
    {
        replyFinished = false;
        partnerWaiting = false;
        _curCharIndex = 0;
        sendButton.SetActive(false);
        Ungreen();
    }

    public void UpdateCurrentMessage(MessageData messageData)
    {
        messageHistory.Add(messageData);

        greyedOutReply.text = messageData.response;
        messageText.text = messageData.messageText;
        //Debug.Log(messageData.messageText);
    }

    public void UpdateReply(string reply)
    {

        messageReply.text = reply;
        Debug.Log(reply);
    }

    public void SendReply()
    {
        Debug.Log("Sent!");
        SoundEffectManager.Play("sfx_send");
        currentMessage = null;

        relationshipManager.StopLoseAffection();
        ResetMessages();

        TogglePhone();
    }

    public void TurnReplyGreen()
    {
        //He's green for an amazing reason
        messageReply.color = Color.green;
    }

    public void Ungreen()
    {
        //He's not green for a terrible reason
        messageReply.color = Color.black;
    }
}
