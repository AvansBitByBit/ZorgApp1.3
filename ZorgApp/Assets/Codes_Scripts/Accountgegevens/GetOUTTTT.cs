using UnityEngine;

public class GetOUTTTT : MonoBehaviour
{
    public WebClient webClient;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        webClient.CheckToken();
        
    }


}
