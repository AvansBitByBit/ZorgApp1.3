using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public void StartAnimationToScene(string sceneName)
    {
        // Start the transition animation
        transition.SetTrigger("Start");

        // Wait for the animation to finish before loading the new scene
        StartCoroutine(LoadSceneAfterAnimation(sceneName));

    }
    
    private IEnumerator LoadSceneAfterAnimation(string sceneName)
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(2f); // Adjust the wait time based on your animation length
        
        // Load the new scene
        SceneManager.LoadScene(sceneName);
    }
}

