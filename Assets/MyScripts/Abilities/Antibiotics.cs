using UnityEngine;

[CreateAssetMenu]
public class Antibiotics : Ability
{
    public override string Name
    {
        get
        {
            return "Antibiotics";
        }
    }

    protected override void TakeAffect(Patient target)
    {
        ApplyEffect(new AntibioticsEffect());
    }


}

public class AntibioticsEffect : Effect
{
    public float increaseAmount = 0.5f;

    private float difference;

    public override float MaxTime
    {
        get
        {
            return 45f;
        }
    }

    public override void EffectOnApplication()
    {
        difference = Mathf.Min(patient.Profile.Resilience + increaseAmount, 1f) - patient.Profile.Resilience;
        patient.Profile.Resilience += difference;
    }

    public override void EffectOnExpiration()
    {
        patient.Profile.Resilience -= difference;
    }
}

