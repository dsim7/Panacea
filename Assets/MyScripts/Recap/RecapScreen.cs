using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecapScreen : MonoBehaviour {

    #region PUBLIC VARIABLES
    public PopulationManager populationManager;
    public Transform infoPanel;
    public GameObject recapTextPrefab;
    #endregion

    #region PRIVATE VARIABLES
    private float timeBetweenTextSpawns = 0.25f;
    private List<PanaceaEvent> events = new List<PanaceaEvent>();
    #endregion

    #region UNITY CALLBACKS
    void Start()
    {
        StartCoroutine(ShowRecapCoroutine());
    }
    #endregion

    #region PUBLIC FUNCTIONS
    #endregion

    #region PRIVATE FUNCTIONS
    private IEnumerator ShowRecapCoroutine()
    {
        Debug.Log("Show Recap Screen");
        
        ClearRecapPanel();

        // wait for intro animation to finish
        Animator a = GetComponent<Animator>();
        while (!a.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            yield return null;
        }

        // populate events
        GenerateEvents();
        StartCoroutine(PopulateRecapPanel());
    }

    private void GenerateEvents()
    {
        populationManager.RecountDay(events);
    }

    private void ClearRecapPanel()
    {
        foreach (Transform child in infoPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private IEnumerator PopulateRecapPanel()
    {
        foreach (PanaceaEvent e in events)
        {
            GameObject newRecapText = Instantiate(recapTextPrefab, infoPanel);
            newRecapText.GetComponent<Text>().text = e.EventText;
            yield return new WaitForSeconds(timeBetweenTextSpawns);
        }
    }

    #endregion
}

