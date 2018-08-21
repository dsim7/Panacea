using UnityEngine;

[CreateAssetMenu]
public class ScavengerPop : Population
{
    [Header("Abilities")]
    public AbilitySet allAbilities;

    protected override void EventEffects(SuccessLevel successLevel)
    {
        for (int i = 0; i < (int) successLevel; i++)
        {
            Ability ability = allAbilities.Items[Random.Range(0, allAbilities.Items.Count - 1)];
            if (ability is Nurse)
            {
                i--;
                continue;
            }
            ability.Charges++;
        }
    }
}