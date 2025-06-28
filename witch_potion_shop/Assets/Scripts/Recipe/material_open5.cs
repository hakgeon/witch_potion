using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class material_open5 : MonoBehaviour
{
    public Image image;
    public Image image2;
    // Start is called before the first frame update
    void Start()
    {


        if (PlayerPrefs.GetInt("211") == 1)
        {
            Color color = image.color;
            color.a = 0f;
            image.color = color;
            GameObject.Find("Potion").GetComponent<Image>().color = new Color(1, 1, 1);


        }

        if (PlayerPrefs.GetInt("212") == 1)
        {
            Color color = image2.color;
            color.a = 0f;
            image2.color = color;
            GameObject.Find("Potion22").GetComponent<Image>().color = new Color(1, 1, 1);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
