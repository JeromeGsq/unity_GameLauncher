using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class StreamingAssetsLoader
{
    public static IEnumerator LoadJson<T>(string filePath, Action<T> onComplete)
    {
        var path = Path.Combine(Application.streamingAssetsPath, filePath);
        using (var request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.Log(request.error);
            }
            else
            {
                T data = JsonConvert.DeserializeObject<T>(request.downloadHandler.text);
                onComplete.Invoke(data);
            }
        }
    }

    public static IEnumerator LoadSprite(string filePath, Action<Sprite> onComplete)
    {
        var path = Path.Combine(Application.streamingAssetsPath, filePath);

        using (var request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.Log(request.error);
            }
            else
            {
                var texture = new Texture2D(2, 2);
                texture.LoadImage(request.downloadHandler.data);
                texture.Compress(true);

                var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
                onComplete.Invoke(sprite);
            }
        }
    }

    public static IEnumerator LoadTexture2D(string filePath, Action<Texture2D> onComplete)
    {
        var path = Path.Combine(Application.streamingAssetsPath, filePath);

        using (var request = UnityWebRequest.Get(path))
        {
            yield return request.SendWebRequest();

            if (!string.IsNullOrEmpty(request.error))
            {
                Debug.Log(request.error);
            }
            else
            {
                var texture = new Texture2D(2, 2);
                texture.LoadImage(request.downloadHandler.data);

                onComplete.Invoke(texture);
            }
        }
    }
}
