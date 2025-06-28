using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cauldron_backbutton : MonoBehaviour
{
    private Image original_item;
    private GameObject item;
    private GameObject[] item_copy = new GameObject[22];
    private Text item_count;

    private Animator gotcha_black_animator;
    private Animator gotcha_white_animator;
    private Animator gotcha_shadow_animator;
    private Animator gotcha_potion_animator;
    private Animator gotcha_new_animator;
    private Animator gotcha_potionname_animator;
    private Animator normal_complete_animator;

    private Image gotcha_potion;
    private Text gotcha_potionname;
    private Image normal_complete;
    private Text normal_complete_text;

    private GameObject gotcha_black_object;
    private GameObject gotcha_white_object;
    private GameObject gotcha_shadow_object;
    private GameObject gotcha_potion_object;
    private GameObject gotcha_new_object;
    private GameObject gotcha_potionname_object;
    private GameObject normal_complete_object;


    public void SceneChange()
    {
        for(int i=1;i<22;i++)
        {
            item_count = GameObject.Find("item count " + i.ToString()).GetComponent<Text>();
            PlayerPrefs.SetInt(i.ToString(), int.Parse(item_count.text));
        }
        SceneManager.LoadScene("scene2");
    }

    // Start is called before the first frame update
    void Start()
    {

        for(int i=1;i<22;i++)
        {
            PlayerPrefs.SetInt(i.ToString() + "_in_cauldron", 0);
        }
        PlayerPrefs.SetInt("location", 1);
        for (int i = 1; i < 22; i++)
        {
            original_item = GameObject.Find("original item " + i.ToString()).GetComponent<Image>();
            item_copy[i] = GameObject.Find("item " + i.ToString());
            item = GameObject.Find("item " + i.ToString());
            item_count = GameObject.Find("item count " + i.ToString()).GetComponent<Text>();
            item_count.text = PlayerPrefs.GetInt(i.ToString()).ToString();
            if (item_count.text == "0")
            {
                item.SetActive(false);
                original_item.color = Color.black;
            }
            if(PlayerPrefs.GetInt(i.ToString() + "_in_cauldron")==1)
            {
                original_item.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                item.SetActive(false);
            }
        }

        gotcha_potion = GameObject.Find("gotcha_potion").GetComponent<Image>();
        gotcha_potionname = GameObject.Find("gotcha_potionname").GetComponent<Text>();
        normal_complete = GameObject.Find("normal_complete").GetComponent<Image>();
        normal_complete_text = GameObject.Find("normal_complete_text").GetComponent<Text>();

        gotcha_black_animator = GameObject.Find("gotcha_black").GetComponent<Animator>();
        gotcha_white_animator = GameObject.Find("gotcha_white").GetComponent<Animator>();
        gotcha_shadow_animator = GameObject.Find("gotcha_shadow").GetComponent<Animator>();
        gotcha_potion_animator = GameObject.Find("gotcha_potion").GetComponent<Animator>();
        gotcha_new_animator = GameObject.Find("gotcha_new").GetComponent<Animator>();
        gotcha_potionname_animator = GameObject.Find("gotcha_potionname").GetComponent<Animator>();
        normal_complete_animator = GameObject.Find("normal_complete").GetComponent<Animator>();

        gotcha_black_object = GameObject.Find("gotcha_black");
        gotcha_white_object = GameObject.Find("gotcha_white");
        gotcha_shadow_object = GameObject.Find("gotcha_shadow");
        gotcha_potion_object = GameObject.Find("gotcha_potion");
        gotcha_new_object = GameObject.Find("gotcha_new");
        gotcha_potionname_object = GameObject.Find("gotcha_potionname");
        normal_complete_object = GameObject.Find("normal_complete");

        gotcha_black_object.SetActive(false);
        gotcha_white_object.SetActive(false);
        gotcha_shadow_object.SetActive(false);
        gotcha_potion_object.SetActive(false);
        gotcha_new_object.SetActive(false);
        gotcha_potionname_object.SetActive(false);
        normal_complete_object.SetActive(false);

        SetResolution();
    }

    public void SetResolution()
    {
        int setWidth = 1920;
        int setHeight = 1080;

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);
        if((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)
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

    public void cooking()
    {

        bool[] temp = new bool[22];
        int count=0;
        int temp2;
        temp[0] = false;

        for(int i=1;i<22;i++)
        {
            if (PlayerPrefs.GetInt(i.ToString() + "_in_cauldron") == 1)
            {
                temp[i] = true;
                count++;
            }
            else temp[i] = false;
        }
        if(count==0)
        {
            temp[0] = true;
        }
        if(count==2)
        {
            if(temp[1]&&temp[7])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("101") + 1;
                PlayerPrefs.SetInt("101", temp2);
                if (PlayerPrefs.GetInt("201")==0)
                {
                    PlayerPrefs.SetInt("201", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/회복", typeof(Sprite));
                    gotcha_potionname.text = "회복포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/회복", typeof(Sprite));
                    normal_complete_text.text = "회복포션 완성!!";
                    start_showing();
                }
            }
            if (temp[2] && temp[4])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("103") + 1;
                PlayerPrefs.SetInt("103", temp2);
                if (PlayerPrefs.GetInt("203") == 0)
                {
                    PlayerPrefs.SetInt("203", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/방어력강화", typeof(Sprite));
                    gotcha_potionname.text = "방어력강화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/방어력강화", typeof(Sprite));
                    normal_complete_text.text = "방어력강화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[4] && temp[6])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("107") + 1;
                PlayerPrefs.SetInt("107", temp2);
                if (PlayerPrefs.GetInt("207") == 0)
                {
                    PlayerPrefs.SetInt("207", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/냉기저항", typeof(Sprite));
                    gotcha_potionname.text = "냉기저항 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/냉기저항", typeof(Sprite));
                    normal_complete_text.text = "냉기저항 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[8] && temp[15])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("115") + 1;
                PlayerPrefs.SetInt("115", temp2);
                if (PlayerPrefs.GetInt("215") == 0)
                {
                    PlayerPrefs.SetInt("215", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/맛있는포션", typeof(Sprite));
                    gotcha_potionname.text = "맛있는 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/맛있는포션", typeof(Sprite));
                    normal_complete_text.text = "맛있는 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[16] && temp[17])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("116") + 1;
                PlayerPrefs.SetInt("116", temp2);
                if (PlayerPrefs.GetInt("216") == 0)
                {
                    PlayerPrefs.SetInt("216", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/머리카락이자라는포션", typeof(Sprite));
                    gotcha_potionname.text = "머리카락이 자라는 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/머리카락이자라는포션", typeof(Sprite));
                    normal_complete_text.text = "머리카락이 자라는 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[5] && temp[16])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("118") + 1;
                PlayerPrefs.SetInt("118", temp2);
                if (PlayerPrefs.GetInt("218") == 0)
                {
                    PlayerPrefs.SetInt("218", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/발광", typeof(Sprite));
                    gotcha_potionname.text = "발광포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/발광", typeof(Sprite));
                    normal_complete_text.text = "발광포션 완성!!";
                    start_showing();
                }
            }
            if (temp[6] && temp[20])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("126") + 1;
                PlayerPrefs.SetInt("126", temp2);
                if (PlayerPrefs.GetInt("226") == 0)
                {
                    PlayerPrefs.SetInt("226", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/APTX4869", typeof(Sprite));
                    gotcha_potionname.text = "APTX4869";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/APTX4869", typeof(Sprite));
                    normal_complete_text.text = "APTX4869 완성!!";
                    start_showing();
                }
            }
            if (temp[4] && temp[12])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("127") + 1;
                PlayerPrefs.SetInt("127", temp2);
                if (PlayerPrefs.GetInt("227") == 0)
                {
                    PlayerPrefs.SetInt("227", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/해독", typeof(Sprite));
                    gotcha_potionname.text = "해독포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/해독", typeof(Sprite));
                    normal_complete_text.text = "해독포션 완성!!";
                    start_showing();
                }
            }
            if (temp[7] && temp[16])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("129") + 1;
                PlayerPrefs.SetInt("129", temp2);
                if (PlayerPrefs.GetInt("229") == 0)
                {
                    PlayerPrefs.SetInt("229", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/긍정", typeof(Sprite));
                    gotcha_potionname.text = "긍정포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/긍정", typeof(Sprite));
                    normal_complete_text.text = "긍정포션 완성!!";
                    start_showing();
                }
            }
        }
        if(count==3)
        {
            if (temp[2] && temp[9] && temp[20])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("102") + 1;
                PlayerPrefs.SetInt("102", temp2);
                if (PlayerPrefs.GetInt("202") == 0)
                {
                    PlayerPrefs.SetInt("202", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/신속", typeof(Sprite));
                    gotcha_potionname.text = "신속포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/신속", typeof(Sprite));
                    normal_complete_text.text = "신속포션 완성!!";
                    start_showing();
                }
            }
            if (temp[8] && temp[10] && temp[17])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("104") + 1;
                PlayerPrefs.SetInt("104", temp2);
                if (PlayerPrefs.GetInt("204") == 0)
                {
                    PlayerPrefs.SetInt("204", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/힘강화", typeof(Sprite));
                    gotcha_potionname.text = "힘 강화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/힘강화", typeof(Sprite));
                    normal_complete_text.text = "힘 강화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[11] && temp[12] && temp[18])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("105") + 1;
                PlayerPrefs.SetInt("105", temp2);
                if (PlayerPrefs.GetInt("205") == 0)
                {
                    PlayerPrefs.SetInt("205", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/야간투시", typeof(Sprite));
                    gotcha_potionname.text = "야간 투시 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/야간투시", typeof(Sprite));
                    normal_complete_text.text = "야간 투시 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[3] && temp[13] && temp[16])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("106") + 1;
                PlayerPrefs.SetInt("106", temp2);
                if (PlayerPrefs.GetInt("206") == 0)
                {
                    PlayerPrefs.SetInt("206", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/투명화", typeof(Sprite));
                    gotcha_potionname.text = "투명화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/투명화", typeof(Sprite));
                    normal_complete_text.text = "투명화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[2] && temp[7] && temp[8])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("108") + 1;
                PlayerPrefs.SetInt("108", temp2);
                if (PlayerPrefs.GetInt("208") == 0)
                {
                    PlayerPrefs.SetInt("208", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/화염저항", typeof(Sprite));
                    gotcha_potionname.text = "화염저항 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/화염저항", typeof(Sprite));
                    normal_complete_text.text = "화염저항 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[6] && temp[19] && temp[20])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("110") + 1;
                PlayerPrefs.SetInt("110", temp2);
                if (PlayerPrefs.GetInt("210") == 0)
                {
                    PlayerPrefs.SetInt("210", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/소인화", typeof(Sprite));
                    gotcha_potionname.text = "소인화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/소인화", typeof(Sprite));
                    normal_complete_text.text = "소인화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[5] && temp[12] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("111") + 1;
                PlayerPrefs.SetInt("111", temp2);
                if (PlayerPrefs.GetInt("211") == 0)
                {
                    PlayerPrefs.SetInt("211", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/성전환", typeof(Sprite));
                    gotcha_potionname.text = "성전환 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/성전환", typeof(Sprite));
                    normal_complete_text.text = "성전환 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[3] && temp[13] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("113") + 1;
                PlayerPrefs.SetInt("113", temp2);
                if (PlayerPrefs.GetInt("213") == 0)
                {
                    PlayerPrefs.SetInt("213", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/최음제", typeof(Sprite));
                    gotcha_potionname.text = "최음제";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/최음제", typeof(Sprite));
                    normal_complete_text.text = "최음제 완성!!";
                    start_showing();
                }
            }
            if (temp[3] && temp[14] && temp[20])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("119") + 1;
                PlayerPrefs.SetInt("119", temp2);
                if (PlayerPrefs.GetInt("219") == 0)
                {
                    PlayerPrefs.SetInt("219", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/자백제", typeof(Sprite));
                    gotcha_potionname.text = "자백제";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/자백제", typeof(Sprite));
                    normal_complete_text.text = "자백제 완성!!";
                    start_showing();
                }
            }
            if (temp[1] && temp[11] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("120") + 1;
                PlayerPrefs.SetInt("120", temp2);
                if (PlayerPrefs.GetInt("220") == 0)
                {
                    PlayerPrefs.SetInt("220", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/조커", typeof(Sprite));
                    gotcha_potionname.text = "조커 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/조커", typeof(Sprite));
                    normal_complete_text.text = "조커 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[2] && temp[7] && temp[8])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("121") + 1;
                PlayerPrefs.SetInt("121", temp2);
                if (PlayerPrefs.GetInt("221") == 0)
                {
                    PlayerPrefs.SetInt("221", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/마력회복", typeof(Sprite));
                    gotcha_potionname.text = "마력 회복 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/마력회복", typeof(Sprite));
                    normal_complete_text.text = "마력 회복 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[1] && temp[3] && temp[12])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("123") + 1;
                PlayerPrefs.SetInt("123", temp2);
                if (PlayerPrefs.GetInt("223") == 0)
                {
                    PlayerPrefs.SetInt("223", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/발음이정확해지는포션", typeof(Sprite));
                    gotcha_potionname.text = "발음이 정확해지는 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/발음이정확해지는포션", typeof(Sprite));
                    normal_complete_text.text = "발음이 정확해지는 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[8] && temp[16] && temp[18])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("124") + 1;
                PlayerPrefs.SetInt("124", temp2);
                if (PlayerPrefs.GetInt("224") == 0)
                {
                    PlayerPrefs.SetInt("224", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/마초화", typeof(Sprite));
                    gotcha_potionname.text = "마초화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/마초화", typeof(Sprite));
                    normal_complete_text.text = "마초화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[3] && temp[11] && temp[18])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("125") + 1;
                PlayerPrefs.SetInt("125", temp2);
                if (PlayerPrefs.GetInt("225") == 0)
                {
                    PlayerPrefs.SetInt("225", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/얼굴변환포션", typeof(Sprite));
                    gotcha_potionname.text = "얼굴 변환 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/얼굴변환포션", typeof(Sprite));
                    normal_complete_text.text = "얼굴 변환 포션 완성!!";
                    start_showing();
                }
            }
        }
        if(count==4)
        {
            if (temp[13] && temp[16] && temp[17] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("112") + 1;
                PlayerPrefs.SetInt("112", temp2);
                if (PlayerPrefs.GetInt("212") == 0)
                {
                    PlayerPrefs.SetInt("212", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/정력제", typeof(Sprite));
                    gotcha_potionname.text = "정력제";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/정력제", typeof(Sprite));
                    normal_complete_text.text = "정력제 완성!!";
                    start_showing();
                }
            }
            if (temp[1] && temp[7] && temp[12] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("114") + 1;
                PlayerPrefs.SetInt("114", temp2);
                if (PlayerPrefs.GetInt("214") == 0)
                {
                    PlayerPrefs.SetInt("214", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/인기많아지는포션", typeof(Sprite));
                    gotcha_potionname.text = "인기 많아지는 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/인기많아지는포션", typeof(Sprite));
                    normal_complete_text.text = "인기 많아지는 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[4] && temp[12] && temp[13] && temp[18])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("117") + 1;
                PlayerPrefs.SetInt("117", temp2);
                if (PlayerPrefs.GetInt("217") == 0)
                {
                    PlayerPrefs.SetInt("217", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/투시", typeof(Sprite));
                    gotcha_potionname.text = "투시포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/투시", typeof(Sprite));
                    normal_complete_text.text = "투시포션 완성!!";
                    start_showing();
                }
            }
            if (temp[1] && temp[6] && temp[7] && temp[21])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("130") + 1;
                PlayerPrefs.SetInt("130", temp2);
                if (PlayerPrefs.GetInt("230") == 0)
                {
                    PlayerPrefs.SetInt("230", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/츤데레", typeof(Sprite));
                    gotcha_potionname.text = "츤데레 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/츤데레", typeof(Sprite));
                    normal_complete_text.text = "츤데레 포션 완성!!";
                    start_showing();
                }
            }
        }
        if(count==5)
        {
            if (temp[3] && temp[9] && temp[10] && temp[14] && temp[18])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("109") + 1;
                PlayerPrefs.SetInt("109", temp2);
                if (PlayerPrefs.GetInt("209") == 0)
                {
                    PlayerPrefs.SetInt("209", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/거인화", typeof(Sprite));
                    gotcha_potionname.text = "거인화 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/거인화", typeof(Sprite));
                    normal_complete_text.text = "거인화 포션 완성!!";
                    start_showing();
                }
            }
            if (temp[5] && temp[9] && temp[10] && temp[12] && temp[14])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("122") + 1;
                PlayerPrefs.SetInt("122", temp2);
                if (PlayerPrefs.GetInt("222") == 0)
                {
                    PlayerPrefs.SetInt("222", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/수중호흡", typeof(Sprite));
                    gotcha_potionname.text = "수중 호흡 포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/수중호흡", typeof(Sprite));
                    normal_complete_text.text = "수중 호흡 포션 완성!!";
                    start_showing();
                }
            }
        }
        if(count==20)
        {
            if (!temp[16])
            {
                temp[0] = true;
                temp2 = PlayerPrefs.GetInt("131") + 1;
                PlayerPrefs.SetInt("131", temp2);
                if (PlayerPrefs.GetInt("231") == 0)
                {
                    PlayerPrefs.SetInt("231", 1);
                    gotcha_potion.sprite = (Sprite)Resources.Load("image/cauldron/potions/각성", typeof(Sprite));
                    gotcha_potionname.text = "각성포션";
                    start_gotcha();
                }
                else
                {
                    normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/각성", typeof(Sprite));
                    normal_complete_text.text = "각성포션 완성!!";
                    start_showing();
                }
            }
        }
        if (!temp[0])
        {
            temp2 = PlayerPrefs.GetInt("128") + 1;
            PlayerPrefs.SetInt("128", temp2);
            normal_complete.sprite = (Sprite)Resources.Load("image/cauldron/making_complete/가짜", typeof(Sprite));
            normal_complete_text.text = "가짜포션 완성...";
            start_showing();
        }

        for (int i=1;i<22;i++)
        {
            PlayerPrefs.SetInt(i.ToString() + "_in_cauldron", 0);
        }
    }

    void start_showing()
    {
        normal_complete_object.SetActive(true);

        normal_complete_animator.SetBool("cooking_complete", true);
        Invoke("end_showing", 2.5f);
    }

    void end_showing()
    {
        normal_complete_animator.SetBool("cooking_complete", false);

        normal_complete_object.SetActive(false);

        for (int i = 1; i < 22; i++)
        {
            original_item = GameObject.Find("original item " + i.ToString()).GetComponent<Image>();
            item_count = GameObject.Find("item count " + i.ToString()).GetComponent<Text>();
            original_item.color = new Color32(255, 255, 225, 100);
            item_copy[i].SetActive(true);
            if (item_count.text == "0")
            {
                original_item.color = Color.black;
                item_copy[i].SetActive(false);
            }
            PlayerPrefs.SetInt(i.ToString() + "_in_cauldron", 0);
        }
    }

    void start_gotcha()
    {
        gotcha_black_object.SetActive(true);
        gotcha_white_object.SetActive(true);
        gotcha_shadow_object.SetActive(true);
        gotcha_potion_object.SetActive(true);
        gotcha_new_object.SetActive(true);
        gotcha_potionname_object.SetActive(true);

        gotcha_black_animator.SetBool("gotcha_black", true);
        gotcha_white_animator.SetBool("gotcha_white", true);
        gotcha_shadow_animator.SetBool("gotcha_shadow", true);
        gotcha_potion_animator.SetBool("gotcha_potion", true);
        gotcha_new_animator.SetBool("gotcha_new", true);
        gotcha_potionname_animator.SetBool("gotcha_potionname", true);
        Invoke("end_gotcha", 15f);
    }

    void end_gotcha()
    {
        gotcha_black_animator.SetBool("gotcha_black", false);
        gotcha_white_animator.SetBool("gotcha_white", false);
        gotcha_shadow_animator.SetBool("gotcha_shadow", false);
        gotcha_potion_animator.SetBool("gotcha_potion", false);
        gotcha_new_animator.SetBool("gotcha_new", false);
        gotcha_potionname_animator.SetBool("gotcha_potionname", false);

        gotcha_black_object.SetActive(false);
        gotcha_white_object.SetActive(false);
        gotcha_shadow_object.SetActive(false);
        gotcha_potion_object.SetActive(false);
        gotcha_new_object.SetActive(false);
        gotcha_potionname_object.SetActive(false);

        for (int i = 1; i < 22; i++)
        {
            original_item = GameObject.Find("original item " + i.ToString()).GetComponent<Image>();
            item_count = GameObject.Find("item count " + i.ToString()).GetComponent<Text>();
            original_item.color = new Color32(255, 255, 225, 100);
            item_copy[i].SetActive(true);
            if (item_count.text == "0")
            {
                original_item.color = Color.black;
                item_copy[i].SetActive(false);
            }
            PlayerPrefs.SetInt(i.ToString() + "_in_cauldron", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
