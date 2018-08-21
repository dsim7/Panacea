using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {

    public Ability ability;

    private Patient target;
    private Button button;
    private Text abilityName;
    private Text charges;

    public void InitButtonAbility(Ability abilityToInit)
    {
        button = GetComponent<Button>();
        ability = abilityToInit;
        charges = transform.Find("Charges").GetComponent<Text>();
        abilityName = transform.Find("AbilityText").GetComponent<Text>();
        SetName();

        SubscribeChargesListener();
        SubscribeToPatientClick();
        InitClickListener();
        UpdateCharges();
    }

    public Ability InitAbility<T>() where T : Ability
    {
        ability = ScriptableObject.CreateInstance<T>();
        charges = transform.Find("Charges").GetComponent<Text>();
        abilityName = transform.Find("AbilityText").GetComponent<Text>();
        SetName();

        SubscribeToPatientClick();
        SubscribeChargesListener();
        InitClickListener();
        UpdateCharges();
        return ability;
    }
    
    private void SetTarget(Patient target)
    {
        this.target = target;
    }

    private void SubscribeToPatientClick()
    {
        PatientClickListener.PatientClicked += SetTarget;
    }

    private void SubscribeChargesListener()
    {
        ability.UpdateCharges += UpdateCharges;
    }

    private void InitClickListener()
    {
        if (ability != null)
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                if (target != null)
                {
                    ability.Activate(target);
                    UpdateCharges();
                }
            });
        }
    }

    private void SetName()
    {
        if (ability != null && abilityName != null)
        {
            abilityName.text = ability.Name;
        }
    }

    private void UpdateCharges()
    {
        if (ability != null && charges != null)
        {
            charges.text = ability.Charges.ToString();
        }

        if (button != null)
        {
            if (ability.Charges == 0)
            {
                GetComponent<Button>().interactable = false;
            }
            else
            {
                GetComponent<Button>().interactable = true;
            }
        }

    }
}
