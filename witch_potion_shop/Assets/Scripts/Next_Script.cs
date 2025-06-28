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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 1;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if(get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���ŧ���� �۰���, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 2;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���� ���, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 3;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���̾�Ʈ ī�᷹���� ���ٴ�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 4;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���� ������ ���� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 5;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���ؾƱ��� �߱��˼�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 6;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "������� ���, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 7;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "����� ���� �巡���� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 8;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���ø���ġŲ �ߴٸ�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 9;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�ް��ε��� ��������, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 10;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "ũ������ �˼�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 11;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�� ������ ŷ�� �Ӹ�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 12;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�ξ��� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 13;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "���� ���̴� �������� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 14;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "��������� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 15;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "��Ʈ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 16;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�¸��� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 17;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�������� ��, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 18;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "����Ŭ�ӽ��� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 19;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�ǵ巹��ũ �Ѹ�, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 20;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "�ϱ� �Ǹ��� ����, ";
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
                script_box.text = "�װ� " + ingredient_cost.ToString() + "���ϼ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                Scrollview_Ingredients.SetActive(false);
                trading_item = 21;
            }
            else
            {
                script_box.text = "�װ� ���� ������ ���߳׸�.";
            }
        }
        else
        {
            if (get_item_num == 0)
            {
                script_box.text = "";
            }
            get_item_num++;
            script_box.text += "��ť������ ����, ";
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
                script_box.text = "����, �̷� ������ �ư����� �ó�. ���� �ŷ����� �Ϳ� �� ��ö�ؼ� ���̿�.";
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
                script_box.text = "�ȳ��ϽŰ�, �� �ֳ�? ���� ���� �� ���� ���ǵ�.";
                Scrollview_Potions.SetActive(true);
                for (int i = 1; i < 32; i++)
                {
                    image_potions = GameObject.Find("potion" + i.ToString()).GetComponent<Image>();
                    if (potions[i] <= 0) image_potions.color = Color.black;
                }
                break;
            case 2:
                potion_cost = 110;
                script_box.text = "��ȣ! ȸ�� ����! �̰� ��� �� ��� �� �ߴٿ�. " + potion_cost.ToString() + "�� ������ �ǰ���.";
                break;
            case 3:
                potion_cost = 200;
                script_box.text = "�ż� ����! ����ó�� ������! �� Ư���� " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 4:
                potion_cost = 150;
                script_box.text = "����! �̰� ���䰡 ���ڱ�. ������� ���°� �Ⱦ��ϴϱ� ���̾�. �׷��� " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 5:
                potion_cost = 240;
                script_box.text = "�̰ž߸��� �ʼ���. ���� �� �� ���̾�. " + potion_cost.ToString() + "�� ����.";
                break;
            case 6:
                potion_cost = 210;
                script_box.text = "���ð��� �޵� ���� ���� ��濡 ����! �̰� Ư���� ���� ��߰ڱ�. " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 7:
                potion_cost = 220;
                script_box.text = "����........�� ���� �ƴ���? �����? �ȿ� �� ��ü�� �����̶��? �˾Ҵ�. �ϴ� " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 8:
                potion_cost = 120;
                script_box.text = "���! �̰� ���� ��δ°� ������. ������ ������ �ձ��� ������ ���ƿ��״� ���̾�. " + potion_cost.ToString() + "�� ����!";
                break;
            case 9:
                potion_cost = 190;
                script_box.text = "��! �� ��ó�� �����Ű�� ����Ϸ��� ������ �ִ���, ��� ���� �� �� �ְڱ�......" + potion_cost.ToString() + "�� ����.";
                break;
            case 10:
                potion_cost = 360;
                script_box.text = "�׳�, �η��� ���÷ȴ�. " + potion_cost.ToString() + "�� �ְڳ�.";
                break;
            case 11:
                potion_cost = 210;
                script_box.text = "ȣ��, �ڳװ� ���� ���� �䳢���� �־��� ������ ���� ����ϱ�! " + potion_cost.ToString() + "�� �ְڳ�!";
                break;
            case 12:
                potion_cost = 200;
                script_box.text = "�̷� �͵� ���� ���䰡 ���� ������." + potion_cost.ToString() + "�� �ְڳ�!";
                break;
            case 13:
                potion_cost = 360;
                script_box.text = "�� �ʿ���׸�... " + potion_cost.ToString() + "�� ����.";
                break;
            case 14:
                potion_cost = 200;
                script_box.text = "�̷� �� �ȸ� �ȵȴ� ���� �ʾҳ�! �̹������� " + potion_cost.ToString() + "���� �絵�� ����.";
                break;
            case 15:
                potion_cost = 260;
                script_box.text = "������ ������� �ʿ� ������, �� �簡�� ����� �ִٴϱ�." + potion_cost.ToString() + "���� �絵�� ����.";
                break;
            case 16:
                potion_cost = 140;
                script_box.text = "������...�������� ����Ḧ �Ĵ°� ���? �ϴ� " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 17:
                potion_cost = 220;
                script_box.text = "�̰ž߸��� ���� �� �ʿ��� �ű���! ......��? Ż��� �ȵȴٰ�?" + potion_cost.ToString() + "�� ����.";
                break;
            case 18:
                potion_cost = 250;
                script_box.text = "���� �� ���� ��������! ���� ��ȭ������ ���ð� �ٴϴ°� �ҹ��̳׸�......�ϴ� " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 19:
                potion_cost = 160;
                script_box.text = "�̰� ���� ����� ������ ���� ���ٰ�? �ҷ����! " + potion_cost.ToString() + "�� ��ŭ ����!!";
                break;
            case 20:
                potion_cost = 200;
                script_box.text = "ȣ��. �� ������ ������ �ð��̿� ���� ȿ���� �� �� �ִٴ�. " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 21:
                potion_cost = 230;
                script_box.text = "���� �����ٱ�? ��, �ȴٰ�? ����, �� �׷��� ������? �� ������ " + potion_cost.ToString() + "������ �� ��ġ �ֱ⸦.";
                break;
            case 22:
                potion_cost = 190;
                script_box.text = "Ȥ�� ������ �Ϸ翡 �� �� ���� �� �� �ִ� ���ھֿ� ���� �̾߱� ���ó�? ���ٰ�? ���߿� �������. ��û ������״�. " + potion_cost.ToString() + "�� ��ŭ �ְڳ�.";
                break;
            case 23:
                potion_cost = 350;
                script_box.text = "�ö��� ��ġ�� ȣ ž�±��� ������ǰ���� ���Դٴ°� �����? �� �踦 Ÿ���� �� ���� ȣ�� ������ �ʼ���. " + potion_cost.ToString() + "�� ��ŭ �ְڳ�.";
                break;
            case 24:
                potion_cost = 150;
                script_box.text = "�Ƴ�����̳� ���۵����� ���̸� ���̰ڱ�! " + potion_cost.ToString() + "�� ��ŭ �ְڳ�.";
                break;
            case 25:
                potion_cost = 240;
                script_box.text = "��ȣ! ���ۿ��� ����? ��, ���� �׽�Ʈ �غ����......�˰ڳ�. " + potion_cost.ToString() + "�� ����.";
                break;
            case 26:
                potion_cost = 210;
                script_box.text = "�Ϳ�! ����, ���� ������� ���� �����̶��?! ���� ��հڱ�. " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 27:
                potion_cost = 120;
                script_box.text = "��....���� �����ε� ������ �������� ����� ��Ÿ������ �ִٰ�? ���� ��� �� �Ѵ� Ž������ �����ϴ� ������ �Ẹ�� ��հڱ�......����, ����̳�. " + potion_cost.ToString() + "�� ��ŭ ����.";
                break;
            case 28:
                potion_cost = 120;
                script_box.text = "������ ������ ���� �ִ� ������. ��ε� ���� �ʴ� ���� �ٰ� ���� ���̾�. ������ �̰Ÿ� ������! " + potion_cost.ToString() + "�� �ְڳ�!";
                break;
            case 29:
                potion_cost = 40;
                script_box.text = "��¥�� �ֱ⿡ ��¥�� �ִ� ��. �� ���� ���� ������ ���� ����. " + potion_cost.ToString() + "�� ��ŭ �ְڳ�.";
                break;
            case 30:
                potion_cost = 160;
                script_box.text = "ȣ��, ���� ���� ����� ġ������? �� �ȸ��ھ�. " + potion_cost.ToString() + "�� ����.";
                break;
            case 31:
                potion_cost = 270;
                script_box.text = potion_cost.ToString() + "�� ����. ��? ��Һ��� ���� ���ִ� �� ���ٰ�? �ƴ�, ���� �� ���ؼ� �ִ� �� �ƴϰ�, �ڱ��� ����ؾ� ������ ���ǵ��� ���� �� �ְ���......�ٺ� �Ƴ�.";
                break;
            case 32:
                potion_cost = 1600;
                script_box.text = "������ �¸��� �ƴ� �й谡 �ʿ��� ��. �� ��! ��! �߽��ϴ�! ";
                break;
            case 33:
                script_box.text = "�Ȱ� ���ٸ� ���⼭ �̸� ������ ����. ���� ���ڳ�. ��.";
                Invoke("GetOut", 4f);
                break;
            case 34:
                script_box.text = "�ȳ��Ͻÿ�. �� �߿� ��� ���� ���� �ּ�?";
                Scrollview_Ingredients.SetActive(true);
                ingredients_selling_over_text.text = "����";
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
                script_box.text = "����, �̷� ������ �ư����� �ó�. ���� �ŷ����� �Ϳ� �� ��ö�ؼ� ���̿�.";
                break;
            case 36:
                script_box.text = "���� �� ��ŭ�� ���. �̾�����.";
                break;
            case 37:
                script_box.text = "�� ���γ��� ������ ��������հ�?! ��� ��. ���γ׸� �׷��� �񸮸� �ȵ���.";
                break;
            case 38:
                script_box.text = "���� �������� ���� ���������� �ϴ� ��ᰡ �ּ�? �� �ִ��� ����غ�����. 3���� ��󺸽ÿ�.";
                Scrollview_Ingredients.SetActive(true);
                ingredients_selling_over_text.text = "�ʱ�ȭ";
                get_item_num = 0;
                for (int i = 1; i < 22; i++)
                {
                    image_potions = GameObject.Find("ingredients" + i.ToString()).GetComponent<Image>();
                    image_potions.color = new Color32(255, 255, 225, 255);
                }
                break;
            case 39:
                script_box.text = "�� �˰ڼ�. �׷�.";
                Invoke("GetOut", 4f);
                break;
            case 40:
                selecting_potion();
                potion_cost *= 2;
                script_box.text = "�ȳ�! �̹��� " + potion_name + "�� ���; ���̾�. " + potion_cost.ToString() + "�� �� �� ������?";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 41:
                script_box.text = "���� ����!";
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
                script_box.text = "�ŷ� ������ ���� �ȵ�� ����......�˰ھ�!";
                Invoke("GetOut", 4f);
                break;
            case 43:
                script_box.text = "������ �� �ð�!";
                break;
            case 44:
                selecting_potion();
                potion_cost *= 4;
                script_box.text = "�Ƹ����� �ư���, " + potion_name + "�� ���⼭ �Ǵٰ� �ؼ� �Դµ�.....Ȥ�� " + potion_cost.ToString() + "������ ���� �� ������?";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 45:
                script_box.text = "���� ����. ��ٸ���.";
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
                script_box.text = "�ο��� �ƴѰ� ����.";
                Invoke("GetOut", 4f);
                break;
            case 47:
                script_box.text = "�׷� ���߿� �� ���ڳ�. �޺�ó�� ������ �����ڸ� ���� �ҳ࿩.";
                break;
            case 48:
                selecting_potion();
                potion_cost *= 3;
                script_box.text = "�ȳ��ϼ���~ Ȥ�� " + potion_name + "�� �� " + potion_cost.ToString() + "������ ���� �� �������? ���� ������ ���ܼ���.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 49:
                script_box.text = "��������~";
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
                script_box.text = "��¿ �� ����.";
                Invoke("GetOut", 4f);
                break;
            case 51:
                script_box.text = "�� ã�ƿðԿ�!";
                break;
            case 52:
                selecting_potion();
                potion_cost *= 5;
                script_box.text = "�� " + potion_name + "�� �ʿ���. ���� �� ������. " + potion_cost.ToString() + "�� �ٰ�.";
                Button_Yes.SetActive(true);
                Button_No.SetActive(true);
                break;
            case 53:
                script_box.text = "�����~";
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
                script_box.text = "�̷�, �������� ������� ���࿴��.";
                Invoke("GetOut", 4f);
                break;
            case 55:
                script_box.text = "���߿� �� �ð�. �ּ���.";
                break;
            case 56:
                script_box.text = "�׷��� �̾����� �ʿ� ����! ���ٸ�� ��¿ �� ����. ������ �� �ð�!";
                break;
            case 57:
                script_box.text = "�̰��� ���� ���°ǰ�......�׷�.";
                break;
            case 58:
                script_box.text = "���ٱ���? �̰��� ������ ����̾��µ�.......��¿ �� ����.";
                break;
            case 59:
                script_box.text = "��� ����.";
                break;
            case 60:
                script_box.text = "�׷��� �ƹ��� ������.";
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
                potion_name = "ȸ������";
                potion_cost = 110;
                PlayerPrefs.SetInt("trading_potion", 1);
                break;
            case 1:
                potion_name = "�ż�����";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 2);
                break;
            case 2:
                potion_name = "���°�ȭ ����";
                potion_cost = 150;
                PlayerPrefs.SetInt("trading_potion", 3);
                break;
            case 3:
                potion_name = "�� ��ȭ ����";
                potion_cost = 240;
                PlayerPrefs.SetInt("trading_potion", 4);
                break;
            case 4:
                potion_name = "�߰����� ����";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 5);
                break;
            case 5:
                potion_name = "����ȭ ����";
                potion_cost = 220;
                PlayerPrefs.SetInt("trading_potion", 6);
                break;
            case 6:
                potion_name = "�ñ����� ����";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 7);
                break;
            case 7:
                potion_name = "ȭ������ ����";
                potion_cost = 190;
                PlayerPrefs.SetInt("trading_potion", 8);
                break;
            case 8:
                potion_name = "����ȭ ����";
                potion_cost = 360;
                PlayerPrefs.SetInt("trading_potion", 9);
                break;
            case 9:
                potion_name = "����ȭ ����";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 10);
                break;
            case 10:
                potion_name = "����ȯ ����";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 11);
                break;
            case 11:
                potion_name = "������";
                potion_cost = 360;
                PlayerPrefs.SetInt("trading_potion", 12);
                break;
            case 12:
                potion_name = "������";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 13);
                break;
            case 13:
                potion_name = "�αⰡ �������� ����";
                potion_cost = 260;
                PlayerPrefs.SetInt("trading_potion", 14);
                break;
            case 14:
                potion_name = "���ִ� ����";
                potion_cost = 140;
                PlayerPrefs.SetInt("trading_potion", 15);
                break;
            case 15:
                potion_name = "�Ӹ�ī���� �ڶ�� ����";
                potion_cost = 220;
                PlayerPrefs.SetInt("trading_potion", 16);
                break;
            case 16:
                potion_name = "��������";
                potion_cost = 250;
                PlayerPrefs.SetInt("trading_potion", 17);
                break;
            case 17:
                potion_name = "�߱�����";
                potion_cost = 160;
                PlayerPrefs.SetInt("trading_potion", 18);
                break;
            case 18:
                potion_name = "�ڹ���";
                potion_cost = 200;
                PlayerPrefs.SetInt("trading_potion", 19);
                break;
            case 19:
                potion_name = "��Ŀ����";
                potion_cost = 230;
                PlayerPrefs.SetInt("trading_potion", 20);
                break;
            case 20:
                potion_name = "����ȸ�� ����";
                potion_cost = 190;
                PlayerPrefs.SetInt("trading_potion", 21);
                break;
            case 21:
                potion_name = "����ȣ�� ����";
                potion_cost = 350;
                PlayerPrefs.SetInt("trading_potion", 22);
                break;
            case 22:
                potion_name = "������ ��Ȯ������ ����";
                potion_cost = 150;
                PlayerPrefs.SetInt("trading_potion", 23);
                break;
            case 23:
                potion_name = "����ȭ ����";
                potion_cost = 240;
                PlayerPrefs.SetInt("trading_potion", 24);
                break;
            case 24:
                potion_name = "�� ��ȯ ����";
                potion_cost = 210;
                PlayerPrefs.SetInt("trading_potion", 25);
                break;
            case 25:
                potion_name = "APTX4869";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 26);
                break;
            case 26:
                potion_name = "�ص�����";
                potion_cost = 120;
                PlayerPrefs.SetInt("trading_potion", 27);
                break;
            case 27:
                potion_name = "��¥����";
                potion_cost = 40;
                PlayerPrefs.SetInt("trading_potion", 28);
                break;
            case 28:
                potion_name = "��������";
                potion_cost = 160;
                PlayerPrefs.SetInt("trading_potion", 29);
                break;
            case 29:
                potion_name = "������ ����";
                potion_cost = 270;
                PlayerPrefs.SetInt("trading_potion", 30);
                break;
            case 30:
                potion_name = "���� ����";
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


