using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "MyData/PlayerData")]
public class PlayerMessageData : ScriptableObject
{
    [TextArea]public string acceptTask;
    [TextArea]public string rejectTask;
    [TextArea]public string tryBargain;
    [TextArea]public string rejectBargain;
    
}
