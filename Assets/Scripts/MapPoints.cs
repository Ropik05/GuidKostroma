using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class MapPoints : MonoBehaviour
    {
        public Transform canvas;
        public Image point_green;
        public Image point_yellow;
        Image[] drawedpoints;
        bool d = true;
        // Start is called before the first frame update
        private void Update()
        {
            if (drawedpoints == null) drawedpoints = new Image[Quest.points.Count];
                DrawPoints(Quest.points);
        }
        void DrawPoints(List<QuestPoint> chp)
        {
            for (int i = 0; i <drawedpoints.Length; i++)
            {
                // 40.91971 и 40.93198 - крайние координаты карты по X
                // 57.75959 и 57.75959 - крайние координаты карты по Y

                double Y = (chp[i].latitudeValue - 57.75959) / (57.77128 - 57.75959);
                double X = (chp[i].longitudeValue - 40.91971) / (40.93198 - 40.91971);

                if(drawedpoints[i] == null)
                    drawedpoints[i] = Instantiate((chp[i].Completed ? point_green.CloneViaFakeSerialization() : point_yellow.CloneViaFakeSerialization()), canvas);
                else
                    drawedpoints[i].sprite = chp[i].Completed ? point_green.sprite : point_yellow.sprite;
                double x = canvas.position[0];
                double y = canvas.position[1];
                drawedpoints[i].transform.localPosition = new Vector3((float)(1080 * X) - 540, (float)(1920 * Y) - 960, 0);
                if (!chp[i].Completed) break;
            }
            d = false;
        }
    }
}