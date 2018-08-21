using UnityEngine;
using UnityEngine.Events;

// Ability base class
public abstract class Ability : ScriptableObject {

    public abstract string Name { get; }

    [SerializeField]
    private int _charges = 1;
    public int Charges { get { return _charges; } set { _charges = value; UpdateCharges(); } }

    public event UnityAction UpdateCharges = delegate { };

    private Patient target;

    // When Activated, an ability sets the target patient, decreases one charge, and takes affect.
    public void Activate(Patient target)
    {
        if (Charges != 0)
        {
            Debug.Log("Activated " + this);
            this.target = target;
            Charges -= 1;
            TakeAffect(target);
        }
    }

    // Abilities may Apply Effects. When this function is called, an Effect of the specified type
    // is instantiated and applied onto the target patient.
    public void ApplyEffect(Effect effect)
    {
        target.AddEffect(effect);
    }

    protected abstract void TakeAffect(Patient target);
}


