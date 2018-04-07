using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextChangeScript : MonoBehaviour {

    public enum TEXT_DATA_TYPE
    {
        LEVEL, 
        COINS, 
        TIME
    }

    Text uiText;
    [SerializeField]
    public TEXT_DATA_TYPE dataType; 
	// Use this for initialization
	void Start () {
        uiText = GetComponent<Text>();
        //uiText.text = ""; 
	}
	
	// Update is called once per frame
	void Update () {
		switch(dataType)
        {
            case TEXT_DATA_TYPE.TIME:
                if(uiText.text != GameManager.Instance.TimerString)
                    uiText.text = GameManager.Instance.TimerString; 
                break;
            case TEXT_DATA_TYPE.COINS:
                if (uiText.text != GameManager.Instance.NumCoins.ToString())
                    uiText.text = GameManager.Instance.NumCoins.ToString(); 
                break;
            case TEXT_DATA_TYPE.LEVEL:
                string str = GameManager.Instance.Level.ToString() + "'s";
                if (uiText.text != str)
                    uiText.text = str;
                break; 
        }
	}
}
