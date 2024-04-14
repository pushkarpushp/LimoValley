using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ChatData", menuName = "ScriptableObject/ChatData")]
public class ChatData : ScriptableObject
{
    public List<ChatMessages> chatMessages=new List<ChatMessages> ();
}
[System.Serializable]
public class ChatMessages
{
    public string
        Name;
    public string
        Messages;
   
}
