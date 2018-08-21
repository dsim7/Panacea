using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PatientManager : MonoBehaviour {

    public PatientProfileSet patients;
    [Space]
    [Range(0.01f, 5f)]
    public float tickRate = 1f;

    public static event UnityAction onTick = delegate { };
    private IEnumerator decayCoroutine;
    private IEnumerator criticalCoroutine;

    void Start()
    {
        decayCoroutine = DecayTick();
        criticalCoroutine = CheckCritical();
        SortPatientsByRisk();
        StartPatientCoroutines();
    }

    public void StartPatientCoroutines()
    {
        StartCoroutine(decayCoroutine);
        StartCoroutine(criticalCoroutine);
    }

    public void EndPatientCoroutines()
    {
        StopCoroutine(decayCoroutine);
        StopCoroutine(criticalCoroutine);
    }


    public IEnumerator DecayTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickRate);
            onTick();
        }
    }

    public IEnumerator CheckCritical()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 35f));

            foreach (PatientProfile patient in patients.Items)
            {
                if (patient.DetermineGoCritical())
                {
                    patient.GoCritical();
                    break;
                }
            }
        }
    }

    public void SortPatientsByRisk()
    {
        patients.Items.Sort((PatientProfile p1, PatientProfile p2) =>
        {
            if (p1 == null && p2 == null)
                return 0;
            else if (p1 == null)
                return -1;
            else if (p2 == null)
                return 1;
            else
                return p2.Risk.CompareTo(p1.Risk);
        });
    }


}
