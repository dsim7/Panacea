using UnityEngine;

[CreateAssetMenu]
public class Painkillers : Ability
{
    public override string Name
    {
        get
        {
            return "Painkillers";
        }
    }

    protected override void TakeAffect(Patient target)
    {
        ApplyEffect(new PainkillersEffect());
    }
}

public class PainkillersEffect : Effect
{

    public override float MaxTime
    {
        get
        {
            return 45f;
        }
    }

    public override void EffectOnDecay(ref float baseDecay, ref float baseCondDecay)
    {
        if (baseDecay > 0 && !patient.Profile.IsCritical)
        {
            baseCondDecay = 0;
            baseDecay = 0;
        }
    }
}