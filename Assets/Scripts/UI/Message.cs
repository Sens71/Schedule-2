using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour, IUIPanel
{
    [SerializeField] private MessageComponents messageSent;
    [SerializeField] private MessageComponents messageReceived;
    [SerializeField] private TMP_Text phoneNumber;
    [SerializeField] private GameObject messagePanel;
    [SerializeField] private Transform content;
    [SerializeField] private GameObject messagePreviewPanel;
    [SerializeField] private MessageOption optionPrefab;
    [SerializeField] private BargainMenu bargainMenuPrefab;

    private bool _isOpen;
    private MessageOption _option;
    private Order _order;
    private BargainMenu _bargainMenu;
    private TimeManager _timeManager;
    private QuestFactory _questFactory;

    private void Awake()
    {
        _timeManager = FindAnyObjectByType<TimeManager>();
        _questFactory = FindAnyObjectByType<QuestFactory>();
    }

    public void Open() { }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        for (int i = content.childCount - 1; i >= 0; i--)
            Destroy(content.GetChild(i).gameObject);
        messagePanel.SetActive(false);
        IUIPanel.Notify(this, false, false);
    }

    public void OpenMessage(Order order)
    {
        if (_isOpen) Close();
        _isOpen = true;
        _order = order;
        messagePanel.SetActive(true);
        IUIPanel.Notify(this, true, true);

        switch (_order.State)
        {
            case MessageState.Intruduction:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                _option = Instantiate(optionPrefab, content);
                LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
                Canvas.ForceUpdateCanvases();
                _option.accept.onClick.AddListener(AcceptTask);
                _option.reject.onClick.AddListener(RejectTask);
                _option.bargain.onClick.AddListener(TryBargain);
                break;
            case MessageState.AcceptTask:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.PlayerMessageData.acceptTask, false, order.dateResponded);
                GenerateMessage(order.MessageData.acceptTask, true, order.dateResponded);
                break;
            case MessageState.RejectTask:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.PlayerMessageData.rejectTask, false, order.dateResponded);
                GenerateMessage(order.MessageData.rejectTask, true, order.dateResponded);
                break;
            case MessageState.BargainPositive:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.MessageData.bargainPositive, true, order.dateResponded);
                break;
            case MessageState.BargainNegative:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.PlayerMessageData.rejectBargain, false, order.dateResponded);
                GenerateMessage(order.MessageData.bargainNegative, true, order.dateResponded);
                break;
            case MessageState.DealOverPositiveBargain:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.MessageData.bargainPositive, true, order.dateResponded);
                GenerateMessage(order.MessageData.dealOverPositive, true, order.dateResponded);
                break;
            case MessageState.DealOverNegativeBaragain:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.MessageData.bargainPositive, true, order.dateResponded);
                GenerateMessage(order.MessageData.dealOverNegative, true, order.dateFinished);
                break;
            case MessageState.DealOverPositiveNoBargain:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.MessageData.acceptTask, true, order.dateResponded);
                GenerateMessage(order.MessageData.dealOverPositive, true, order.dateFinished);
                break;
            case MessageState.DealOverNegativeNoBargain:
                GenerateMessage(order.MessageData.introduction, true, order.dateTaken);
                GenerateMessage(order.MessageData.acceptTask, true, order.dateResponded);
                GenerateMessage(order.MessageData.dealOverNegative, true, order.dateFinished);
                break;
        }
    }

    private void GenerateMessage(string message, bool receiver, ClockTime time)
    {
        var instance = Instantiate(receiver ? messageReceived : messageSent, content);
        instance.MessageText.text = message;
        instance.DateText.text = time.ToString();
    }

    private void AcceptTask()
    {
        _order.dateResponded = _timeManager.GetCurrentTime();
        Destroy(_option.gameObject);
        _option = null;
        _order.State = MessageState.AcceptTask;
        GenerateMessage(_order.PlayerMessageData.acceptTask, false, _order.dateResponded);
        GenerateMessage(_order.MessageData.acceptTask, true, _order.dateResponded);
        _questFactory.GenerateQuest(_order);
    }

    private void RejectTask()
    {
        _order.dateResponded = _timeManager.GetCurrentTime();
        Destroy(_option.gameObject);
        _option = null;
        _order.State = MessageState.RejectTask;
        GenerateMessage(_order.PlayerMessageData.rejectTask, false, _order.dateResponded);
        GenerateMessage(_order.MessageData.rejectTask, true, _order.dateResponded);
    }

    private void TryBargain()
    {
        Destroy(_option.gameObject);
        _option = null;
        _bargainMenu = Instantiate(bargainMenuPrefab, content);
        _bargainMenu.SetData(_order, MakeDealCallback, CancelBargainCallback);
    }

    private void MakeDealCallback()
    {
        Destroy(_bargainMenu.gameObject);
        _bargainMenu = null;
        if (_order.OfferedPrice <= _order.MaxBargain)
        {
            _order.State = MessageState.BargainPositive;
            _order.MessageData.bargainPositive = _order.MessageData.bargainPositive.Replace("{price}", _order.OfferedPrice.ToString());
            GenerateMessage(_order.MessageData.bargainPositive, true, _order.dateResponded);
            _questFactory.GenerateQuest(_order);
        }
        else
        {
            _order.State = MessageState.BargainNegative;
            _order.MessageData.bargainNegative = _order.MessageData.bargainNegative.Replace("{price}", _order.OfferedPrice.ToString());
            GenerateMessage(_order.PlayerMessageData.rejectBargain, false, _order.dateResponded);
            GenerateMessage(_order.MessageData.bargainNegative, true, _order.dateResponded);
        }
    }

    private void CancelBargainCallback()
    {
        Destroy(_bargainMenu.gameObject);
        _bargainMenu = null;
        _option = Instantiate(optionPrefab, content);
        _option.accept.onClick.AddListener(AcceptTask);
        _option.reject.onClick.AddListener(RejectTask);
        _option.bargain.onClick.AddListener(TryBargain);
    }
}

public enum MessageState
{
    Intruduction,
    AcceptTask,
    RejectTask,
    DealOverPositiveNoBargain,
    DealOverNegativeNoBargain,
    DealOverPositiveBargain,
    DealOverNegativeBaragain,
    BargainPositive,
    BargainNegative
}
