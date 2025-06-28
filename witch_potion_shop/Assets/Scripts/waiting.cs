using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class waiting : MonoBehaviour
{

    private Image human1;
    private GameObject human;
    public float waiting_sec1;
    public Text countdown_box;
    private float played_time;

    // Start is called before the first frame update
    void Start()
    {
        human1 = GameObject.Find("human1").GetComponent<Image>();
        human = GameObject.Find("human1");
        countdown_box = GameObject.Find("countdown_box").GetComponent<Text>();
        if(PlayerPrefs.GetInt("countdown")==0)
        {
            waiting_sec1 = PlayerPrefs.GetFloat("waiting1");
            if (waiting_sec1 > 0)
            {
                human1.color = Color.white;
                human1.CrossFadeAlpha(1,0,true);
            }
            else human1.CrossFadeAlpha(0,0,true);
            played_time = 0;
            countdown_box.text = "";
        }
        else
        {
            human1.CrossFadeAlpha(0,0,true);
            countdown_box.text = PlayerPrefs.GetInt("countdown").ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        played_time += Time.deltaTime;
        if (PlayerPrefs.GetInt("countdown") == 0)
        {
            if (waiting_sec1 > 0) waiting_sec1 += Time.deltaTime;
            if (waiting_sec1 > 30)
            {
                human1.color = Color.red;
            }
            if (waiting_sec1 > 40)
            {
                human1.CrossFadeAlpha(0, 0, true);
                waiting_sec1 = -0.01f;
            }
            if (waiting_sec1 <= 0)
            {
                waiting_sec1 += -Time.deltaTime;
                if (waiting_sec1 < -20)
                {
                    waiting_sec1 = 0;
                    if (Random.Range(0, 3) > 0)
                    {
                        waiting_sec1 = 0.01f;
                        human1.CrossFadeAlpha(1, 0, true);
                        human1.color = Color.white;
                    }
                }
            }
        }
        else
        {
            countdown_box.text = (PlayerPrefs.GetInt("countdown") - (int)played_time).ToString();
            if(int.Parse(countdown_box.text)<=0)
            {
                waiting_sec1 = 0;
                human1.CrossFadeAlpha(0, 0, true);
                countdown_box.text = "";
                PlayerPrefs.SetInt("countdown", 0);
            }
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("waiting1", waiting_sec1);
        if(countdown_box.text != "")
        {
            PlayerPrefs.SetInt("countdown", int.Parse(countdown_box.text));
        }   
    }
}
