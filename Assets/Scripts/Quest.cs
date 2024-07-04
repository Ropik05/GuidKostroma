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
        public GameObject VideoPlane;
        public GameObject CollectItem;
        private GameObject GameVideo;
        public static Transform VideoPoss;
        public Text Text;

        GameObject AudioPlayer;
        GameObject GifPlayer; //Нужен ли? мб один объект с одной анимацией а не вот это вот все
        GPSLocation GPS;

        public List<QuestPoint> points;
        public int CurrentPos = 0;
        int GameState = 0;
        bool forceStart = false;
        public static bool QuestR = false;
        public void ForceStart() { forceStart = true; }
        void work()
        {
            //Переход к следующей точке квеста

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
            Button.SetActive(false);
            PlaneMarker.SetActive(false);
            ARRaycastManagerScript = FindObjectOfType<ARRaycastManager>();
            // Здесь надо инициализировать точки квеста в список
            Text.text = "Start";
            ForceStart();
        }

        // Update is called once per frame
        void Update()
        {
           if ( CurrentPos >= points.Count)
            {
                //пока заглушка на конец надо править 
                return;
            }
            // Проверка геолокации
           /* if (GameState == 0)
            {
                if (!(CheckLocation(points[CurrentPos], 0.0001f) || forceStart)) return;
            }*/
            Text.text = "Prishel";
            // поиск плоскостей
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            ARRaycastManagerScript.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

            //пока просто скопировал надо менять с видео на гифку.
            if (hits.Count > 0)
            {
                PlaneMarker.transform.position = hits[0].pose.position;
                PlaneMarker.SetActive(true);
                if (GameState == 0)
                {
                    Button.SetActive(true);
                }
            }

            if (GameState == 1)
            {
                GameVideo = Instantiate(VideoPlane, hits[0].pose.position, ARCamera.transform.rotation);
                GameState = 2;
            }
            VideoPoss = ArCamera.transform;
            TouchCollectItem();

            // Сюда спавн екатерины
            ////
            // Воспроизведение Гифки
            // Воспроизведение Аудио
            // Загадка
            if (QuestR)
            {

            }

            if (GameState == 3)
            {
                CurrentPos++;
                GameState = 0;
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
            GameState = 1;
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
        public string[] FrameSources;

        //Список звуковых дорожек, которые воспроизводятся
        public string AudioSource;

        public string RewardItemModelSource;

        public string Question;
        public string[] Answers = new string[4] {"","1","","" };
        public int CorrectIndex;


    }
}