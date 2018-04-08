using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudStates : MonoBehaviour {

    [SerializeField]
    private GameObject[] panels;

    public void ShowHud()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(true);
            foreach (Image i in panel.GetComponentsInChildren<Image>())
            {
                Color color = i.color;
                color.a = 1;
                i.color = color;
            }
        }
    }
}
