using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    private TimeManager timeManager;
    void Start()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeManager.hours > 15 && timeManager.hours < 18) 
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.ReceiveQuest(quest);
        }

    }
}
