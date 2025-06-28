using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class cheat : MonoBehaviour
{

    private Text coin_count;

    // Start is called before the first frame update
    void Start()
    {
        coin_count = GameObject.Find("coincount").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cheating()
    {
        coin_count.text = "10000";
        PlayerPrefs.SetInt("money", 10000);
        for (int i = 1; i < 22; i++)
        {
            PlayerPrefs.SetInt((i).ToString(), 10);
        }
        for (int i = 1; i < 32; i++)
        {
            PlayerPrefs.SetInt((100 + i).ToString(), 5);
            PlayerPrefs.SetInt((200 + i).ToString(), 1);
        }
    }
}
