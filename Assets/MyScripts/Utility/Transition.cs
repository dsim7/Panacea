
using System.Collections;
using UnityEngine;

public abstract class Transition : MonoBehaviour {

    public void TransitionTo(string sceneName)
    {
        StartCoroutine(SceneTransition(sceneName));
    }

    protected abstract IEnumerator SceneTransition(string sceneName);

}
