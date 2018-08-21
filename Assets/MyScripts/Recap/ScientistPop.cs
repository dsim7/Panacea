using UnityEngine;

[CreateAssetMenu]
public class ScientistPop : Population
{

    [Header("Science Progress")]
    public BasicHeal basicHeal;
    public int progress = 0;

    private int progressRequired = 7;

    protected override void EventEffects(SuccessLevel successLevel)
    {
        basicHeal.Reset();
        progress += (int)successLevel;
        if (progress > progressRequired)
        {
            progress -= progressRequired;
            basicHeal.Upgrade();
        }
    }
}