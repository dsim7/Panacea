
using UnityEngine;

// Ability manager attached to Main Camera's canvas responsible for activating abilities.
public class AbilityManager : MonoBehaviour {

    #region PUBLIC VARIABLES
    public AbilitySet abilities;
    public Transform abilityButtonPrefab;
    #endregion

    #region UNITY CALLBACKS
    void Start()
    {
        InitAbilities();
    }
    #endregion

    #region PUBLIC FUNCTIONS
    public void InitAbilities()
    {
        foreach (Ability ability in abilities.Items)
        {
            Transform newButton = Instantiate(abilityButtonPrefab, transform);
            newButton.GetComponent<AbilityButton>().InitButtonAbility(ability);
        }
    }

    public Ability AddAbility<T>(int charges = 1) where T : Ability
    {
        Transform newButton = Instantiate(abilityButtonPrefab, transform);
        Ability newAbility = newButton.GetComponent<AbilityButton>().InitAbility<T>();
        newAbility.Charges = charges;
        abilities.Add(newAbility);
        return newAbility;
    }
    #endregion

    #region PRIVATE FUNCTIONS

    #endregion
}
