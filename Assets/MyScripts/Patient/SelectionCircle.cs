using UnityEngine;

public class SelectionCircle : MonoBehaviour {

    void Start()
    {
        PatientClickListener.PatientClicked += SelectPatient;
    }

    void OnDisable()
    {
        PatientClickListener.PatientClicked -= SelectPatient;
    }

    private void SelectPatient(Patient patient)
    {
        transform.position = patient.transform.position;
    }
}
