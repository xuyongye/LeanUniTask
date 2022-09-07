using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks.Linq;
using System;
using TMPro;
using UnityEngine.Events;
using Cysharp.Threading.Tasks.Triggers;

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
    // public Text tempText;
    // private async UniTaskVoid StandardRun()
    // {
    //     // 切换到多线程进行操作
    //     await UniTask.RunOnThreadPool(() =>
    //     {
    //         Debug.Log("处于多线程异步中");
    //         try
    //         {
    //             //这段会报错.
    //             tempText.text = "非主线程,不能使用unity 对象";
    //         }
    //         catch (System.Exception)
    //         {
    //             Debug.LogError("非主线程");
    //         }
    //     });
    //     await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
    //     // 切换回主线程
    //     await UniTask.SwitchToMainThread();
    //     tempText.text = "主线程,正常使用unity 对象";
    //     Debug.Log("切换回主线程");
    // }

    // private async UniTaskVoid YieldRun()
    // {
    //     //切换到多线程池
    //     await UniTask.SwitchToThreadPool();
    //     try
    //     {
    //         tempText.text = "非主线程, 不能使用unity 对象";
    //     }
    //     catch (System.Exception)
    //     {
    //         Debug.LogError("非主线程");
    //     }
    //     await UniTask.Delay(System.TimeSpan.FromSeconds(2f));
    //     //切换回主线程了
    // {
    //     // StandardRun().Forget();

    //     YieldRun().Forget();
    // }

    #endregion

    #region  异步迭代器

    // public Button CubeBtn;
    // private void Start()
    // {
    //     //点击cube
    //     CheckCubeClick(CubeBtn.GetCancellationTokenOnDestroy()).Forget();
    //     //双击
    //     CheckDoubleClick(doubleClickBtn, doubleClickBtn.GetCancellationTokenOnDestroy()).Forget();
    //     //点击冷却    
    //     CheckCooldownClickButton(coolDownBtn, coolDownBtn.GetCancellationTokenOnDestroy()).Forget();
    //     //初始化Slier数据
    //     InitSlider();
    // }

    // /// <summary>
    // /// 点击立方体的事件 
    // /// </summary>
    // /// <param name="token"></param>
    // /// <returns></returns>
    // private async UniTaskVoid CheckCubeClick(CancellationToken token)
    // {
    //     //创建一个按钮的迭代器
    //     IUniTaskAsyncEnumerable<AsyncUnit> asyncEnumerable = CubeBtn.OnClickAsAsyncEnumerable();
    //     //取前三个点击进行迭代判断
    //     await asyncEnumerable.Take(3).ForEachAsync((_/*  没有用的参数 */, index) =>
    //     {
    //         //如果取消,则返回
    //         if (token.IsCancellationRequested) return;
    //         //第一次点击
    //         if (index == 0)
    //             Debug.Log("点击了矩形一次");
    //         if (index == 1)
    //             Debug.Log("点击了第二次");
    //         if (index == 2)
    //             Debug.Log("点击了第三次");
    //     }, token);

    //     //取出的3次点击都处理了以后进行到这步(在最后一次迭代完成的同时进行到这步)
    //     Destroy(CubeBtn.gameObject);
    // }

    // public Button doubleClickBtn;

    // private async UniTaskVoid CheckDoubleClick(Button doubleClick, CancellationToken token)
    // {
    //     //一直检测的按钮 如果项目中需要注意退出循环
    //     while (true)
    //     {
    //         Debug.Log("等待双击按钮点击!");
    //         //监听第一次按键的事件
    //         var clickAsync = doubleClick.OnClickAsync(token);
    //         await clickAsync;
    //         Debug.Log("按钮第一次点击了");
    //         //监听第二次按钮的事件
    //         var secondClickAsync = doubleClick.OnClickAsync(token);
    //         int resultIndex = await UniTask.WhenAny(UniTask.Delay(TimeSpan.FromSeconds(3))/* 双击超时事件 */,
    //         secondClickAsync/* 第二次按钮按下了 */);
    //         if (resultIndex == 1)
    //         {
    //             _currentHP.Value -= 10;//测试滑动条数值监听的
    //             Debug.Log("双击了");
    //         }
    //         if (resultIndex == 0)
    //         {
    //             _currentHP.Value = 100;//测试滑动条数值监听的
    //             Debug.Log("超时了");
    //         }
    //     }
    // }

    // public Button coolDownBtn;

    // private async UniTaskVoid CheckCooldownClickButton(Button coolDownBtn, CancellationToken token)
    // {
    //     var asyncEnumerable = coolDownBtn.OnClickAsAsyncEnumerable();
    //     await asyncEnumerable.ForEachAwaitAsync(async (_) =>
    //     {
    //         Debug.Log("触发, 进入冷却CD");
    //         //等待CD时间
    //         await UniTask.Delay(TimeSpan.FromSeconds(3), cancellationToken: token);
    //         Debug.Log("冷却完成");
    //     }, token);
    // }

    // ////////////////////////////////////Slider 更新(数值转化为异步迭代器)////////////////////////////////////////////////////////////////
    // public Slider slider;
    // public TextMeshProUGUI sliderNum;
    // int maxHP = 100;
    // /// <summary>
    // /// 异步的数值监听
    // /// </summary>
    // AsyncReactiveProperty<int> _currentHP;
    // /// <summary>
    // /// 用于取消的token
    // /// </summary>
    // CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    // CancellationTokenSource _linkedTokenSource;
    // public void InitSlider()
    // {
    //     //初始化slider 的数值
    //     slider.value = slider.maxValue = maxHP;
    //     //异步的数值监听.
    //     _currentHP = new AsyncReactiveProperty<int>(maxHP);
    //     //重要:订阅异步数值的监听函数
    //     //只要数值变化就会调用传入的委托函数
    //     _currentHP.Subscribe(OnHpChange);
    //     CheckHpChange(_currentHP).Forget();
    //     CheckFirstTargetValue(_currentHP).Forget();
    //     //绑定数据. 数值直接更新到组件上
    //     _currentHP.BindTo(sliderNum);
    // }

    // private async UniTaskVoid OnHpChange(int hp)
    // {
    //     _cancellationTokenSource.Cancel();//先取消上一次的异步.
    //     _cancellationTokenSource = new CancellationTokenSource();
    //     //创建两个用于取消的token
    //     _linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(_cancellationTokenSource.Token, this.GetCancellationTokenOnDestroy());
    //     //进行数值变化的异步(主要用于数值动画)
    //     await SyncSlider(hp, _linkedTokenSource.Token);
    //     //
    // }

    // private async UniTask SyncSlider(int hp, CancellationToken token)
    // {
    //     float speed = 10f;
    //     while (true)
    //     {
    //         //更改slider的值
    //         slider.value = Mathf.Lerp(slider.value, hp, speed * Time.deltaTime);
    //         //在unity的update线程里面进行更新. 同时如果取消token执行的时候返回取消结果. 也要停止
    //         bool cancel = await UniTask.Yield(PlayerLoopTiming.Update, token).SuppressCancellationThrow();
    //         if (cancel) return;//取消了. 返回
    //         if (Mathf.Abs(slider.value - hp) < 0.01f)
    //         {
    //             slider.value = hp;
    //             Debug.Log("到达目标");
    //             return; //接近目标值了. 也直接返回.
    //         }
    //     }
    // }

    // /// <summary>
    // /// 迭代每次变化的数值.
    // /// </summary>
    // /// <param name="hp"></param>
    // /// <returns></returns>
    // private async UniTaskVoid CheckHpChange(AsyncReactiveProperty<int> hp)
    // {
    //     int hpValue = hp.Value; //保存最初始的值
    //     Debug.Log("初始值为:" + hpValue);
    //     await hp.WithoutCurrent().ForEachAsync((_, index) => //过滤初始化的默认值
    //     // await hp.ForEachAsync((_, index) => //不过滤初始的默认值. index 0 就是初始默认值
    //     {
    //         Debug.Log($"hp第{index}次变化. 变化值{hp.Value - hpValue}");
    //     }, this.GetCancellationTokenOnDestroy());

    //     //疑问????? 这个遍历会一直递增. 效率问题?
    // }


    // /// <summary>
    // /// 首次触发一次的异步.
    // /// </summary>
    // /// <param name="hp"></param>
    // /// <returns></returns>
    // private async UniTaskVoid CheckFirstTargetValue(AsyncReactiveProperty<int> hp)
    // {
    //     //等待数值第一次小于指定数值
    //     await hp.FirstAsync((value) =>
    //     {
    //         return hp < maxHP * 0.5f;
    //     }, this.GetCancellationTokenOnDestroy());
    //     Debug.Log("###数值第一次低于指定数值###");
    // }

    #endregion

    #region 异步的内聚代码块. 将按键 移动 碰撞整合在一起. 比如发射子弹
    [Space(5)]
    public Transform playerRoot;
    public Transform bulletPrefab;
    public Transform firePosition;
    public ControlParams _controlParams;
    public UnityEvent OnFire;
    private PlayerControl _playerControl;
    private FireBullet _fireBullet;

    private void Start()
    {
        _fireBullet = new FireBullet(bulletPrefab, firePosition, _controlParams.bulletSpeed);
        _playerControl = new PlayerControl(playerRoot, _controlParams);
        _playerControl.Start();
        OnFire.AddListener(_fireBullet.OnFire);
        _playerControl.OnFire = OnFire;
    }
    #endregion
}

//----------------------异步内聚代码使用的类, 直接在此定义------------------------------------------------------------------------------
[Serializable]
public struct ControlParams
{
    [Header("旋转速度")] public float rotateSpeed;
    [Header("移动速度")] public float moveSpeed;
    [Header("开枪间隔")] public float fireInterval;
    //子弹移动速度也放这里
    [Header("子弹速度")] public float bulletSpeed;
}
/// <summary>
/// 玩家控制类
/// </summary>
public class PlayerControl
{
    private Transform _playerRoot;
    private ControlParams _controlParams;
    public UnityEvent OnFire;

    private float _lastFireTime = Time.time;

    public PlayerControl(Transform playerRoot, ControlParams controlParams)
    {
        _playerRoot = playerRoot;
        _controlParams = controlParams;
    }

    public void Start()
    {
        StartCheckInput();
    }
    //启动输入检测
    private void StartCheckInput()
    {
        CheckPlayerInput().ForEachAsync((delta) =>
        {
            //根据返回的控制变量进行处理位移,旋转和开火.
            _playerRoot.position += delta.movePos;
            _playerRoot.forward =
             Quaternion.AngleAxis(delta.horizontalRotateAng, Vector3.up) * _playerRoot.forward +
             Quaternion.AngleAxis(delta.verticalRotateAng, Vector3.left) * _playerRoot.forward;

            if (delta.fireTime - _lastFireTime > _controlParams.fireInterval)
            {
                //开火
                OnFire?.Invoke();
                _lastFireTime = delta.fireTime;
            }
        }, _playerRoot.GetCancellationTokenOnDestroy()).Forget();
    }

    /// <summary>
    ///  用于创建一个保存了输入的元素组(Vector3 移动数据, float 旋转角度, float 开枪时间)
    /// 的异步迭代器. 它的作用是 脱离 MonoBehavior 的update 按键检测模式
    /// </summary>
    private IUniTaskAsyncEnumerable<(Vector3 movePos, float horizontalRotateAng, float verticalRotateAng, float fireTime)> CheckPlayerInput()
    {
        //创建的异步迭代器 需要一个异步函数. 通过writer 就是写入保存的 泛型T的内容;
        return UniTaskAsyncEnumerable.Create<(Vector3, float, float, float)>(async (writer, token) =>
        {
            while (!token.IsCancellationRequested) // 如果没有取消
            {
                // 移动数据             //旋转角度              //开火时间
                await writer.YieldAsync((GetInputMoveValue(), GetInputAxisValue(true), GetInputAxisValue(false), GetFiredTime()));
                await UniTask.Yield();
            }
        });
    }

    private Vector3 GetInputMoveValue()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        //主角的前方乘于鼠标上下移动量  + 主角右方乘于横向鼠标横向移动量 
        // 最后乘于移动速度
        Vector3 move = (_playerRoot.forward * vertical + _playerRoot.right * horizontal) *
                        (_controlParams.moveSpeed * Time.deltaTime);
        return move;
    }

    private float GetInputAxisValue(bool isX)
    {
        float result = default;
        if (!Input.GetMouseButton(1)) return result; //如果没有按下鼠标右键, 就不进行旋转( 返回 0)
        if (isX)
            result = Input.GetAxis("Mouse X") * _controlParams.rotateSpeed;
        else
            result = Input.GetAxis("Mouse Y") * _controlParams.rotateSpeed;
        // return result;
        return Mathf.Clamp(result, -90, 90);
    }

    private float GetFiredTime()
    {
        if (Input.GetMouseButtonDown(0))
            return Time.time;
        return 0;
    }
}


public class FireBullet
{
    private Transform _bulletPrefab;
    private Transform _firePoint;
    private float _moveSpeed;

    public FireBullet(Transform bulletPrefab, Transform firePoint, float moveSpeed)
    {
        _bulletPrefab = bulletPrefab;
        _firePoint = firePoint;
        _moveSpeed = moveSpeed;
    }

    public void OnFire()
    {
        UniTask.UnityAction(OnClickFire).Invoke();//异步这样调用会更快? 作者推荐的.
    }

    /// <summary>
    /// 子弹的主要内聚逻辑. 生成 飞行 碰撞. 销魂都在一起.与unity自己的分开处理完全不一样.
    /// 因为异步处理. 所以也不需要类是MonoBehavior
    /// </summary>
    private async UniTaskVoid OnClickFire()
    {
        //生成
        Transform bullet = GameObject.Instantiate(_bulletPrefab);
        bullet.position = _firePoint.position;
        bullet.forward = _firePoint.forward;

        //飞出去
        BulletMove(bullet, _moveSpeed).Forget();

        CancellationToken bulletToken = bullet.GetCancellationTokenOnDestroy();
        //创建一个用于延迟销毁的Task, 时间到了它还没有碰到目标. 也进行销毁.
        UniTask waitAutoDestroy = UniTask.Delay(TimeSpan.FromSeconds(1.5f), cancellationToken: bulletToken);

        //创建一个拥有碰撞对象的Source 用于后面如果碰到了对象. 保存collision,以及 完成 await
        var source = new UniTaskCompletionSource<Collision>();

        //异步迭代. 获取碰撞物体
        //可以使用 where, take(1), 或者直接FirstAsync来简化 :
        //示例: var conllisionTask = bullet.GetAsyncCollisionEnterTrigger()
        // .FirstAsync((collision)=> collision.collider.name.CompareTo("Target") == 0, bulletToken );
        // 这样就可以使用 conllisionTask 代替source 了
        //头文件 :using Cysharp.Threading.Tasks.Triggers;
        //如果是 Trigger碰撞框 需要更改 Get函数为 trigger的
        bullet.GetAsyncCollisionEnterTrigger().ForEachAsync((collision) =>
        {
            Debug.Log("????" + collision.collider.name);
            if (collision.collider.name.CompareTo("Target") == 0)
            {
                //碰撞到对的物品了.
                source.TrySetResult(collision);
            }
        }, bulletToken).Forget();

        //等待时间到. 或者碰撞到对象了
        int result = await UniTask.WhenAny(waitAutoDestroy, source.Task);
        if (result == 0)
        {
            Debug.Log("延迟时间到. 销毁子弹");
            GameObject.Destroy(bullet.gameObject);
        }
        else if (result == 1)
        {
            Collider collider = source.GetResult(0).collider; // 获取第一个碰撞体
            Debug.Log($"碰到目标:{collider.name}. 进行播放特效. 加分等处理");
            GameObject.Destroy(bullet.gameObject);
        }
        else
        {
            //其他不管什么原因的结束都要销魂子弹
            GameObject.Destroy(bullet.gameObject);
        }
    }

    private async UniTaskVoid BulletMove(Transform bullet, float speed)
    {
        while (true)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, bullet.GetCancellationTokenOnDestroy());
            bullet.Translate(bullet.forward * speed * Time.deltaTime, Space.World);
        }
    }
}