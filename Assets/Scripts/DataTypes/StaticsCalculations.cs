using UnityEngine;

public class StaticsCalculations
{
    public static bool CompareDrugs(Drug drug1, Drug drug2)
    {
        if (drug1.icon == drug2.icon &&
            Mathf.Approximately(drug2.adictivness, drug1.adictivness) &&
            Mathf.Approximately(drug2.energizing, drug1.energizing) &&
            Mathf.Approximately(drug2.focused, drug1.focused) &&
            Mathf.Approximately(drug2.athletics, drug1.athletics) &&
            Mathf.Approximately(drug2.calming, drug1.calming) &&
            Mathf.Approximately(drug2.brightEyed, drug1.brightEyed) &&
            Mathf.Approximately(drug2.disorienting, drug1.disorienting) &&
            Mathf.Approximately(drug2.foggy, drug1.foggy) &&
            Mathf.Approximately(drug2.glowing, drug1.glowing) &&
            Mathf.Approximately(drug2.longFaced, drug1.longFaced) &&
            Mathf.Approximately(drug2.sedating, drug1.sedating) &&
            Mathf.Approximately(drug2.seizure, drug1.seizure) &&
            Mathf.Approximately(drug2.slippery, drug1.slippery) &&
            Mathf.Approximately(drug2.sneaky, drug1.sneaky))
        {
            return true;
        }
        return false;
    }
}