
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PopulationManager : ScriptableObject {
    public const float MIN_OVERNIGHT_DECAY = 0.25f;
    public const float OVERNIGHT_DECAY_FACTOR = 0.8f;
    public const float OVERNIGHT_LIFE_LIMIT = 0.2f;

    public PatientProfileSet profiles;
    
    public List<Population> populations = new List<Population>();

    public void RecountDay(List<PanaceaEvent> events)
    {
        DecayOvernight();

        events.Clear();
        populations.ForEach(pop => pop.DailyDecay());
        foreach (Population pop in populations)
        {
            PanaceaEvent e = pop.GenerateEvent();
            e.TakeEffect();
            events.Add(e);
        }
    }

    private void DecayOvernight()
    {
        foreach (PatientProfile profile in profiles.Items)
        {
            profile.Life = Mathf.Max(PatientProfile.MAX_LIFE * MIN_OVERNIGHT_DECAY, profile.Life * OVERNIGHT_DECAY_FACTOR);
            profile.LifeLimit = Mathf.Min(profile.Life + (PatientProfile.MAX_LIFE * OVERNIGHT_LIFE_LIMIT), PatientProfile.MAX_LIFE);
            Debug.Log(profile.Life);
            Debug.Log(profile.LifeLimit);
        }
    }
}
