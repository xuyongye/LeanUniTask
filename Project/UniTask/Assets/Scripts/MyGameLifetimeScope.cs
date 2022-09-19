using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MyGame
{
    public class VContainerTest
    {
        public VContainerTest()
        {
            UnityEngine.Debug.Log("VContainer Init");
        }
        public void Hello()
        {
            UnityEngine.Debug.Log("Hello world");
        }
    }

    public class VContainerTest2
    {
        public VContainerTest2()
        {
            UnityEngine.Debug.Log("VContainer2 Init");
        }
    }

    public class GamePresenter : ITickable, IStartable
    {
        [Inject] VContainerTest _vContainerTest;
        [Inject] Tmp tmp;
        // [Inject] UniTaskTest_mono _uniTaskTest_mono;
        public void Start()
        {
            // _vContainerTest.Hello();
            // Debug.Log(_uniTaskTest_mono._controlParams.bulletSpeed + " okok");

            // tmp.TempHello();
        }
        public void Tick()
        {
            // Debug.Log("Tick");
        }
    }
    public class Tmp
    {
        readonly VContainerTest2 _vContainerTest2;
        public Tmp(VContainerTest2 vContainerTest2, VContainerTest vContainerTest)
        {
            _vContainerTest2 = vContainerTest2;
        }

        [Inject]
        public void TempHello(VContainerTest vContainerTest)
        {
            // vContainerTest.Hello();
        }

    }
    public class MyGameLifetimeScope : LifetimeScope
    {
        // [SerializeField]
        // private UniTaskTest_mono uniTaskTest_mono;
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<VContainerTest>(Lifetime.Singleton);
            builder.Register<VContainerTest2>(Lifetime.Singleton);
            builder.Register<Tmp>(Lifetime.Singleton);
            // //
            builder.RegisterEntryPoint<GamePresenter>(Lifetime.Singleton);
            // builder.RegisterComponent(uniTaskTest_mono);
            // builder.RegisterComponentInHierarchy<UniTaskTest_mono>();
        }
    }
}
