using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Patient))]
public class PatientClickListener : MonoBehaviour {
    
    public static event UnityAction<Patient> PatientClicked = delegate { };

    private Patient patient;

    void Start()
    {
        patient = GetComponent<Patient>();
    }

    void OnMouseDown()
    {
        PatientClicked(patient);
    }
}
