using TMPro;
using UnityEngine;

public class MessageComponents : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text dateText;
    public TMP_Text MessageText => messageText;
    public TMP_Text DateText => dateText;
}
