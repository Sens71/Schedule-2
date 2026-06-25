using UnityEngine;

public class CanDrug
{
    public float adictivness;
    public float energizing;
    public float focused;
    public Color iconColor;
    
    public CanDrug(float adictivness=0f, float energizing=0f, float focused=0f)
    {
        this.adictivness = adictivness;
        this.energizing = energizing;
        this.focused = focused;
    }
}
