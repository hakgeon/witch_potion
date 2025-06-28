using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{




    public Transform tf;

    public GameObject go;
    public GameObject[] slot;
    
    public GameObject[] selectedTabImages;



    private int selectedTab;








    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void SelectedTab()
    {
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0.6f;
        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
        Color color1 = selectedTabImages[selectedTab + 1].GetComponent<Image>().color;
        color1.a = 0f;
        selectedTabImages[selectedTab + 1].GetComponent<Image>().color = color1;
    }
    public void SelectedTab2()
    {
        Color color = selectedTabImages[selectedTab].GetComponent<Image>().color;
        color.a = 0.6f;
        selectedTabImages[selectedTab].GetComponent<Image>().color = color;
        Color color1 = selectedTabImages[selectedTab - 1].GetComponent<Image>().color;
        color1.a = 0f;
        selectedTabImages[selectedTab - 1].GetComponent<Image>().color = color1;
    }

    public void ShowTab()
    {
        SelectedTab();
        ShowItem();
    }
    public void ShowTab2()
    {
        SelectedTab2();
        ShowItem();
    }

    public void Clear()
    {
        for (int i = 0; i <= 51; i++)
        {
            slot[i].SetActive(false);
        }
    }

    public void ShowItem()
    {
        switch (selectedTab)
        {
            case 0:
                Clear();
                for (int i = 1; i <= 21; i++)
                {
                    if (PlayerPrefs.GetInt(i.ToString()) > 0)
                    {
                        slot[i-1].SetActive(true);
                        GameObject.Find("Item_Count_Text"+i.ToString()).GetComponent<Text>().text = PlayerPrefs.GetInt(i.ToString()).ToString();
                    }
                }
                break;
            case 1:
                Clear();
                for (int i = 1; i <= 31; i++)
                {
                    if (PlayerPrefs.GetInt((i+100).ToString()) > 0)
                    {
                        slot[i+20].SetActive(true);
                        GameObject.Find("Item_Count_Text"+(i+21).ToString()).GetComponent<Text>().text = PlayerPrefs.GetInt((i+100).ToString()).ToString();
                    }
                }
                break;
        }
    }


        // Update is called once per frame
        void Update()
        {

        }
        public void Close()
        {
            go.SetActive(false);

        }
        public void Open()
        {
            go.SetActive(true);
            selectedTab = 0;

            ShowTab();
            ShowItem();
        }
        public void material()
        {
            if (selectedTab != 0)
            {
                selectedTab = 0;
                ShowTab();

            }
        }
        public void potion()
        {
            if (selectedTab != 1)
            {
                selectedTab = 1;
                ShowTab2();

            }
        }

    

}
