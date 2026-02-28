using UnityEngine;

[CreateAssetMenu(fileName = "MessageData", menuName = "MyData/MessageData")]
public class MessageData : ScriptableObject
{
    [TextArea] public string preview;
    [TextArea] public string introduction;
    [TextArea] public string acceptTask;
    [TextArea] public string rejectTask;
    [TextArea] public string bargainPositive;
    [TextArea] public string bargainNegative;
    [TextArea] public string dealOverPositive;
    [TextArea] public string dealOverNegative;
}