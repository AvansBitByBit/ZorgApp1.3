using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string Behandeltraject)
    {
        SceneManager.LoadScene(Behandeltraject);
    }
}
