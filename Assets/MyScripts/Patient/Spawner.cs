using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public PatientProfileSet profiles;
    [Space]
    public Transform UI;
    public Transform patientPrefab;
    public List<Transform> spawnPoints;

    private int currentSpawnPoint = 0;
    
    void Start () {
        PopulatePatients();
    }

    public void PopulatePatients()
    {
        foreach (PatientProfile profile in profiles.Items)
        {
            Transform patientTransform = Instantiate(patientPrefab, spawnPoints[currentSpawnPoint++].position, Quaternion.identity);
            Patient patient = patientTransform.GetComponent<Patient>();
            patient.Profile = profile;
            patientTransform.parent = UI.transform;
        }
    }
}

