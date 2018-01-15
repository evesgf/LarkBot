﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Console
{
    /// <summary>
    /// 帧率计算器
    /// </summary>
    public class FPSCounter
    {
        // 帧率计算频率
        private const float calcRate = 0.5f;
        // 本次计算频率下帧数
        private int frameCount = 0;
        // 频率时长
        private float rateDuration = 0f;
        // 显示帧率
        private int fps = 0;

        public FPSCounter(Console console)
        {
            console.onUpdateCallback += Update;
            console.onGUICallback += OnGUI;
        }

        void Start()
        {
            this.frameCount = 0;
            this.rateDuration = 0f;
            this.fps = 0;
        }

        void Update()
        {
            ++this.frameCount;
            this.rateDuration += Time.deltaTime;
            if (this.rateDuration > calcRate)
            {
                // 计算帧率
                this.fps = (int)(this.frameCount / this.rateDuration);
                this.frameCount = 0;
                this.rateDuration = 0f;
            }
        }

        void OnGUI()
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(80, 20, 120, 20), "fps:" + this.fps.ToString());
        }
    }
}
