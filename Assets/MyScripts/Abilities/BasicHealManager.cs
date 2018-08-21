
using UnityEngine;
using UnityEngine.UI;

// Basic Heal Manager responsible for controlling the activation and execution of Basic Heal.
public class BasicHealManager : MonoBehaviour {

    public BasicHeal basicHeal;
    public Image basicHealCastBar;
    
    private float basicHealCurrentTime = 0f;
    private Patient previousTarget;
    private Patient target;
    private bool castingBasicHeal = false;

    void Start()
    {
        PatientClickListener.PatientClicked += BasicHealClicked;
    }
	
	void Update () {
        if (castingBasicHeal)
        {
            basicHealCurrentTime += Time.deltaTime;
            if (basicHealCurrentTime >= basicHeal.castTime)
            {
                basicHealCurrentTime = 0f;
                castingBasicHeal = false;
                basicHeal.Activate(target);
            }
            basicHealCastBar.fillAmount = basicHealCurrentTime / basicHeal.castTime;
        }
    }

    public void BasicHealClicked(Patient target)
    {
        if (!castingBasicHeal)
        {
            if (previousTarget == target)
            {
                this.target = target;
                castingBasicHeal = true;
            }
            else
            {
                previousTarget = target;
            }
        }
    }
}
