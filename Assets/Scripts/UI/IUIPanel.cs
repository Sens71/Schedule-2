using System;

public interface IUIPanel
{
    static event Action<IUIPanel, bool, bool> OnUIStateChanged;

    static void Notify(IUIPanel panel, bool isOpen, bool closePrevious)
        => OnUIStateChanged?.Invoke(panel, isOpen, closePrevious);

    void Open();
    void Close();
}