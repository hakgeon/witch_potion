using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Next_Script : MonoBehaviour
{

    private int potion_cost;
    private int ingredient_cost;
    private int ingredient_cost_discount;

    int script_num;
    int get_item_num;
    private Text script_box;
    private Text coin_box;
    private Image image_potions;
    private SpriteRenderer customer_picture;
    private GameObject Button_Yes;
    private GameObject Button_No;
    private GameObject Scrollview_Potions;
    private GameObject Scrollview_Ingredients;
    private GameObject number_to_sell_box;
    private GameObject number_to_sell_up_button;
    private GameObject number_to_sell_down_button;
    private GameObject number_to_sell_save_button;
    private GameObject customer_picture_object;
    private GameObject script_box_object;
    private GameObject Button_GetOut;
    private Text ingredients_selling_over_text;

    string potion_name;
    int trading_item;

    public int[] potions = new int[32];
    public int[] ingredients = new int[22];

    int number_to_sell_count;
    private Text number_to_sell_textbox;
    bool IsFirst;
    bool[] IsSelling = new bool[22];

    int process;
    float p_seller, p_buyer, p_ctm1, p_ctm2, p_ctm3, p_ctm4;

    // Start is called before the first frame update
    void Start()
    {
        potion_cost = 100;
        ingredient_cost = 10;
        ingredient_cost_discount = 0;

        IsFirst = true;
        Button_Yes = GameObject.Find("Yes");
        Button_No = GameObject.Find("No");
        Scrollview_Potions = GameObject.Find("potions");
        Scrollview_Ingredients = GameObject.Find("ingredients");
        number_to_sell_box = GameObject.Find("number_to_sell");
        number_to_sell_up_button = GameObject.Find("number_to_sell_up");
        number_to_sell_down_button = GameObject.Find("number_to_sell_down");
        number_to_sell_save_button = GameObject.Find("number_to_sell_save");
        Button_GetOut = GameObject.Find("backbutton");
        ingredients_selling_over_text = GameObject.Find("ingredients_selling_over_text").GetComponent<Text>();

        script_box = GameObject.Find("script").GetComponent<Text>();
        coin_box = GameObject.Find("coincount").GetComponent<Text>();

        number_to_sell_textbox = GameObject.Find("real_number_to_sell").GetComponent<Text>();

        for (int i=1;i<22;i++)
        {
            ingredients[i] = PlayerPrefs.GetInt(i.ToString());
        }
        process = 0;
        for (int i = 1; i < 32; i++)
        {
            potions[i] = PlayerPrefs.GetInt((100+i).ToString());
            image_potions = GameObject.Find("potion" + i.ToString()).GetComponent<Image>();
            if (potions[i] <= 0) image_potions.color = Color.black;
            if (PlayerPrefs.GetInt((200 + i).ToString()) == 1) process++;
        }

        Button_Yes.SetActive(false);
        Button_No.SetActive(false);
        Scrollview_Potions.SetActive(false);
        Scrollview_Ingredients.SetActive(false);
        number_to_sell_box.SetActive(false);
        number_to_sell_up_button.SetActive(false);
        number_to_sell_down_button.SetActive(false);
        number_to_sell_save_button.SetActive(false);

        customer_picture = GameObject.Find("customer").GetComponent<SpriteRenderer>();

        customer_picture_object = GameObject.Find("customer");
        script_box_object = GameObject.Find("script_background");

        p_seller = 0.6f - (0.4f * process / 31);
        p_buyer = 0.36f - (0.26f * process / 31) + p_seller;
        p_ctm1 = 0.01f + (0.19f * process / 31);
        p_ctm2 = 0.01f + (0.16f * process / 31);
        p_ctm3 = 0.01f + (0.17f * process / 31);
        p_ctm4 = 0.01f + (0.15f * process / 31);

        if (PlayerPrefs.GetInt("countdown")==0)
        {
            if (PlayerPrefs.GetFloat("waiting1") > 0)
            {
                customer_picture_object.SetActive(true);
                script_box_object.SetActive(true);
                PlayerPrefs.SetFloat("waiting1", 0);
                float temp = Random.Range(0f, 1f);
                if(temp <= p_seller)
                {
                    ingredient_cost_discount = Random.Range(-1, 2) * 10;
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/seller", typeof(Sprite));
                    PlayerPrefs.SetInt("script_num", 34);
                    Take_Next_Script();
                }
                else if(temp <= p_buyer)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/buyer", typeof(Sprite));
                    PlayerPrefs.SetInt("script_num", 1);
                    Take_Next_Script();
                }
                else if(temp <= p_buyer + p_ctm1 * 0.33f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer1", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 1);
                    PlayerPrefs.SetInt("script_num", 40);
                    Take_Next_Script();
                }
                else if(temp <= p_buyer + p_ctm1 * 0.66f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer2", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 2);
                    PlayerPrefs.SetInt("script_num", 40);
                    Take_Next_Script();
                }
                else if(temp <= p_buyer + p_ctm1)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer3", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 3);
                    PlayerPrefs.SetInt("script_num", 40);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2 * 0.33f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer4", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 4);
                    PlayerPrefs.SetInt("script_num", 44);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2 * 0.66f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer5", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 5);
                    PlayerPrefs.SetInt("script_num", 44);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer6", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 6);
                    PlayerPrefs.SetInt("script_num", 44);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2 + p_ctm3 * 0.5f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer7", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 7);
                    PlayerPrefs.SetInt("script_num", 48);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2 + p_ctm3)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer8", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 8);
                    PlayerPrefs.SetInt("script_num", 48);
                    Take_Next_Script();
                }
                else if (temp <= p_buyer + p_ctm1 + p_ctm2 + p_ctm3 + p_ctm4 * 0.5f)
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer9", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 9);
                    PlayerPrefs.SetInt("script_num", 52);
                    Take_Next_Script();
                }
                else
                {
                    customer_picture.sprite = (Sprite)Resources.Load("image/store/customer10", typeof(Sprite));
                    PlayerPrefs.SetInt("customer", 10);
                    PlayerPrefs.SetInt("script_num", 52);
                    Take_Next_Script();
                }
            }
            else
            {
                customer_picture_object.SetActive(false);
                PlayerPrefs.SetInt("script_num", 60);
                Take_Next_Script();
            }
        }
        else
        {
            customer_picture.sprite = (Sprite)Resources.Load("image/store/customer" + PlayerPrefs.GetInt("customer").ToString(), typeof(Sprite));
            if (PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString())>0)
            {
                trading_succeed();
            }
            else
            {
                trading_fail();
            }
        }
        Button_GetOut.SetActive(false);

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

    public void potion1()
    {
        if (potions[1] > 0)
        {
            PlayerPrefs.SetInt("script_num", 2);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 1;
        }
    }
    public void potion2()
    {
        if (potions[2] > 0)
        {
            PlayerPrefs.SetInt("script_num", 3);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 2;
        }
    }
    public void potion3()
    {
        if (potions[3] > 0)
        {
            PlayerPrefs.SetInt("script_num", 4);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 3;
        }
    }
    public void potion4()
    {
        if (potions[4] > 0)
        {
            PlayerPrefs.SetInt("script_num", 5);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 4;
        }
    }
    public void potion5()
    {
        if (potions[5] > 0)
        {
            PlayerPrefs.SetInt("script_num", 6);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 5;
        }
    }
    public void potion6()
    {
        if (potions[6] > 0)
        {
            PlayerPrefs.SetInt("script_num", 7);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 6;
        }
    }
    public void potion7()
    {
        if (potions[7] > 0)
        {
            PlayerPrefs.SetInt("script_num", 8);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 7;
        }
    }
    public void potion8()
    {
        if (potions[8] > 0)
        {
            PlayerPrefs.SetInt("script_num", 9);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 8;
        }
    }
    public void potion9()
    {
        if (potions[9] > 0)
        {
            PlayerPrefs.SetInt("script_num", 10);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 9;
        }
    }
    public void potion10()
    {
        if (potions[10] > 0)
        {
            PlayerPrefs.SetInt("script_num", 11);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 10;
        }
    }
    public void potion11()
    {
        if (potions[11] > 0)
        {
            PlayerPrefs.SetInt("script_num", 12);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 11;
        }
    }
    public void potion12()
    {
        if (potions[12] > 0)
        {
            PlayerPrefs.SetInt("script_num", 13);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 12;
        }
    }
    public void potion13()
    {
        if (potions[13] > 0)
        {
            PlayerPrefs.SetInt("script_num", 14);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 13;
        }
    }
    public void potion14()
    {
        if (potions[14] > 0)
        {
            PlayerPrefs.SetInt("script_num", 15);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 14;
        }
    }
    public void potion15()
    {
        if (potions[15] > 0)
        {
            PlayerPrefs.SetInt("script_num", 16);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 15;
        }
    }
    public void potion16()
    {
        if (potions[16] > 0)
        {
            PlayerPrefs.SetInt("script_num", 17);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 16;
        }
    }
    public void potion17()
    {
        if (potions[17] > 0)
        {
            PlayerPrefs.SetInt("script_num", 18);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 17;
        }
    }
    public void potion18()
    {
        if (potions[18] > 0)
        {
            PlayerPrefs.SetInt("script_num", 19);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 18;
        }
    }
    public void potion19()
    {
        if (potions[19] > 0)
        {
            PlayerPrefs.SetInt("script_num", 20);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 19;
        }
    }
    public void potion20()
    {
        if (potions[20] > 0)
        {
            PlayerPrefs.SetInt("script_num", 21);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 20;
        }
    }
    public void potion21()
    {
        if (potions[21] > 0)
        {
            PlayerPrefs.SetInt("script_num", 22);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 21;
        }
    }
    public void potion22()
    {
        if (potions[22] > 0)
        {
            PlayerPrefs.SetInt("script_num", 23);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 22;
        }
    }
    public void potion23()
    {
        if (potions[23] > 0)
        {
            PlayerPrefs.SetInt("script_num", 24);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 23;
        }
    }
    public void potion24()
    {
        if (potions[24] > 0)
        {
            PlayerPrefs.SetInt("script_num", 25);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 24;
        }
    }
    public void potion25()
    {
        if (potions[25] > 0)
        {
            PlayerPrefs.SetInt("script_num", 26);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 25;
        }
    }
    public void potion26()
    {
        if (potions[26] > 0)
        {
            PlayerPrefs.SetInt("script_num", 27);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 26;
        }
    }
    public void potion27()
    {
        if (potions[27] > 0)
        {
            PlayerPrefs.SetInt("script_num", 28);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 27;
        }
    }
    public void potion28()
    {
        if (potions[28] > 0)
        {
            PlayerPrefs.SetInt("script_num", 29);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 28;
        }
    }
    public void potion29()
    {
        if (potions[29] > 0)
        {
            PlayerPrefs.SetInt("script_num", 30);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 29;
        }
    }
    public void potion30()
    {
        if (potions[30] > 0)
        {
            PlayerPrefs.SetInt("script_num", 31);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 30;
        }
    }
    public void potion31()
    {
        if (potions[31] > 0)
        {
            PlayerPrefs.SetInt("script_num", 32);
            Take_Next_Script();
            Button_Yes.SetActive(true);
            Button_No.SetActive(true);
            Scrollview_Potions.SetActive(false);
            trading_item = 31;
        }
    }

    public void ingredient1()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients1").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 1;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if(get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "드라큘라의 송곳니, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 1);
            if(get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient2()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients2").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 2;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "용의 비늘, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 2);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient3()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients3").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 3;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "자이언트 카멜레온의 혓바닥, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 3);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient4()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients4").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 4;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "눈의 정령의 눈물 결정, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 4);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient5()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients5").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 5;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "심해아귀의 발광촉수, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 5);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient6()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients6").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 6;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "어린왕자의 장미, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 6);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient7()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients7").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 7;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "사랑에 빠진 드래곤의 심장, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 7);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient8()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients8").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 8;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "골든올리브치킨 닭다리, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 8);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient9()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients9").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 9;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "메갈로돈의 지느러미, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 9);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient10()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients10").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 10;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "크라켄의 촉수, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 10);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient11()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients11").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 90 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 11;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "노 라이프 킹의 머리, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 11);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient12()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients12").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 12;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "인어의 눈물, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 12);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
    }
    public void ingredient13()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients13").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 13;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "옷을 녹이는 슬라임의 점액, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 13);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient14()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients14").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 90 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 14;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "대왕조개의 진주, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 14);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient15()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients15").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 90 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 15;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "민트초코, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 15);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient16()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients16").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 110 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 16;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "승리의 성배, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 16);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient17()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients17").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 110 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 17;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "유니콘의 뿔, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 17);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient18()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients18").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 70 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 18;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "사이클롭스의 눈알, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 18);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient19()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients19").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 90 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 19;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "맨드레이크 뿌리, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 19);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient20()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients20").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 50 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 20;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "하급 악마의 날개, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 20);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }
    public void ingredient21()
    {
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            image_potions = GameObject.Find("ingredients21").GetComponent<Image>();
            if (image_potions.color != Color.black)
            {
                ingredient_cost = 90 + ingredient_cost_discount;
                script_box.text = "그건 " + ingredient_cost.ToString() + "원일세.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 21;
            }
            else
            {
                script_box.text = "그건 아직 구하지 못했네만.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "서큐버스의 꼬리, ";
            PlayerPrefs.SetInt("get_item_num" + get_item_num.ToString(), 21);
            if (get_item_num == 3)
            {
                PlayerPrefs.SetInt("script_num", 39);
                Scrollview_Ingredients.SetActive(false);
                Take_Next_Script();
            }
        }
        
    }




    public void click_yes()
    {
        if(PlayerPrefs.GetInt("script_num")>=2&&PlayerPrefs.GetInt("script_num")<=32)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            number_to_sell_box.SetActive(true);
            number_to_sell_up_button.SetActive(true);
            number_to_sell_down_button.SetActive(true);
            number_to_sell_save_button.SetActive(true);
        }
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            number_to_sell_box.SetActive(true);
            number_to_sell_up_button.SetActive(true);
            number_to_sell_down_button.SetActive(true);
            number_to_sell_save_button.SetActive(true);
        }
        if (PlayerPrefs.GetInt("script_num") == 40)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 41);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 44)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 45);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 48)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 49);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 52)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 53);
            Take_Next_Script();
        }
    }

    public void click_no()
    {
        if (PlayerPrefs.GetInt("script_num") >= 2 && PlayerPrefs.GetInt("script_num") <= 32)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 1);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 40)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 42);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 44)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 46);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 48)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 50);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 52)
        {
            Button_Yes.SetActive(false);
            Button_No.SetActive(false);
            PlayerPrefs.SetInt("script_num", 54);
            Take_Next_Script();
        }
    }

    public void number_to_sell_up()
    {
        if (PlayerPrefs.GetInt("script_num") >= 2 && PlayerPrefs.GetInt("script_num") <= 32)
        {
            if (int.Parse(number_to_sell_textbox.text) < PlayerPrefs.GetInt((100+trading_item).ToString()))
            {
                number_to_sell_textbox.text = (int.Parse(number_to_sell_textbox.text) + 1).ToString();
            }
        }
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            if((int.Parse(number_to_sell_textbox.text)+1) * ingredient_cost > int.Parse(coin_box.text))
            {
                script_box.text = "어허, 이런 날강도 아가씨를 봤나. 내가 거래같은 것엔 좀 투철해서 말이오.";
            }
            else
            {
                number_to_sell_textbox.text = (int.Parse(number_to_sell_textbox.text) + 1).ToString();
            }
        }
    }
    public void number_to_sell_down()
    {
        if (int.Parse(number_to_sell_textbox.text) > 0)
        {
            number_to_sell_textbox.text = (int.Parse(number_to_sell_textbox.text) - 1).ToString();
        }
    }

    public void number_to_sell_save()
    {
        if (PlayerPrefs.GetInt("script_num") >= 2 && PlayerPrefs.GetInt("script_num") <= 32)
        {
            potions[trading_item] = potions[trading_item] - int.Parse(number_to_sell_textbox.text);
            coin_box.text = (int.Parse(coin_box.text) + potion_cost * int.Parse(number_to_sell_textbox.text)).ToString();
            number_to_sell_textbox.text = "0";
            PlayerPrefs.SetInt((100 + trading_item).ToString(), potions[trading_item]);
            PlayerPrefs.SetInt("script_num", 1);
            number_to_sell_box.SetActive(false);
            number_to_sell_up_button.SetActive(false);
            number_to_sell_down_button.SetActive(false);
            number_to_sell_save_button.SetActive(false);
            Take_Next_Script();
        }

        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            ingredients[trading_item] = ingredients[trading_item] + int.Parse(number_to_sell_textbox.text);
            coin_box.text = (int.Parse(coin_box.text) - ingredient_cost * int.Parse(number_to_sell_textbox.text)).ToString();
            number_to_sell_textbox.text = "0";
            PlayerPrefs.SetInt(trading_item.ToString(), ingredients[trading_item]);
            number_to_sell_box.SetActive(false);
            number_to_sell_up_button.SetActive(false);
            number_to_sell_down_button.SetActive(false);
            number_to_sell_save_button.SetActive(false);
            Take_Next_Script();
        }

    }

    public void potion_selling_over()
    {
        if (PlayerPrefs.GetInt("script_num") <= 32)
        {
            PlayerPrefs.SetInt("script_num", 33);
            Scrollview_Potions.SetActive(false);
        }
        if (PlayerPrefs.GetInt("script_num") == 34)
        {
            PlayerPrefs.SetInt("script_num", 38);
            Scrollview_Ingredients.SetActive(false);
        }
        Take_Next_Script();
    }

    public void Take_Next_Script()
    {
        script_num = PlayerPrefs.GetInt("script_num");
        switch(script_num)
        {
            case 1:
                script_box.text = "안녕하신가, 좀 있나? 오늘 내가 살 만한 포션들.";
                Scrollview_Potions.SetActive(true);
                for (int i = 1; i < 32; i++)
                {
                    image_potions = GameObject.Find("potion" + i.ToString()).GetComponent<Image>();
                    if (potions[i] <= 0) image_potions.color = Color.black;
                }
                break;
            case 2:
                potion_cost = 110;
                script_box.text = "오호! 회복 포션! 이게 없어서 내 고생 좀 했다우. " + potion_cost.ToString() + "원 정도면 되겠지.";
                break;
            case 3:
                potion_cost = 200;
                script_box.text = "신속 물약! 깃털처럼 빠르게! 내 특별히 " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 4:
                potion_cost = 150;
                script_box.text = "오우! 이건 수요가 많겠군. 사람들이 아픈건 싫어하니까 말이야. 그러니 " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 5:
                potion_cost = 240;
                script_box.text = "이거야말로 필수지. 던전 들어갈 때 말이야. " + potion_cost.ToString() + "원 주지.";
                break;
            case 6:
                potion_cost = 210;
                script_box.text = "오늘같이 달도 뜨지 않은 밤길에 좋지! 이건 특별히 내가 써야겠군. " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 7:
                potion_cost = 220;
                script_box.text = "으흠........빈 포션 아니지? 뭐라고? 안에 든 액체도 투명이라고? 알았다. 일단 " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 8:
                potion_cost = 120;
                script_box.text = "요우! 이건 많이 사두는게 좋겠지. 언젠가 얼어붙은 왕국의 기사들이 돌아올테니 말이야. " + potion_cost.ToString() + "원 주지!";
                break;
            case 9:
                potion_cost = 190;
                script_box.text = "흠! 저 근처에 재버워키를 사냥하려는 무리가 있던데, 비싼 값에 팔 수 있겠군......" + potion_cost.ToString() + "원 주지.";
                break;
            case 10:
                potion_cost = 360;
                script_box.text = "그날, 인류는 떠올렸다. " + potion_cost.ToString() + "원 주겠네.";
                break;
            case 11:
                potion_cost = 210;
                script_box.text = "호오, 자네가 만든 것이 토끼굴에 있었던 원본과 가장 비슷하군! " + potion_cost.ToString() + "원 주겠네!";
                break;
            case 12:
                potion_cost = 200;
                script_box.text = "이런 것도 은근 수요가 많단 말이지." + potion_cost.ToString() + "원 주겠네!";
                break;
            case 13:
                potion_cost = 360;
                script_box.text = "난 필요없네만... " + potion_cost.ToString() + "원 주지.";
                break;
            case 14:
                potion_cost = 200;
                script_box.text = "이런 거 팔면 안된다 했지 않았나! 이번까지만 " + potion_cost.ToString() + "원에 사도록 하지.";
                break;
            case 15:
                potion_cost = 260;
                script_box.text = "나같은 사람한텐 필요 없지만, 또 사가는 사람이 있다니깐." + potion_cost.ToString() + "원에 사도록 하지.";
                break;
            case 16:
                potion_cost = 140;
                script_box.text = "스으읍...다음부턴 원재료를 파는게 어떤가? 일단 " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 17:
                potion_cost = 220;
                script_box.text = "이거야말로 내가 꼭 필요한 거구만! ......뭐? 탈모는 안된다고?" + potion_cost.ToString() + "원 주지.";
                break;
            case 18:
                potion_cost = 250;
                script_box.text = "쓸모가 참 많은 포션이지! 물론 번화가에서 마시고 다니는건 불법이네만......일단 " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 19:
                potion_cost = 160;
                script_box.text = "이걸 마신 사람은 몸에서 빛이 난다고? 할렐루야! " + potion_cost.ToString() + "원 만큼 주지!!";
                break;
            case 20:
                potion_cost = 200;
                script_box.text = "호오. 그 유명한 진실의 올가미와 같은 효과를 볼 수 있다니. " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 21:
                potion_cost = 230;
                script_box.text = "마술 보여줄까? 아, 싫다고? 하핫, 왜 그렇게 진지해? 이 물약이 " + potion_cost.ToString() + "원보단 더 가치 있기를.";
                break;
            case 22:
                potion_cost = 190;
                script_box.text = "혹시 마법을 하루에 단 한 번만 쏠 수 있는 여자애에 대한 이야기 들어봤나? 없다고? 나중에 들려주지. 엄청 재밌을테니. " + potion_cost.ToString() + "원 만큼 주겠네.";
                break;
            case 23:
                potion_cost = 350;
                script_box.text = "플라잉 더치맨 호 탑승권이 관광상품으로 나왔다는거 들었나? 그 배를 타려면 이 수중 호흡 포션이 필수지. " + potion_cost.ToString() + "원 만큼 주겠네.";
                break;
            case 24:
                potion_cost = 150;
                script_box.text = "아나운서들이나 랩퍼들한테 먹이면 딱이겠군! " + potion_cost.ToString() + "원 만큼 주겠네.";
                break;
            case 25:
                potion_cost = 240;
                script_box.text = "오호! 부작용은 없나? 아, 직접 테스트 해보라고......알겠네. " + potion_cost.ToString() + "원 주지.";
                break;
            case 26:
                potion_cost = 210;
                script_box.text = "와우! 성별, 나이 상관없이 전부 랜덤이라고?! 정말 재밌겠군. " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 27:
                potion_cost = 120;
                script_box.text = "음....원래 독약인데 가끔씩 젊어지는 결과가 나타날때도 있다고? 요즘 들어 날 쫓는 탐정인지 뭔지하는 놈한테 써보면 재밌겠군......하핫, 농담이네. " + potion_cost.ToString() + "원 만큼 주지.";
                break;
            case 28:
                potion_cost = 120;
                script_box.text = "시작이 있으면 끝이 있는 법이지. 모두들 깨지 않는 꿈을 꾸고 있을 뿐이야. 하지만 이거면 깨겠지! " + potion_cost.ToString() + "원 주겠네!";
                break;
            case 29:
                potion_cost = 40;
                script_box.text = "가짜가 있기에 진짜가 있는 법. 내 좋은 곳에 쓰도록 하지 ㅎㅎ. " + potion_cost.ToString() + "원 만큼 주겠네.";
                break;
            case 30:
                potion_cost = 160;
                script_box.text = "호오, 직접 만든 우울증 치료젠가? 잘 팔리겠어. " + potion_cost.ToString() + "원 주지.";
                break;
            case 31:
                potion_cost = 270;
                script_box.text = potion_cost.ToString() + "원 주지. 뭐? 평소보다 많이 쳐주는 것 같다고? 아니, 딱히 널 위해서 주는 건 아니고, 자금이 충분해야 양질의 포션들을 만들 수 있겠지......바보 아냐.";
                break;
            case 32:
                potion_cost = 1600;
                script_box.text = "각성엔 승리가 아닌 패배가 필요한 법. 저 각! 성! 했습니다! ";
                break;
            case 33:
                script_box.text = "팔게 없다면 여기서 이만 가도록 하지. 돌아 오겠네. 곧.";
                Invoke("GetOut", 4f);
                break;
            case 34:
                script_box.text = "안녕하시오. 이 중에 사고 싶은 것이 있소?";
                Scrollview_Ingredients.SetActive(true);
                ingredients_selling_over_text.text = "종료";
                if (IsFirst)
                {
                    IsFirst = false;
                    for(int i=1;i<22;i++)
                    {
                        IsSelling[i] = true;
                        if (Random.Range(0, 3) < 2)
                        {
                            if (PlayerPrefs.GetInt("get_item_num1") != i || Random.Range(0, 3) < 2)
                            {
                                if (PlayerPrefs.GetInt("get_item_num2") != i || Random.Range(0, 3) < 2)
                                {
                                    if (PlayerPrefs.GetInt("get_item_num3") != i || Random.Range(0, 3) < 2)
                                    {
                                        IsSelling[i] = false;
                                    }
                                }
                            }
                        }
                    }
                }
                for (int i = 1; i < 22; i++)
                {
                    image_potions = GameObject.Find("ingredients" + i.ToString()).GetComponent<Image>();
                    if (!IsSelling[i]) {
                        image_potions.color = Color.black;
                    }
                }

                break;
            case 35:
                script_box.text = "어허, 이런 날강도 아가씨를 봤나. 내가 거래같은 것엔 좀 투철해서 말이오.";
                break;
            case 36:
                script_box.text = "내가 그 만큼은 없어서. 미안하이.";
                break;
            case 37:
                script_box.text = "이 노인네의 반응을 보고싶은겐가?! 고얀 놈. 노인네를 그렇게 골리면 안되지.";
                break;
            case 38:
                script_box.text = "만약 다음번에 내가 가져왔으면 하는 재료가 있소? 내 최대한 노력해보지요. 3개만 골라보시오.";
                Scrollview_Ingredients.SetActive(true);
                ingredients_selling_over_text.text = "초기화";
                get_item_num = 0;
                for (int i = 1; i < 22; i++)
                {
                    image_potions = GameObject.Find("ingredients" + i.ToString()).GetComponent<Image>();
                    image_potions.color = new Color32(255, 255, 225, 255);
                }
                break;
            case 39:
                script_box.text = "잘 알겠소. 그럼.";
                Invoke("GetOut", 4f);
                break;
            case 40:
                selecting_potion();
                potion_cost *= 2;
                script_box.text = "안녕! 이번엔 " + potion_name + "을 사고싶어서 말이야. " + potion_cost.ToString() + "에 살 수 있을까?";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 41:
                script_box.text = "정말 고마워!";
                if (PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString()) > 0)
                {
                    trading_succeed();
                }
                else
                {
                    PlayerPrefs.SetInt("countdown", 50);
                }
                Invoke("GetOut", 4f);
                break;
            case 42:
                script_box.text = "거래 조건이 맘에 안드는 구나......알겠어!";
                Invoke("GetOut", 4f);
                break;
            case 43:
                script_box.text = "다음에 또 올게!";
                break;
            case 44:
                selecting_potion();
                potion_cost *= 4;
                script_box.text = "아리따운 아가씨, " + potion_name + "을 여기서 판다고 해서 왔는데.....혹시 " + potion_cost.ToString() + "원으로 얻을 수 있을지?";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 45:
                script_box.text = "정말 고맙네. 기다리지.";
                if (PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString()) > 0)
                {
                    trading_succeed();
                }
                else
                {
                    PlayerPrefs.SetInt("countdown", 30);
                }
                Invoke("GetOut", 4f);
                break;
            case 46:
                script_box.text = "인연이 아닌가 보군.";
                Invoke("GetOut", 4f);
                break;
            case 47:
                script_box.text = "그럼 나중에 또 오겠네. 달빛처럼 차가운 눈동자를 가진 소녀여.";
                break;
            case 48:
                selecting_potion();
                potion_cost *= 3;
                script_box.text = "안녕하세요~ 혹시 " + potion_name + "을 좀 " + potion_cost.ToString() + "원으로 얻을 수 있을까요? 급히 쓸데가 생겨서요.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 49:
                script_box.text = "꺼마워요~";
                if (PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString()) > 0)
                {
                    trading_succeed();
                }
                else
                {
                    PlayerPrefs.SetInt("countdown", 40);
                }
                Invoke("GetOut", 4f);
                break;
            case 50:
                script_box.text = "어쩔 수 없죠.";
                Invoke("GetOut", 4f);
                break;
            case 51:
                script_box.text = "또 찾아올게요!";
                break;
            case 52:
                selecting_potion();
                potion_cost *= 5;
                script_box.text = "나 " + potion_name + "이 필요해. 빨리 좀 갖다줘. " + potion_cost.ToString() + "원 줄게.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 53:
                script_box.text = "감사요~";
                if (PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString()) > 0)
                {
                    trading_succeed();
                }
                else
                {
                    PlayerPrefs.SetInt("countdown", 20);
                }
                Invoke("GetOut", 4f);
                break;
            case 54:
                script_box.text = "이런, 생각보다 형편없는 마녀였네.";
                Invoke("GetOut", 4f);
                break;
            case 55:
                script_box.text = "나중에 또 올게. 애송이.";
                break;
            case 56:
                script_box.text = "그렇게 미안해할 필요 없어! 없다면야 어쩔 수 없지. 다음에 또 올게!";
                break;
            case 57:
                script_box.text = "이곳도 아직 없는건가......그럼.";
                break;
            case 58:
                script_box.text = "없다구요? 이곳이 마지막 희망이었는데.......어쩔 수 없죠.";
                break;
            case 59:
                script_box.text = "장사 접어.";
                break;
            case 60:
                script_box.text = "그러나 아무도 없었다.";
                Invoke("GetOut", 4f);
                break;
        }
    }

    void selecting_potion()
    {
        int temp;
        temp = Random.Range(0, 31);
        switch(temp)
        {
            case 0:
                potion_name = "회복포션";
                potion_cost = 110;
                PlayerPrefs.SetInt("trading_potion", 1);
                break;
            case 1:
                potion_name = "신속포션";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 2);
                break;
            case 2:
                potion_name = "방어력강화 포션";
                potion_cost = 150;
                PlayerPrefs.SetInt("trading_potion", 3);
                break;
            case 3:
                potion_name = "힘 강화 포션";
                potion_cost = 240;
                PlayerPrefs.SetInt("trading_potion", 4);
                break;
            case 4:
                potion_name = "야간투시 포션";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 5);
                break;
            case 5:
                potion_name = "투명화 포션";
                potion_cost = 220;
                PlayerPrefs.SetInt("trading_potion", 6);
                break;
            case 6:
                potion_name = "냉기저항 포션";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 7);
                break;
            case 7:
                potion_name = "화염저항 포션";
                potion_cost = 190;
                PlayerPrefs.SetInt("trading_potion", 8);
                break;
            case 8:
                potion_name = "거인화 포션";
                potion_cost = 360;
                PlayerPrefs.SetInt("trading_potion", 9);
                break;
            case 9:
                potion_name = "소인화 포션";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 10);
                break;
            case 10:
                potion_name = "성전환 포션";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 11);
                break;
            case 11:
                potion_name = "정력제";
                potion_cost = 360;
                PlayerPrefs.SetInt("trading_potion", 12);
                break;
            case 12:
                potion_name = "최음제";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 13);
                break;
            case 13:
                potion_name = "인기가 많아지는 포션";
                potion_cost = 260;
                PlayerPrefs.SetInt("trading_potion", 14);
                break;
            case 14:
                potion_name = "맛있는 포션";
                potion_cost = 140;
                PlayerPrefs.SetInt("trading_potion", 15);
                break;
            case 15:
                potion_name = "머리카락이 자라는 포션";
                potion_cost = 220;
                PlayerPrefs.SetInt("trading_potion", 16);
                break;
            case 16:
                potion_name = "투시포션";
                potion_cost = 250;
                PlayerPrefs.SetInt("trading_potion", 17);
                break;
            case 17:
                potion_name = "발광포션";
                potion_cost = 160;
                PlayerPrefs.SetInt("trading_potion", 18);
                break;
            case 18:
                potion_name = "자백제";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 19);
                break;
            case 19:
                potion_name = "조커포션";
                potion_cost = 230;
                PlayerPrefs.SetInt("trading_potion", 20);
                break;
            case 20:
                potion_name = "마력회복 포션";
                potion_cost = 190;
                PlayerPrefs.SetInt("trading_potion", 21);
                break;
            case 21:
                potion_name = "수중호흡 포션";
                potion_cost = 350;
                PlayerPrefs.SetInt("trading_potion", 22);
                break;
            case 22:
                potion_name = "발음이 정확해지는 포션";
                potion_cost = 150;
                PlayerPrefs.SetInt("trading_potion", 23);
                break;
            case 23:
                potion_name = "마초화 포션";
                potion_cost = 240;
                PlayerPrefs.SetInt("trading_potion", 24);
                break;
            case 24:
                potion_name = "얼굴 변환 포션";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 25);
                break;
            case 25:
                potion_name = "APTX4869";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 26);
                break;
            case 26:
                potion_name = "해독포션";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 27);
                break;
            case 27:
                potion_name = "가짜포션";
                potion_cost = 40;
                PlayerPrefs.SetInt("trading_potion", 28);
                break;
            case 28:
                potion_name = "긍정포션";
                potion_cost = 160;
                PlayerPrefs.SetInt("trading_potion", 29);
                break;
            case 29:
                potion_name = "츤데레 포션";
                potion_cost = 270;
                PlayerPrefs.SetInt("trading_potion", 30);
                break;
            case 30:
                potion_name = "각성 포션";
                potion_cost = 1600;
                PlayerPrefs.SetInt("trading_potion", 31);
                break;
        }
        
    }

    void trading_succeed()
    {
        PlayerPrefs.SetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString(), PlayerPrefs.GetInt((100 + PlayerPrefs.GetInt("trading_potion")).ToString()) - 1);
        switch (PlayerPrefs.GetInt("trading_potion"))
        {
            case 1:
                potion_cost = 110;
                break;
            case 2:
                potion_cost = 200;
                break;
            case 3:
                potion_cost = 150;
                break;
            case 4:
                potion_cost = 240;
                break;
            case 5:
                potion_cost = 210;
                break;
            case 6:
                potion_cost = 220;
                break;
            case 7:
                potion_cost = 120;
                break;
            case 8:
                potion_cost = 190;
                break;
            case 9:
                potion_cost = 360;
                break;
            case 10:
                potion_cost = 210;
                break;
            case 11:
                potion_cost = 200;
                break;
            case 12:
                potion_cost = 360;
                break;
            case 13:
                potion_cost = 200;
                break;
            case 14:
                potion_cost = 260;
                break;
            case 15:
                potion_cost = 140;
                break;
            case 16:
                potion_cost = 220;
                break;
            case 17:
                potion_cost = 250;
                break;
            case 18:
                potion_cost = 160;
                break;
            case 19:
                potion_cost = 200;
                break;
            case 20:
                potion_cost = 230;
                break;
            case 21:
                potion_cost = 190;
                break;
            case 22:
                potion_cost = 350;
                break;
            case 23:
                potion_cost = 150;
                break;
            case 24:
                potion_cost = 240;
                break;
            case 25:
                potion_cost = 210;
                break;
            case 26:
                potion_cost = 120;
                break;
            case 27:
                potion_cost = 120;
                break;
            case 28:
                potion_cost = 40;
                break;
            case 29:
                potion_cost = 160;
                break;
            case 30:
                potion_cost = 270;
                break;
            case 31:
                potion_cost = 1600;
                break;

        }

        PlayerPrefs.SetInt("countdown", 0);
        PlayerPrefs.SetFloat("waiting1", 0);
        Invoke("GetOut", 4f);
        if (PlayerPrefs.GetInt("script_num") == 41)
        {
            potion_cost *= 2;
            PlayerPrefs.SetInt("script_num", 43);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 45)
        {
            potion_cost *= 4;
            PlayerPrefs.SetInt("script_num", 47);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 49)
        {
            potion_cost *= 3;
            PlayerPrefs.SetInt("script_num", 51);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 53)
        {
            potion_cost *= 5;
            PlayerPrefs.SetInt("script_num", 55);
            Take_Next_Script();
        }
        coin_box.text = (int.Parse(coin_box.text) + potion_cost).ToString();
    }

    void trading_fail()
    {
        PlayerPrefs.SetInt("countdown", 0);
        PlayerPrefs.SetFloat("waiting1", 0);
        Invoke("GetOut", 4f);
        if (PlayerPrefs.GetInt("script_num") == 41)
        {
            PlayerPrefs.SetInt("script_num", 56);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 45)
        {
            PlayerPrefs.SetInt("script_num", 57);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 49)
        {
            PlayerPrefs.SetInt("script_num", 58);
            Take_Next_Script();
        }
        if (PlayerPrefs.GetInt("script_num") == 53)
        {
            PlayerPrefs.SetInt("script_num", 59);
            Take_Next_Script();
        }
    }

    public void GetOut()
    {
        SceneManager.LoadScene("scene2");
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("money", int.Parse(coin_box.text));
    }
}


