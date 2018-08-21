using System;
using UnityEngine;

[Serializable]
public abstract class Population : ScriptableObject {
    
    [SerializeField]
    private float _populationStrength = 0.8f;
    public float PopulationStrength
    {
        get { return _populationStrength; }
        set { _populationStrength = Mathf.Clamp01(value); }
    }

    [SerializeField]
    private float _dailyDecayAmount = 0.1f;
    public float DailyDecayAmount { get { return _dailyDecayAmount; } private set { _dailyDecayAmount = value; } }

    [Header("Event Success Calculations")]
    [SerializeField]
    private float _strengthDeviation = 0.25f;
    public float StrengthDeviation { get { return _strengthDeviation; } private set { _strengthDeviation = value; } }
    [Space]
    [SerializeField]
    private float _successThresholdVeryBad = 0.2f;
    public float SuccessThresholdVeryBad { get { return _successThresholdVeryBad; } private set { _successThresholdVeryBad = value; } }
    [SerializeField]
    private float _successThresholdBad = 0.4f;
    public float SuccessThresholdBad { get { return _successThresholdBad; } private set { _successThresholdBad = value; } }
    [SerializeField]
    private float _successThresholdGood = 0.8f;
    public float SuccessThresholdGood { get { return _successThresholdGood; } private set { _successThresholdGood = value; } }

    [Header("Event Texts")]
    [TextArea(2, 4)]
    public string[] eventDescriptions;
    [TextArea(2, 4)]
    public string[] successDescriptions = new string[Enum.GetNames(typeof(SuccessLevel)).Length];

    public void DailyDecay()
    {
        PopulationStrength -= DailyDecayAmount;
    }

    public PanaceaEvent GenerateEvent()
    {
        PanaceaEvent newEvent = new PanaceaEvent();
        SuccessLevel successLevel = GenerateEventSuccessLevel();
        newEvent.SuccessLevel = successLevel;
        newEvent.EventText = eventDescriptions[UnityEngine.Random.Range(0, eventDescriptions.Length)] + " " +
            successDescriptions[(int)successLevel];
        newEvent.Effect += EventEffects;
        return newEvent;
    }

    private SuccessLevel GenerateEventSuccessLevel()
    {
        float determinant = Mathf.Clamp01(UnityEngine.Random.Range(
            PopulationStrength - StrengthDeviation, PopulationStrength + StrengthDeviation));
        Debug.Log("Determinant: " + determinant + " From " +
            (PopulationStrength - StrengthDeviation) + " to " + (PopulationStrength + StrengthDeviation));
        if (determinant < SuccessThresholdVeryBad)
        {
            return SuccessLevel.VeryBad;
        }
        if (determinant < SuccessThresholdBad)
        {
            return SuccessLevel.Bad;
        }
        if (determinant < SuccessThresholdGood)
        {
            return SuccessLevel.Good;
        }
        return SuccessLevel.VeryGood;
    }

    protected abstract void EventEffects(SuccessLevel successLevel);

}
