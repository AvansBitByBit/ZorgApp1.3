using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using JetBrains.Annotations;

public class SceneLoader : MonoBehaviour
{
    [CanBeNull] public Animator transition;
    public void StartAnimationToScene(string sceneName)
    {
        if (transition != null)
        {
            // Start the transition animation
            transition.SetTrigger("Start");

            // Wait for the animation to finish before loading the new scene
            StartCoroutine(LoadSceneAfterAnimation(sceneName));
        }
    }

    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
    
    private IEnumerator LoadSceneAfterAnimation(string sceneName)
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(2f); // Adjust the wait time based on your animation length
        
        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}

