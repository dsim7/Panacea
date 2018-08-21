using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FadeToBlackTransition : Transition {

    public GameObject blackScreen;
    public float fadeSpeed = 1;

    private Animator animator;

    void Start()
    {
        animator = blackScreen.GetComponent<Animator>();
        animator.speed = fadeSpeed;
    }

    protected override IEnumerator SceneTransition(string sceneName)
    {
        animator.Play("FadeOut");
        yield return null;
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2);
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
