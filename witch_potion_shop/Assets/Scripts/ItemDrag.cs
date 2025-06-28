using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {

    private Vector3 orginalPosition;
    private bool iscollide;
    private Image original_item;
    private GameObject item;
    private Text item_count;

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        transform.position = orginalPosition;
        if (iscollide)
        {
            for(int i=1;i<22;i++)
            {
                if (name == "item " + i.ToString())
                {
                    original_item = GameObject.Find("original item " + i.ToString()).GetComponent<Image>();
                    item = GameObject.Find("item " + i.ToString());
                    item_count = GameObject.Find("item count " + i.ToString()).GetComponent<Text>();
                    original_item.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    item.SetActive(false);
                    item_count.text = (int.Parse(item_count.text) - 1).ToString();
                    if (item_count.text == "0") original_item.color = Color.black;
                    PlayerPrefs.SetInt(i.ToString() + "_in_cauldron", 1);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "cauldron")
        {
            iscollide = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "cauldron")
        {
            iscollide = false;
        }
    }

    private void Start()
    {
        orginalPosition = gameObject.transform.position;
        iscollide = false;
    }
}
