using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public GameObject btn;

    public void New()
    {
        PlayerPrefs.SetInt("con", 1);
        PlayerPrefs.SetInt("money", 2000);
        for (int i = 1; i <= 21; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        for (int i = 101; i <= 131; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        for (int i = 201; i <= 231; i++)
        {
            PlayerPrefs.SetInt(i.ToString(), 0);
        }
        PlayerPrefs.SetInt("location", 0);
        SceneManager.LoadScene("scene2");
    }
    public void Continue()
    {
        SceneManager.LoadScene("scene2");
    }
    // Start is called before the first frame update
    void Start()
    {

        btn = GameObject.Find("button_continue");
        if (PlayerPrefs.GetInt("con") == 1)
        {
            btn.SetActive(true);
        }
        else
        {
            btn.SetActive(false);
        }

        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight);
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);
        }
        else
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight);
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
