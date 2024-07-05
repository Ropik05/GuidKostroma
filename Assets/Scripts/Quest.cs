using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

namespace Assets.Scripts
{
    public class Quest : MonoBehaviour
    {
        public GameObject ARCamera;
        [SerializeField] private Camera ArCamera;
        public GameObject Button;
        private ARRaycastManager ARRaycastManagerScript;
        public GameObject PlaneMarker;
        private Vector2 TouchPosition;
        public GameObject[] VideoPlane;
        public GameObject[] CollectItem;
        private GameObject GameVideo;
        public static Transform VideoPoss;
        public Text Text;

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
        public static List<QuestPoint> points;
        public GameObject imherebtn;
        #endregion
        public static int CurrentPos = 0;
        // Значения:
        // 0 - Начало квеста,проверка геолокации
        // 1 - начало квеста, ищем плоскость
        public static int GameState = 1;
        bool forceStart = false;
        public void ForceStart() 
        {
            forceStart = true;
        }
        void pointend()
        {
            //Переход к следующей точке квеста
            points[CurrentPos].Completed = true;
            forceStart = false;
            CurrentPos++;
            GameState = 0;
            hits.Clear();
            imherebtn.SetActive(true);
        }

        public bool CheckLocation(QuestPoint p, float e)
        {
            //Если gps не работает - возвращаем истину, чтоб квест работал. Статусы есть, но мне впадлу писать это сейчас
            if (GPSLocation.GPSStatus != 1) return true;

            return math.abs(p.latitudeValue - GPSLocation.latitudeValue) < e && math.abs(p.longitudeValue - GPSLocation.longitudeValue) < e;
        }


        //Use this for initialization

        void Start()
        {

            points = new List<QuestPoint>() 
            {
                // координаты наших мест
                new QuestPoint() {latitudeValue = 57.76462547805589f, longitudeValue = 40.92026989198701f, FrameSources = "Ekat1Sceen",RewardItemModelSource="Path1"},
                new QuestPoint() {latitudeValue = 57.76685013594274f, longitudeValue = 40.92462403838931f,FrameSources= "Ekat2Sceen",RewardItemModelSource = "Path2"},
                new QuestPoint() {latitudeValue = 57.76749226711287f, longitudeValue = 40.92440946166813f,FrameSources = "Ekat3Sceen",RewardItemModelSource = "Path3"},
                new QuestPoint() {longitudeValue = 40.9258495721882f, latitudeValue = 57.768562096370054f,FrameSources = "Ekat4Sceen",RewardItemModelSource = "Path4"},
                new QuestPoint() {longitudeValue = 40.92690099812203f, latitudeValue = 57.76790851789714f,FrameSources = "Ekat5Sceen",RewardItemModelSource = "Path5"},
                new QuestPoint() {latitudeValue = 57.77031426849492f, longitudeValue = 40.93166896010112f,FrameSources = "Ekat6Sceen",RewardItemModelSource = "Path6"},
                //new QuestPoint() {latitudeValue = 57.76181661202863f, longitudeValue = 40.92877026474288f}
            };
            Button.SetActive(false);
            PlaneMarker.SetActive(false);
            ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
            ForceStart();
        }

        // Update is called once per frame
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        void Update()
        {
            if ( CurrentPos >= points.Count)
            {
                //пока заглушка на конец надо править 
                return;
            }
            switch (GameState)
            {
                case 0: // 0 - Начало квеста,проверка геолокации
                    {
                    if (!(CheckLocation(points[CurrentPos], 0.0001f) || forceStart)) return;
                    GameState++;
                    return;
                }
                case 1: // 1 - ищем плоскость
                    {
                        ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);
                        if (hits.Count > 0)
                        {
                            PlaneMarker.transform.position = hits[0].pose.position;
                            PlaneMarker.SetActive(true);
                            Button.SetActive(true);
                            GameState++;
                        }
                        return;
                    }
                case 2: { return; } // 2 - Ждем пока не нажмут кнопку
                case 3: // 3 - спавним видео, включаем реплику
                    {
                        GameVideo = Instantiate(VideoPlane[CurrentPos], hits[0].pose.position, ARCamera.transform.rotation) as GameObject;
                        GameState++;
                        return; 
                    }
                case 4: { return; }
                case 5: // 4 - загадка
                    {
                        return;
                    }
                case 6: // 5 - сбор предмета
                    {
                        Instantiate(CollectItem[CurrentPos], hits[0].pose.position, ARCamera.transform.rotation);
                        GameState++;
                        return;
                    }
                case 7:
                    {
                        TouchCollectItem();
                        return; }
                case 8: // 6 - переход к следующей точке
                    {
                        pointend();
                        return;
                    }
                default:
                    break;
            }
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
        public void StartQuest()
        {
            GameState = 3;
        }
    }
    public class QuestPoint
    {
        //пройдена ли точка
        public bool Completed;
        //Координаты точки по долготе и широте
        public float latitudeValue;
        public float longitudeValue;

        //Список анимаций персонажа. скорее всего пути к ним или хз
        public string FrameSources;
        //Список собираемых частей
        public string RewardItemModelSource;

        public string Question;
        public string[] Answers = new string[4] {"","1","","" };
        public int CorrectIndex;


    }
}