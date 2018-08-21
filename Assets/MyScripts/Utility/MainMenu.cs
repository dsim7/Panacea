using UnityEngine;

public class MainMenu : MonoBehaviour {

    private Transition transition;

    void Start()
    {
        transition = GetComponent<Transition>();
        Debug.Log(transition);
    }

    public void Play()
    {
        transition.TransitionTo("Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    
}
