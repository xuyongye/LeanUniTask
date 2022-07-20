using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Networking;

public class UniTaskTest
{

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async UniTask<UnityEngine.Object> LoadTestTextAsync<T>(string path) where T : UnityEngine.Object
    {
        var resourceRequest = Resources.LoadAsync<T>(path);
        return await resourceRequest;
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="path"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static async void LoadSceneAsync(string path, Action<float> action = null)
    {
        var asyncOperation = SceneManager.LoadSceneAsync(path);
        //创建一个进度条
        await asyncOperation.ToUniTask(Progress.Create<float>((p) =>
        {
            //p 为当前进度(0-1)
            //...显示进度条与进度数值
            action?.Invoke(p);
        }));
    }

    /// <summary>
    ////异步加载网络图片
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async UniTask<Sprite> LoadSpriteFormWebAsync(string url)
    {
        var webRequest = UnityWebRequestTexture.GetTexture(url);
        var operation = await webRequest.SendWebRequest();
        if (!string.IsNullOrEmpty(operation.error))
        {
            Debug.Log(operation.error);
            return null;
        }
        var texture = ((DownloadHandlerTexture)operation.downloadHandler).texture;
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }


    /// <summary>
    ////异步移动物体 不需要在monobehaviour下
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static async UniTask Move(Transform obj)
    {
        float moveTime = 3;
        while (moveTime > 0)
        {
            moveTime -= Time.deltaTime;
            obj.Translate(Vector3.right * 0.01f, Space.World);
            await UniTask.NextFrame();
        }
    }
}
