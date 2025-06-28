using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recipe_close : MonoBehaviour
{
    public void Close()
    {
        SceneManager.LoadScene("scene2");
    }
    public void Next1()
    {
        SceneManager.LoadScene("Recipe 2");
    }
    public void Next2()
    {
        SceneManager.LoadScene("Recipe 3");
    }
    public void Next3()
    {
        SceneManager.LoadScene("Recipe 4");
    }
    public void Next4()
    {
        SceneManager.LoadScene("Recipe 5");
    }
    public void Next5()
    {
        SceneManager.LoadScene("Recipe 6");
    }
    public void Next6()
    {
        SceneManager.LoadScene("Recipe 7");
    }
    public void Next7()
    {
        SceneManager.LoadScene("Recipe 8");
    }
    public void Next8()
    {
        SceneManager.LoadScene("Recipe 9");
    }
    public void Next9()
    {
        SceneManager.LoadScene("Recipe 10");
    }
    public void Next10()
    {
        SceneManager.LoadScene("Recipe 11");
    }
    public void Next11()
    {
        SceneManager.LoadScene("Recipe 12");
    }
    public void Next12()
    {
        SceneManager.LoadScene("Recipe 13");
    }
    public void Next13()
    {
        SceneManager.LoadScene("Recipe 14");
    }
    public void Next14()
    {
        SceneManager.LoadScene("Recipe 15");
    }
    public void Back2()
    {
        SceneManager.LoadScene("Recipe 1");
    }
    public void Back3()
    {
        SceneManager.LoadScene("Recipe 2");
    }
    public void Back4()
    {
        SceneManager.LoadScene("Recipe 3");
    }
    public void Back5()
    {
        SceneManager.LoadScene("Recipe 4");
    }
    public void Back6()
    {
        SceneManager.LoadScene("Recipe 5");
    }
    public void Back7()
    {
        SceneManager.LoadScene("Recipe 6");
    }
    public void Back8()
    {
        SceneManager.LoadScene("Recipe 7");
    }
    public void Back9()
    {
        SceneManager.LoadScene("Recipe 8");
    }
    public void Back10()
    {
        SceneManager.LoadScene("Recipe 9");
    }
    public void Back11()
    {
        SceneManager.LoadScene("Recipe 10");
    }
    public void Back12()
    {
        SceneManager.LoadScene("Recipe 11");
    }
    public void Back13()
    {
        SceneManager.LoadScene("Recipe 12");
    }
    public void Back14()
    {
        SceneManager.LoadScene("Recipe 13");
    }
    public void Back15()
    {
        SceneManager.LoadScene("Recipe 14");
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("location", 3);

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
