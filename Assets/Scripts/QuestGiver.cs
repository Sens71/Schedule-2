using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    private TimeManager timeManager;
    void Awake()
    {
        timeManager = FindAnyObjectByType<TimeManager>();
        TimeCount();
    }

    // Update is called once per frame
    private async void TimeCount()
    {
        while (true)
        {
            await Awaitable.NextFrameAsync();
            if (timeManager.hours >= 1 && timeManager.hours < 3)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
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
