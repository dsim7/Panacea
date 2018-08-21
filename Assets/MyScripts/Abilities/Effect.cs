using System.Collections;
using UnityEngine;

// Base class for Effects
public abstract class Effect {
    public Patient patient;

    public bool active = true;

    private float _maxTime = -1;
    public virtual float MaxTime { get { return _maxTime; } }

    private float _tickRate = -1;
    public virtual float TickRate { get { return _tickRate; } }

    // Effect which occurs on application
    public virtual void EffectOnApplication() { }

    // Effect which occurs every frame
    public virtual void EffectOnFrame() { }

    // Effect which occurs on Tick
    public virtual void EffectOnTick() { }

    // Effect which occurs when the patient decays. Modifies the decay rate.
    public virtual void EffectOnDecay(ref float baseDecay, ref float baseCondDecay) { }

    // Effect which occurs when the effect expiration
    public virtual void EffectOnExpiration() { }

    public void End()
    {
        EffectOnExpiration();
        patient.RemoveEffect(this);
        active = false;
    }
    
    public IEnumerator ExpireTimer()
    {
        if (MaxTime == -1) // -1 means infinite duration
        {
            yield return null;
        }
        else
        {
            yield return new WaitForSeconds(MaxTime);
            End();
        }
    }

    public IEnumerator TickTimer()
    {
        while (active)
        {
            yield return new WaitForSeconds(TickRate);
            if (active)
            {
                EffectOnTick();
            }
        }
    }
}

