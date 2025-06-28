using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class store_backbutton : MonoBehaviour
{
    // Start is called before the first frame update
    private Text coin_count;

    void Start()
    {
        coin_count = GameObject.Find("coincount").GetComponent<Text>();
        coin_count.text = PlayerPrefs.GetInt("money").ToString();
        PlayerPrefs.SetInt("location", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene("scene2");
    }
}
