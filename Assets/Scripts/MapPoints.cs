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
        // координаты наших мест
    //[57.76462547805589, 40.92026989198701]
    //[57.76685013594274, 40.92462403838931]
    //[57.76749226711287, 40.92440946166813]
    //[40.9258495721882, 57.768562096370054]
    //[40.92690099812203, 57.76790851789714]
    //[57.77031426849492, 40.93166896010112]
    //[57.76181661202863, 40.92877026474288]

        CheckPoint checkPoint1 = new CheckPoint(57.76463, 40.92027, true);
        CheckPoint checkPoint2 = new CheckPoint(57.76685, 40.92462, true);
        CheckPoint checkPoint3 = new CheckPoint(57.76749, 40.92441, true);
        CheckPoint checkPoint4 = new CheckPoint(57.76856, 40.92585, false);
        CheckPoint checkPoint5 = new CheckPoint(57.76801, 40.92699, false);
        CheckPoint checkPoint6 = new CheckPoint(57.77031, 40.93167, false);
        CheckPoint checkPoint7 = new CheckPoint(57.76182, 40.92877, false);

        Queue<CheckPoint> checkPoints = new Queue<CheckPoint>();
        checkPoints.Enqueue(checkPoint1);
        checkPoints.Enqueue(checkPoint2);
        checkPoints.Enqueue(checkPoint3);
        checkPoints.Enqueue(checkPoint4);
        checkPoints.Enqueue(checkPoint5);
        checkPoints.Enqueue(checkPoint6);
        checkPoints.Enqueue(checkPoint7);

        DrawPoints(checkPoints);
    }

    void DrawPoints(Queue<CheckPoint> checkPoints)
    {
        Image point;
        int i = 0;
        foreach (CheckPoint chp in checkPoints)
        {
            // 40.91971 и 40.93198 - крайние координаты карты по X
            // 57.75959 и 57.75959 - крайние координаты карты по Y
            double Y = (chp.coordinateY - 57.75959) / (57.77128 - 57.75959);
            double X = (chp.coordinateX - 40.91971) / (40.93198 - 40.91971);
            if (i == 0)
            {
                point = Instantiate(point_yellow, canvas);
            }
            else if (!chp.complete)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

// заглушка для класса события
public class CheckPoint
{
    public CheckPoint(double y, double x, bool b)
    {
        coordinateX = x;
        coordinateY = y;
        complete = b;
    }
    public double coordinateX { get; set; }
    public double coordinateY { get; set; }
    public bool complete { get; set; }
}