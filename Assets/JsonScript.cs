using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NBS : MonoBehaviour
{
    public Text t;
    public Jsonclass jsnData;
    public string jsonURL;
    void Start()
    {
        StartCoroutine(getData());
    }
    IEnumerator getData()
    {
        Debug.Log("Download...");
        var request = new UnityWebRequest(jsonURL);
        request.method = UnityWebRequest.kHttpVerbGET;
        var result = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(result);
        dh.removeFileOnAbort = true;
        request.downloadHandler = dh;
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            t.text = "ERROR";
        }
        else
        {
            Debug.Log("Download saved to: " + result);
            jsnData = JsonUtility.FromJson<Jsonclass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
            t.text = jsnData.TT.ToString();
            yield return StartCoroutine(getData());
        }
    }
    [System.Serializable]

    public class Jsonclass
    {
        public string TT;
    }
}