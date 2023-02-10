using System;
using System.Collections;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Demo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string _fileName = "demo.json";

    void Start()
    {
        StartCoroutine(GetJson());
    }

    IEnumerator GetJson()
    {
        Debug.Log($"Application.streamingAssetsPath: {Application.streamingAssetsPath}");
        // load json file
        var path = $"{Application.streamingAssetsPath}/{_fileName}";
        var www = UnityWebRequest.Get(path);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("GetJson failed" + www.error);
            yield break;
        }

        // parse json
        var data = JsonConvert.DeserializeObject<Data>(www.downloadHandler.text);
        // display data
        _text.text = data.entryCode;
    }
}

[Serializable]
public class Data
{
    [JsonProperty("entry_code")]
    public string entryCode;
}