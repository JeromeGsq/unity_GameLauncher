using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public static class StreamingAssetsLoader
{
    private static Dictionary<string, object> cache = new Dictionary<string, object>();

    public static IEnumerator LoadJson<T>(string filePath, Action<T> onComplete)
    {
        if (cache.ContainsKey(filePath))
        {
            onComplete.Invoke((T)cache[filePath]);
        }
        else
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
                    if (!cache.ContainsKey(filePath))
                    {
                        cache.Add(filePath, data);
                    }
                    onComplete.Invoke(data);
                }
            }
        }
    }

    public static IEnumerator LoadSprite(string filePath, Action<Sprite> onComplete)
    {
        if (cache.ContainsKey(filePath))
        {
            onComplete.Invoke((Sprite)cache[filePath]);
        }
        else
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
                    if (!cache.ContainsKey(filePath))
                    {
                        cache.Add(filePath, sprite);
                    }
                    onComplete.Invoke(sprite);
                }
            }
        }
    }

    public static IEnumerator LoadTexture2D(string filePath, Action<Texture2D> onComplete)
    {
        if (cache.ContainsKey(filePath))
        {
            onComplete.Invoke((Texture2D)cache[filePath]);
        }
        else
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
                    texture.filterMode = FilterMode.Trilinear;
                    texture.wrapMode = TextureWrapMode.Mirror;
                    texture.Apply();
                    if (!cache.ContainsKey(filePath))
                    {
                        cache.Add(filePath, texture);
                    }
                    onComplete.Invoke(texture);
                }
            }
        }
    }

    public static void ClearCache()
    {
        cache.Clear();
    }
}
