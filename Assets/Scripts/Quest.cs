using System.Collections;
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
        GameObject AudioPlayer;
        GameObject GifPlayer; //Нужен ли? мб один объект с одной анимацией а не вот это вот все

        #region Координаты и карта
        // координаты наших мест
        //[57.76462547805589, 40.92026989198701]
        //[57.76685013594274, 40.92462403838931]
        //[57.76749226711287, 40.92440946166813]
        //[40.9258495721882, 57.768562096370054]
        //[40.92690099812203, 57.76790851789714]
        //[57.77031426849492, 40.93166896010112]
        //[57.76181661202863, 40.92877026474288]
        GPSLocation GPS;
        MapPoints map;
        public List<QuestPoint> points;

        #endregion
        public int CurrentPos = 0;
        int GameState = 0;
        bool forceStart = false;
        public void ForceStart() { forceStart = true; }
        void pointend()
        {
            //Переход к следующей точке квеста
            points[CurrentPos].Completed = true;
            CurrentPos++;
            GameState = 0;
        }

        public bool CheckLocation(QuestPoint p, float e)
        {
            //Если gps не работает - возвращаем истину, чтоб квест работал. Статусы есть, но мне впадлу писать это сейчас
            if (GPS.GPSStatus != 1) return true;

            return math.abs(p.latitudeValue - GPS.latitudeValue) < e && math.abs(p.longitudeValue - GPS.longitudeValue) < e;
        }


        //Use this for initialization

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            //пока просто скопировал надо менять с видео на гифку.
            if (hits.Count > 0)
            {
                PlaneMarker.transform.position = hits[0].pose.position;
                PlaneMarker.SetActive(true);
            }

            if (GameState == 1)
            {
                GameVideo = Instantiate(VideoPlane, hits[0].pose.position, ArCamera.transform.rotation);
                GameState = 2;
            }
            VideoPoss = ARCamera.transform;
            TouchCollectItem();

            // Сюда спавн екатерины
            // Воспроизведение Гифки
            // Воспроизведение Аудио
            // Загадка

        }
        void TouchCollectItem()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                TouchPosition = touch.position;
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = ArCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        if (hitObject.collider.CompareTag("UnTouch"))
                        {
                            hitObject.collider.gameObject.tag = "Touch";
                        }
                    }
                }
            }
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