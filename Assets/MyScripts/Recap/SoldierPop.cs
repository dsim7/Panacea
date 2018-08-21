using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SoldierPop : Population
{
    [Header("Patient Profiles")]
    public PatientProfileSet profiles;
    public PopulationManager populationManager;

    protected override void EventEffects(SuccessLevel successLevel)
    {
        for (int i = 0; i < (4 - (int)successLevel); i++)
        {
            if (profiles.Items.Count < 12)
            {
                PatientProfile newInjury = CreateInstance<PatientProfile>();
                newInjury.InitRandom();
                int randomPop = Random.Range(0, populationManager.populations.Count - 1);
                Debug.Log("Random population: " + randomPop);
                newInjury.Population = populationManager.populations[randomPop];
                newInjury.set = profiles;
                profiles.Add(newInjury);
            }
        }
    }
}
