using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Nurse : Ability
{
    private static Queue<NurseEffect> nurses = new Queue<NurseEffect>();

    public override string Name
    {
        get
        {
            return "Assign Nurse";
        }
    }

    protected override void TakeAffect(Patient target)
    {
        NurseEffect newNurse = new NurseEffect();
        ApplyEffect(newNurse);
        Charges++;
        nurses.Enqueue(newNurse);
        if (nurses.Count > Charges)
        {
            while (nurses.Count > Charges)
            {
                nurses.Peek().End();
                nurses.Dequeue();
            }
        }     
    }
}

public class NurseEffect : Effect
{
    public const float HEAL_AMOUNT = 20f;

    public override float MaxTime
    {
        get
        {
            return -1;
        }
    }

    public override float TickRate
    {
        get
        {
            return 3f;
        }
    }

    public override void EffectOnTick()
    {
        patient.Heal(HEAL_AMOUNT);
    }
}