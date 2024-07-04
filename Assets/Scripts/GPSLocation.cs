using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GPSLocation : MonoBehaviour
    {
        //Статусы GPS, которые я использую
        // 0 - выключен
        // 1 - работает
        // -1 - Ошибка связи
        // -2 - превышено время ожидания
        public static int GPSStatus;
        public static float latitudeValue;
        public static float longitudeValue;
        public static float altitudeValue;
        public static float horizontalAccuracyValue;
        public static double timestampValue;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(GPSLoc());
        }

        IEnumerator GPSLoc()
        {
            if (!Input.location.isEnabledByUser)
                yield break;

            Input.location.Start();

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (maxWait < 1)
            {
                GPSStatus = -2;
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                GPSStatus = -1;
                yield break;
            }
            else
            {
                GPSStatus = 1;
                InvokeRepeating("UpdateGPSData", 0.5f, 1f);
            }
        }

        // Update is called once per frame
        void UpdateGPSData()
        {
            if (Input.location.status == LocationServiceStatus.Running)
            {
                GPSStatus = 1;
                latitudeValue = Input.location.lastData.latitude;
                longitudeValue = Input.location.lastData.longitude;
                altitudeValue = Input.location.lastData.altitude;
                horizontalAccuracyValue = Input.location.lastData.horizontalAccuracy;
                timestampValue = Input.location.lastData.timestamp;
            }
            else
            {
                GPSStatus = 0;
            }
        }
    }
}
