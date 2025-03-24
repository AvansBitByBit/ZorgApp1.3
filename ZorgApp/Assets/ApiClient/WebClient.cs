using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WebClient : MonoBehaviour
{
    public string baseUrl;
    private string token;


    private void Awake()
    {
        string savedToken = PlayerPrefs.GetString("access_token", "");

        if (!string.IsNullOrEmpty(savedToken))
        {
            token = savedToken;
            Debug.Log("✅ Loaded token from PlayerPrefs.");
        }
        else
        {
            Debug.LogWarning("⚠️ No token found in PlayerPrefs.");
        }
    }
    public void CheckToken()
    {
        token = PlayerPrefs.GetString("access_token", null);
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("⚠️ No token found in PlayerPrefs. Redirecting to login page.");
            SceneManager.LoadScene("LoginScene");
        }
        else
        {
            Debug.Log("✅ Token loaded from PlayerPrefs: " + token);
        }
    }

    public void SetToken(string token)
    {
        this.token = token;
        PlayerPrefs.SetString("access_token", token);
        PlayerPrefs.Save();
        Debug.Log("🔑 Token saved to PlayerPrefs." + token);
        
    }

    public void ClearToken()
    {
        token = null;
        PlayerPrefs.DeleteKey("access_token");
        PlayerPrefs.Save();
    }

    public async Awaitable<IWebRequestReponse> SendGetRequest(string route)
    {
        UnityWebRequest webRequest = CreateWebRequest("GET", route, "");
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendPostRequest(string route, string data)
    {
        UnityWebRequest webRequest = CreateWebRequest("POST", route, data);
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendPutRequest(string route, string data)
    {
        UnityWebRequest webRequest = CreateWebRequest("PUT", route, data);
        return await SendWebRequest(webRequest);
    }

    public async Awaitable<IWebRequestReponse> SendDeleteRequest(string route)
    {
        UnityWebRequest webRequest = CreateWebRequest("DELETE", route, "");
        return await SendWebRequest(webRequest);
    }

    private UnityWebRequest CreateWebRequest(string type, string route, string data)
    {
        string url = baseUrl + route;
        Debug.Log("Creating " + type + " request to " + url + " with data: " + data);

        data = RemoveIdFromJson(data); // Backend throws error if it receiving empty strings as a GUID value.
        var webRequest = new UnityWebRequest(url, type);
        byte[] dataInBytes = new UTF8Encoding().GetBytes(data);
        webRequest.uploadHandler = new UploadHandlerRaw(dataInBytes);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        AddToken(webRequest);
        return webRequest;
    }

    private async Awaitable<IWebRequestReponse> SendWebRequest(UnityWebRequest webRequest)
    {
        await webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string responseData = webRequest.downloadHandler.text;
            return new WebRequestData<string>(responseData);
        }
        else
        {
            Debug.LogError($"WebRequestError: {webRequest.error}, Response Code: {webRequest.responseCode}, URL: {webRequest.url}");
            return new WebRequestError(webRequest.error);
        }
    }
 
    private void AddToken(UnityWebRequest webRequest)
    {
        webRequest.SetRequestHeader("Authorization", "Bearer " + token);
    }

    private string RemoveIdFromJson(string json)
    {
        return json.Replace("\"id\":\"\",", "");
    }

}

[Serializable]
public class Token
{
    public string tokenType;
    public string accessToken;
}
