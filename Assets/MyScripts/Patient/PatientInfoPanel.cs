using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Panel which displays the selected Patient's profile info.
public class PatientInfoPanel : MonoBehaviour {

    public Transform conditionBar;
    public Transform resilienceBar;
    public Transform riskBar;
    public Transform occupationText;
    
    private Patient selectedPatient;
    private Image conditionImage, resilienceImage, riskImage;
    private Text occupationTextComponent;

    void Start ()
    {
        PatientClickListener.PatientClicked += SetPatient;

        conditionImage = conditionBar.GetComponent<Image>();
        resilienceImage = resilienceBar.GetComponent<Image>();
        riskImage = riskBar.GetComponent<Image>();
        occupationTextComponent = occupationText.GetComponent<Text>();
    }
	
	void Update ()
    {
        UpdatePatientInfo();
	}

    private void SetPatient(Patient patient)
    {
        selectedPatient = patient;
    }

    private void UpdatePatientInfo()
    {
        if (selectedPatient != null)
        {
            conditionImage.fillAmount = selectedPatient.Profile.Condition;
            resilienceImage.fillAmount = selectedPatient.Profile.Resilience;
            riskImage.fillAmount = selectedPatient.Profile.Risk;
            occupationTextComponent.text = selectedPatient.Profile.Population.ToString();
        }
    }
}
