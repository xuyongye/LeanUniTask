using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UniTaskTest_mono : MonoBehaviour
{
    public Text testText;
    public Image testImage;

    public Transform testObj;
    private bool isPass = false;
    async void Start()
    {
        //从resources中读取txt文件
        // var resourceRequest = Resources.LoadAsync<TextAsset>("Test");
        // //这个await 就是UniTask创建的
        // var txt = await resourceRequest as TextAsset;
        // testText.text = txt.text;

        //使用非Mono类加载
        TextAsset txt = (TextAsset)(await UniTaskTest.LoadTestTextAsync<TextAsset>("Test"));
        testText.text = txt.text;

        //加载场景
        // UniTaskTest.LoadSceneAsync("Scenes/TestScene");

        //加载网络图片
        Sprite sprite = await UniTaskTest.LoadSpriteFormWebAsync("https://i0.hdslb.com/bfs/banner/acef18506e8482b9e9277e3e677d9cad98ca6e30.png");
        testImage.sprite = sprite;
        testImage.SetNativeSize();

        //如果需要线程间隔
        //停止N秒
        Debug.Log("等待2秒");
        await UniTask.Delay(System.TimeSpan.FromSeconds(2), ignoreTimeScale: true);
        //停止N帧
        Debug.Log("等待10帧");
        await UniTask.DelayFrame(10);
        //下一帧开始前
        await UniTask.NextFrame();
        //这帧结束时
        await UniTask.WaitForEndOfFrame();
        //在一帧的某个位置完成wait 
        await UniTask.Yield(PlayerLoopTiming.EarlyUpdate);

        //使用异步移动物体.
        UniTask task = UniTaskTest.Move(testObj);
        await UniTask.WhenAny(task);
        Debug.Log("移动完成");

        //条件等待
        Debug.Log("等待游戏时间到20秒");
        stop = new CancellationTokenSource();

        #region  第一种停止的方式
        // try
        // {
        //     await UniTask.WaitUntil(() => Time.realtimeSinceStartup > 20f, cancellationToken: stop.Token);
        // }
        // catch (System.OperationCanceledException e)
        // {
        //     Debug.Log("等待取消");
        // }
        #endregion

        #region  第二种停止的方式
        bool cancelled = await UniTask.WaitUntil(() => Time.realtimeSinceStartup > 20f, cancellationToken: stop.Token).SuppressCancellationThrow();
        if (cancelled)
            Debug.Log("等待取消");
        #endregion
        //采用链接的方式绑定多个取消. 只要有一个取消了 那么这个link.Token也会取消
        // CancellationTokenSource linkToken = CancellationTokenSource.CreateLinkedTokenSource(stop.Token, ...);

        Debug.Log("等待变量更改");
        await UniTask.WaitUntilValueChanged<UniTaskTest_mono, bool>(this, (obj) => obj.isPass == true);
        Debug.Log("变量已更改");

    }


    CancellationTokenSource stop;
    public void OnClick1()
    {
        stop.Cancel();
        stop.Dispose();
    }

    public void OnClick2()
    {
        isPass = true;
    }
}
