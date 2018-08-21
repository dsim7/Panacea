using UnityEngine;

// The patient is healed and their condition improves slightly

[CreateAssetMenu]
public class BasicHeal : Ability
{
    public const float MAX_MULTIPLIER = 4f;
    public const float MULTIPLIER_INTERVAL = 0.5f;

    public float castTime = 2f;
    public float healAmount = 30f;
    public float conditionHealAmount;
    public float upgradedCastTime = 1f;

    public override string Name
    {
        get
        {
            return "Tend";
        }
    }

    private float defaultCastTime = 2f;
    private float currentMultiplier = 1f;
    private Patient previousTarget = null;

    public void Reset()
    {
        castTime = defaultCastTime;
    }

    public void Upgrade()
    {
        castTime = upgradedCastTime;
    }

    protected override void TakeAffect(Patient target)
    {
        Debug.Log("basic heal take effect");
        currentMultiplier = target == previousTarget ? Mathf.Min(currentMultiplier + MULTIPLIER_INTERVAL, MAX_MULTIPLIER) : 1;
        previousTarget = target;
        Charges = -1;
        target.Heal(healAmount * currentMultiplier);
        if (!target.Profile.IsCritical)
        {
            target.Profile.Condition += conditionHealAmount;
        }
    }
}
