using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActive : MonoBehaviour
{
    public void Toggle(GameObject obj)
    {
        if (obj != null)
            obj.SetActive(!obj.activeSelf);
    }
    public void abv(int i) { }
}
