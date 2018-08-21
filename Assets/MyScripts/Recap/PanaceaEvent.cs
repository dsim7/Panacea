using UnityEngine.Events;

public struct PanaceaEvent {

    public string EventText { get; set; }
    public SuccessLevel SuccessLevel { get; set; }
    public event UnityAction<SuccessLevel> Effect;
    
    public void TakeEffect()
    {
        Effect(SuccessLevel);
    }
}
