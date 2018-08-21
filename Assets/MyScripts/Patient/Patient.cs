
using UnityEngine;

[System.Serializable]
public class Patient : MonoBehaviour {

    [SerializeField]
    private PatientProfile _profile;
    public PatientProfile Profile { get { return _profile; } set { _profile = value; } }

    #region UNITY CALLBACKS
    void Start()
    {
        PatientManager.onTick += Decay;
    }


    void OnDisable()
    {
        PatientManager.onTick -= Decay;
    }
    #endregion


    #region PUBLIC FUNCTIONS
    // Heal for an amount modified by resilience
    public void Heal(float amount)
    {
        _profile.Heal(amount);
        UpdateLifeBar();
    }

    // Lose health based on their condition
    public void Decay()
    {
        _profile.Decay();
        UpdateLifeBar();
    }

    // Applies an Effect from an ability
    public void AddEffect(Effect effect)
    {
        effect.patient = this;
        StartCoroutine(effect.ExpireTimer());
        StartCoroutine(effect.TickTimer());
        _profile.AddEffect(effect);
    }

    public void RemoveEffect(Effect effect)
    {
        _profile.RemoveEffect(effect);
    }
    
    // Updates the health bar display
    public void UpdateLifeBar()
    {
        HealthBar healthBar = GetComponent<HealthBar>();
        healthBar.OnHealthChanged(_profile.Life / PatientProfile.MAX_LIFE);
        healthBar.OnHealthLimitChanged(_profile.LifeLimit / PatientProfile.MAX_LIFE);
    }
    
    
    #endregion


    #region PRIVATE FUNCTIONS




    #endregion

}


