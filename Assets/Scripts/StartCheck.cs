using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCheck : MonoBehaviour
{
    public void CheckOn()
        //��� ����� �������� ����������
    {
        DollControl.GameMode++;
    }
    public void StartSceen()
    {
        DollControl.GameMode=0;
    }
}
