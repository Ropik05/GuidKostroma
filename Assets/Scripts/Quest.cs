﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts
{
    public class Quest : MonoBehaviour
    {
        //

        List<QuestPoint> points;
        int CurrentPos = 0;

        void work()
        {
            VideoPlayer player = GetComponent<VideoPlayer>();
            player.source = new();
            for (; CurrentPos < points.Count; CurrentPos++)
            {
                //Здесь для каждой точки маршрута вызываем проигрыш аудио и воспроизведение анимации персонажа
            }
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
    public class QuestPoint
    {
        //пройдена ли точка
        public bool Completed;
        //Координаты точки по долготе и широте
        public double X;
        public double Y;

        //Список анимаций персонажа. скорее всего пути к ним или хз
        public List<string> FrameSources;

        //Список звуковых дорожек, которые воспроизводятся
        public string AudioSource;

        /// <summary>
        /// Проверяет положение игрока относительно точки квеста
        /// </summary>
        /// <param name="x">Долгота</param>
        /// <param name="y">Широта</param>
        /// <param name="e">Точность</param>
        /// <returns>TRUE если игрок достаточно близко к точке квеста</returns>
        public bool CheckLocation(double x, double y, double e)
        {
            return math.abs(x - X) < e && math.abs(y - Y) < e;
        }
        public void PlayAnimations()
        {

        }
    }
}