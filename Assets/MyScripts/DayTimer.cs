using UnityEngine;
using UnityEngine.UI;


public class DayTimer : MonoBehaviour {

    public GameObject dayText;
    public int dayLength;
    public bool dayEnded { get; set; }
    
    private Text textComponent;
    private Transition transition;
    private float start;


    void Start () {
        transition = GetComponent<Transition>();
        textComponent = dayText.GetComponent<Text>();

        dayEnded = false;
        ResetTimer();
    }
	
	void Update () {
        if (!dayEnded)
        {
            int elapsed = (int)(Time.time - start);
            textComponent.text = elapsed.ToString();
            if (elapsed == dayLength)
            {

                dayEnded = true;
                transition.TransitionTo("Recap");
            }
        }
	}

    private void ResetTimer()
    {
        start = Time.time;
    }
}
