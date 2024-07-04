using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MapPoints : MonoBehaviour
{
    public Transform canvas;
    public Image[] points;
    public Image[] points_green;
    public Image point_yellow;

        // Start is called before the first frame update
        void Start()
        {
            DrawPoints(q.points);
        }

        void DrawPoints(List<QuestPoint> checkPoints)
        {
            Image point;
            int i = 0;
            foreach (QuestPoint chp in checkPoints)
            {
                // 40.91971 и 40.93198 - крайние координаты карты по X
                // 57.75959 и 57.75959 - крайние координаты карты по Y
                double Y = (chp.latitudeValue - 57.75959) / (57.77128 - 57.75959);
                double X = (chp.longitudeValue - 40.91971) / (40.93198 - 40.91971);
                if (i == 0)
                {
                    point = Instantiate(point_yellow, canvas);
                }
                else if (!chp.Completed)
                {
                    point = Instantiate(points[i], canvas);
                }
                else
                {
                    point = Instantiate(points_green[i], canvas);
                }
                double x = canvas.position[0];
                double y = canvas.position[1];
                point.transform.localPosition = new Vector3((float)(1080 * X) - 540, (float)(1920 * Y) - 960, 0);
                i++;
            }
            i = 0;
        }
    }
    public double coordinateX { get; set; }
    public double coordinateY { get; set; }
    public bool complete { get; set; }
}