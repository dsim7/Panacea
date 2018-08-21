using UnityEngine;
using UnityEngine.UI;

// Script to manage Health Bar display
public class HealthBar : MonoBehaviour
{
    #region PUBLIC VARIABLES
    public GameObject health;
    #endregion

    #region PRIVATE_VARIABLES
    //private Vector2 positionCorrection = new Vector2(0, 1.85f);
    //private RectTransform targetCanvas;
    private RectTransform healthBar;
    private RectTransform healthLimitBar;
    //private Transform patientTransform;
    #endregion

    #region UNITY_CALLBACKS
    void Start()
    {
        //patientTransform = GetComponent<Transform>();
        //targetCanvas = GameObject.Find("UI").transform as RectTransform;
        
        healthLimitBar = health.transform.GetChild(0) as RectTransform;
        healthBar = health.transform.GetChild(1) as RectTransform;
        GetComponent<Patient>().UpdateLifeBar();
        //RepositionHealthBar();
    }
    #endregion

    #region PUBLIC_METHODS
    public void OnHealthChanged(float healthFill)
    {
        healthBar.GetComponent<Image>().fillAmount = healthFill;
    }

    public void OnHealthLimitChanged(float healthLimitFill)
    {
        healthLimitBar.GetComponent<Image>().fillAmount = healthLimitFill;
    }
    #endregion

    /*
    #region PRIVATE_METHODS
    private void RepositionHealthBar()
    {
        health.transform.position = (Vector2) patientTransform.position + positionCorrection;
    }
    #endregion
    */
}
