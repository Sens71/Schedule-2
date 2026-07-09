using System;
using UnityEngine;

[Serializable]
public class CanDrug
{
    public Color iconColor;
    public float adictivness;
    public float energizing;
    public float focused;
    public float athletics;
    public float calming;
    public float brightEyed;
    public float disorienting;
    public float foggy;
    public float glowing;
    public float longFaced;
    public float sedating;
    public float seizure;
    public float slippery;
    public float sneaky;
    
    public CanDrug(params ReagentData[] reagents)
    {
        foreach (var reagent in reagents)
        {
            adictivness += reagent.adictivness;
            energizing += reagent.energizing;
            focused += reagent.focused;
            athletics += reagent.athletics;
            calming += reagent.calming;
            brightEyed += reagent.brightEyed;
            disorienting += reagent.disorienting;
            foggy += reagent.foggy;
            glowing += reagent.glowing;
            longFaced += reagent.longFaced;
            sedating += reagent.sedating;
            seizure += reagent.seizure;
            slippery += reagent.slippery;
            sneaky += reagent.sneaky;
        }
        this.adictivness = Mathf.Clamp(adictivness, 0f, 100);
        this.energizing = Mathf.Clamp(energizing, 0f, 100);
        this.focused = Mathf.Clamp(focused, 0f, 100);
        this.athletics = Mathf.Clamp(athletics, 0f, 100);
        this.calming = Mathf.Clamp(calming, 0f, 100);
        this.brightEyed = Mathf.Clamp(brightEyed, 0f, 100);
        this.disorienting = Mathf.Clamp(disorienting, 0f, 100);
        this.foggy = Mathf.Clamp(foggy, 0f, 100);
        this.glowing = Mathf.Clamp(glowing, 0f, 100);
        this.longFaced = Mathf.Clamp(longFaced, 0f, 100);
        this.sedating = Mathf.Clamp(sedating, 0f, 100);
        this.seizure = Mathf.Clamp(seizure, 0f, 100);
        this.slippery = Mathf.Clamp(slippery, 0f, 100);
        this.sneaky = Mathf.Clamp(sneaky, 0f, 100);
        DefineColor();
    }

    private void DefineColor()
    {
        (Color color, float weight)[] contributions =
        {
            (Effects.adictivness.color,  adictivness),
            (Effects.energizing.color,   energizing),
            (Effects.focused.color,      focused),
            (Effects.athletics.color,    athletics),
            (Effects.calming.color,      calming),
            (Effects.brightEyed.color,   brightEyed),
            (Effects.disorienting.color, disorienting),
            (Effects.foggy.color,        foggy),
            (Effects.glowing.color,      glowing),
            (Effects.longFaced.color,    longFaced),
            (Effects.sedating.color,     sedating),
            (Effects.seizure.color,      seizure),
            (Effects.slippery.color,     slippery),
            (Effects.sneaky.color,       sneaky),
        };

        Color sum = Color.black;
        float totalWeight = 0f;

        foreach (var (color, weight) in contributions)
        {
            float w = weight / 100f;
            sum += color * w;
            totalWeight += w;
        }

        iconColor = totalWeight > 0f ? sum / totalWeight : Color.gray;
        iconColor.a = 1f;
    }
}
