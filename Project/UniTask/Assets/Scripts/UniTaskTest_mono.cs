using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UniTaskTest_mono : MonoBehaviour
{
    public Text testText;
    public Image testImage;

    public Transform testObj;
    private bool isPass = false;

    #region 一般使用测试
    // async void Start()
    // {
    //     //从resources中读取txt文件
    //     // var resourceRequest = Resources.LoadAsync<TextAsset>("Test");
    //     // //这个await 就是UniTask创建的
    //     // var txt = await resourceRequest as TextAsset;
    //     // testText.text = txt.text;

    //     //使用非Mono类加载
    //     TextAsset txt = (TextAsset)(await UniTaskTest.LoadTestTextAsync<TextAsset>("Test"));
    //     testText.text = txt.text;

    //     //加载场景
    //     // UniTaskTest.LoadSceneAsync("Scenes/TestScene");

    //     //加载网络图片
    //     Sprite sprite = await UniTaskTest.LoadSpriteFormWebAsync("https://i0.hdslb.com/bfs/banner/acef18506e8482b9e9277e3e677d9cad98ca6e30.png");
    //     testImage.sprite = sprite;
    //     testImage.SetNativeSize();

    //     //如果需要线程间隔
    //     //停止N秒
    //     Debug.Log("等待2秒");
    //     await UniTask.Delay(System.TimeSpan.FromSeconds(2), ignoreTimeScale: true);
    //     //停止N帧
    //     Debug.Log("等待10帧");
    //     await UniTask.DelayFrame(10);
    //     //下一帧开始前
    //     await UniTask.NextFrame();
    //     //这帧结束时
    //     await UniTask.WaitForEndOfFrame();
    //     //在一帧的某个位置完成wait 
    //     await UniTask.Yield(PlayerLoopTiming.EarlyUpdate);

    //     //使用异步移动物体.
    //     UniTask task = UniTaskTest.Move(testObj);
    //     await UniTask.WhenAny(task);
    //     Debug.Log("移动完成");

    //     //条件等待
    //     Debug.Log("等待游戏时间到20秒");
    //     stop = new CancellationTokenSource();

    //     #region  第一种停止的方式
    //     // try
    //     // {
    //     //     await UniTask.WaitUntil(() => Time.realtimeSinceStartup > 20f, cancellationToken: stop.Token);
    //     // }
    //     // catch (System.OperationCanceledException e)
    //     // {
    //     //     Debug.Log("等待取消");
    //     // }
    //     #endregion

    //     #region  第二种停止的方式
    //     bool cancelled = await UniTask.WaitUntil(() => Time.realtimeSinceStartup > 20f, cancellationToken: stop.Token).SuppressCancellationThrow();
    //     if (cancelled)
    //         Debug.Log("等待取消");
    //     #endregion
    //     //采用链接的方式绑定多个取消. 只要有一个取消了 那么这个link.Token也会取消
    //     // CancellationTokenSource linkToken = CancellationTokenSource.CreateLinkedTokenSource(stop.Token, ...);

    //     Debug.Log("等待变量更改");
    //     await UniTask.WaitUntilValueChanged<UniTaskTest_mono, bool>(this, (obj) => obj.isPass == true);
    //     Debug.Log("变量已更改");

    // }


    // CancellationTokenSource stop;
    // public void OnClick1()
    // {
    //     stop.Cancel();
    //     stop.Dispose();
    // }

    // public void OnClick2()
    // {
    //     isPass = true;
    // }

    #endregion

    #region  网络请求测试
    // {
    //     var cts = new CancellationTokenSource();
    //     //设置多久以后取消线程. 代表超时操作
    //     cts.CancelAfterSlim(System.TimeSpan.FromSeconds(timeout));
    //     (bool isCancelOrFailed, UnityWebRequest result)/*  元数组获取返回值 */ = await UnityWebRequest.Get(url). //
    //     SendWebRequest().
    //     WithCancellation(cts.Token).//添加一个用于取消的token. 前面已经设置好. 5秒以后进行取消.代表超时
    //     SuppressCancellationThrow();//取消以后抛出一个异常返回.(bool isCancell, unityWabResult result)

    //     if (result == null) return "Error";
    //     if (!result.isDone) return result.error;
    //     if (!isCancelOrFailed) return result.downloadHandler.text.Substring(0, 200);//太长了. 截取一小段返回就行了

    //     //5秒取消了
    //     return "TimeOut";
    // }

    // public async void TestWeb()
    // {
    //     // testText.text = await GetRequest("https://www.baidu.com/");
    //     // testText.text = await GetRequest("https://www.google.com/seach?q=");
    //     testText.text = await GetRequest("https://www.google.com/");
    //     Debug.Log("测试结束");
    // }

    // private void Start()
    // {
    //     TestWeb();
    //     Debug.Log("start End");
    // }
    #endregion

    #region 回调测试

    // public Transform moveTarget;
    // private async void Start()
    // {
    //     //创建一个用于两个异步关联的source  : 可以重复使用
    //     //使用示例. 等待比如某个物体移动到某处
    //     var source = new UniTaskCompletionSource();
    //     Move(moveTarget, 5, () => Debug.Log("callback"), source).Forget();
    //     //等待另外的地方调用为 TrySetResult() 会继续执行后面的语句
    //     await source.Task;
    //     Debug.Log("startEnd");
    // }

    // private async UniTaskVoid Move(Transform tf, float moveTime, System.Action callback, UniTaskCompletionSource source)
    // {
    //     float startTime = Time.time;
    //     float intervalTime = Time.time - startTime;
    //     while (intervalTime <= moveTime)
    //     {
    //         intervalTime = Time.time - startTime;

    //         if (intervalTime >= moveTime * 0.5f)
    //         {
    //             //回调
    //             callback?.Invoke();
    //             //成功 继续执行外部使用 await source.Task 的代码
    //             source.TrySetResult();
    //             //失败 会阻断外层的await 后面的执行代码
    //             // source.TrySetException(new System.Exception());
    //             //取消 会阻断外层的await 后面的执行代码
    //             // source.TrySetCanceled();

    //             Debug.Log("half complete");
    //             break; // 跳出整个异步循环
    //         }
    //         if (tf != null)
    //             tf.Translate(tf.forward * Time.deltaTime);
    //         /// 如果mono摧毁. 就会取消这个异步 (实际上会报错. 待验证)
    //         await UniTask.Yield(this.GetCancellationTokenOnDestroy());
    //     }
    //     Debug.Log("async end");
    // }

    // public void OnDeleteObj()
    // {
    //     Destroy(this.gameObject);
    // }
    #endregion

    #region  真正的多线程异步
    public Text tempText;
    private async UniTaskVoid StandardRun()
    {
        // 切换到多线程进行操作
        await UniTask.RunOnThreadPool(() =>
        {
            Debug.Log("处于多线程异步中");
            try
            {
                //这段会报错.
                tempText.text = "非主线程,不能使用unity 对象";
            }
            catch (System.Exception)
            {
                Debug.LogError("非主线程");
            }
        });
        await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
        // 切换回主线程
        await UniTask.SwitchToMainThread();
        tempText.text = "主线程,正常使用unity 对象";
        Debug.Log("切换回主线程");
    }

    private async UniTaskVoid YieldRun()
    {
        //切换到多线程池
        await UniTask.SwitchToThreadPool();
        try
        {
            tempText.text = "非主线程, 不能使用unity 对象";
        }
        catch (System.Exception)
        {
            Debug.LogError("非主线程");
        }
        await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
        //切换回主线程了
        await UniTask.Yield(PlayerLoopTiming.Update);
        tempText.text = "主线程, 正常使用unity 对象";
    }

    private void Start()
    {
        // StandardRun().Forget();
        YieldRun().Forget();
    }

    #endregion
}
