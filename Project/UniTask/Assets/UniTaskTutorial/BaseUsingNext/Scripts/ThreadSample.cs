﻿using System;
using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UniTaskTutorial.BaseUsingNext.Scripts
{
    public class ThreadSample : MonoBehaviour
    {
        public Button StandardRun;
        public Button YieldRun;

        public Text Text;
        

        private void Start()
        {
            StandardRun.onClick.AddListener(UniTask.UnityAction(OnClickStandardRun));
            YieldRun.onClick.AddListener(UniTask.UnityAction(OnClickYieldRun));
        }

        private async UniTaskVoid OnClickYieldRun()
        {
            string fileName = Application.dataPath + "/UniTaskTutorial/BaseUsingNext/test.txt";
            await UniTask.SwitchToThreadPool();
            string fileContent = await File.ReadAllTextAsync(fileName);
            await UniTask.Yield(PlayerLoopTiming.Update);
            Text.text = fileContent;


        }

        private async UniTaskVoid OnClickStandardRun()
        {
            int result = 0;
            await UniTask.RunOnThreadPool(() => { result = 1; });
            await UniTask.SwitchToMainThread();
            Text.text = $"计算结束，当前结果是{result}";
        }
    }
}