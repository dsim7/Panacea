
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PatientProfile : ScriptableObject {
    #region CONSTANTS
    public const float MAX_LIFE = 1000;
    public const float TIME_IN_CRITICAL = 12;
    public const float LIFE_LIMIT_GROWTH_RATE = 15f;
    public const float LIFE_LIMIT_GROWTH_THRESHOLD = 100f;
    #endregion

    #region PUBLIC VARIABLES
    public PatientProfileSet set;

    [SerializeField]
    private Population _population;
    public Population Population { get { return _population; } set { _population = value; } }

    [Header("Starting Values")]
    [SerializeField]
    private float startLife;
    [SerializeField]
    private float startCondition;
    [SerializeField]
    private float startResilience;
    [SerializeField]
    private float startLifeLimit;

    [Header("Actual Values")]
    // Life
    [SerializeField]
    [Range(1f, MAX_LIFE)]
    private float _life;
    public float Life
    {
        get { return _life; }
        set { _life = Mathf.Clamp(value, 0, MAX_LIFE); if (_life == 0) Die(); else if (_life == MAX_LIFE) Cured(); }
    }

    // Life Limit
    [SerializeField]
    [Range(1f, MAX_LIFE)]
    private float _lifeLimit;
    public float LifeLimit
    {
        get { return _lifeLimit; }
        set { _lifeLimit = Mathf.Clamp(value, 0, MAX_LIFE); }
    }

    // Condition
    [SerializeField]
    [Range(0f, 1f)]
    private float _condition;
    public float Condition
    {
        get { return _condition; }
        set { _condition = Mathf.Clamp01(value); }
    }

    [SerializeField]
    private float _conditionDecayRate;
    public float ConditionDecayRate
    {
        get { return _conditionDecayRate; }
        set { _conditionDecayRate = Mathf.Clamp(value, -0.04f, 0f); }
    }

    // Resilience
    [SerializeField]
    [Range(0f, 1f)]
    private float _resilience;
    public float Resilience
    {
        get { return _resilience; }
        set { _resilience = Mathf.Clamp01(value); }
    }

    // Risk
    [SerializeField]
    [Range(0f, 1f)]
    private float _risk;
    public float Risk
    {
        get { return _risk; }
        set { _risk = Mathf.Clamp01(value); }
    }

    [SerializeField]
    private bool _isCured = false;
    public bool IsCured { get { return _isCured; } set { _isCured = value; } }

    [SerializeField]
    private bool _isDead = false;
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }

    [SerializeField]
    private bool _isCritical = false;
    public bool IsCritical { get { return _isCritical; } set { _isCritical = value; } }
    
    #endregion

    #region UNITY CALLBACKS
    void OnEnable()
    {
        if (set != null)
        {
            set.Add(this);
        }
        Life = startLife;
        Condition = startCondition;
        LifeLimit = startLifeLimit;
        Resilience = startResilience;
        IsCritical = false;
        IsCured = false;
        IsDead = false;
    }

    void OnDisable()
    {
        if (set != null)
        {
            set.Remove(this);
        }
    }
    #endregion

    #region PRIVATE VARIABLES
    private List<Effect> effects = new List<Effect>();
    #endregion
    
    #region PUBLIC FUNCTIONS
    public void InitRandom()
    {
        Life = Random.Range(MAX_LIFE * 0.15f, MAX_LIFE * 0.4f);
        LifeLimit = Life + Random.Range(MAX_LIFE * 0.1f, MAX_LIFE * 0.3f);
        Condition = Random.Range(0.6f, 0.9f);
        Resilience = Random.Range(0.1f, 0.9f);
        Risk = Random.Range(0.1f, 0.9f);
        ConditionDecayRate = Random.Range(-0.05f, -0.005f);
    }

    public void Heal(float amount)
    {
        if (!IsCured && !IsDead)
        {
            float healAmount = MultiplyHeal(amount);
            float total = Life + healAmount;
            if (total > LifeLimit)
            {
                Life = LifeLimit;
            }
            else
            {
                Life = total;
            }
        }
    }

    public void Decay()
    {
        // unaffected if cured or dead
        if (IsCured || IsDead)
        {
            return;
        }

        // get decay amount
        float decayAmount = IsCritical ? 25f : GetConditionTick();
        float condDecayAmount = ConditionDecayRate;

        // mod decay amount based on effects
        effects.ForEach(e => e.EffectOnDecay(ref decayAmount, ref condDecayAmount));

        // decay life
        Life -= decayAmount;

        // decay condition
        Condition += condDecayAmount;

        // increment life limit
        IncrementLifeLimit();
    }

    public void AddEffect(Effect effect)
    {
        effects.Add(effect);
        effect.EffectOnApplication();
    }

    public void RemoveEffect(Effect effect)
    {
        effects.Remove(effect);
    }
    // The patient goes critical for certain amount of time where their condition becomes -20%
    public IEnumerator GoCritical()
    {
        if (!IsCritical)
        {
            IsCritical = true;
            //face.GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(PatientProfile.TIME_IN_CRITICAL);
            IsCritical = false;
            //face.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public bool DetermineGoCritical()
    {
        float random = Random.Range(0.0f, 1.0f);
        return random < 0.4f;
    }
    #endregion

    #region PRIVATE FUNCTIONS
    private float GetConditionTick()
    {
        float decayAmount = -13 * (Condition - 0.85f);
        return decayAmount;
    }

    private float MultiplyHeal(float amount)
    {
        return amount * Mathf.Pow(Resilience + 0.5f, 1.5f);
    }

    private void IncrementLifeLimit()
    {
        if (Life > LifeLimit - LIFE_LIMIT_GROWTH_THRESHOLD)
        {
            LifeLimit += LIFE_LIMIT_GROWTH_RATE;
        }
    }

    private void Die()
    {
        IsDead = true;
        Debug.Log("Population strength reduced");
        _population.PopulationStrength -= 0.1f;
    }

    private void Cured()
    {
        IsCured = true;
        Debug.Log("Population strength increased");
        _population.PopulationStrength += 0.1f;
    }
    #endregion

}
