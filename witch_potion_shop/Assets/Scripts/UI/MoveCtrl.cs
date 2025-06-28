using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveCtrl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler
{
    private PlayerCtrl playerCtrl;
    public Image leftImage, rightImage, upImage, downImage;
    public Color upColor, downColor;
    private Vector2 centerVec;

    public static bool isMoveStart;

    // Start is called before the first frame update
    void Start()
    {
        playerCtrl = FindObjectOfType<PlayerCtrl>();
        leftImage = this.GetComponentsInChildren<Image>()[0];
        rightImage = this.GetComponentsInChildren<Image>()[1];
        upImage = this.GetComponentsInChildren<Image>()[2];
        downImage = this.GetComponentsInChildren<Image>()[3];
        centerVec = this.GetComponent<RectTransform>().position;

        upColor = Color.white;
        downColor = new Color(0.8f, 0.8f, 0.8f, 1f);
        leftImage.color = rightImage.color = upImage.color = downImage.color = upColor;

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

    public void OnPointerDown(PointerEventData e)
    {
        isMoveStart = true;
        if(e.pointerEnter != null)
        {
            if (e.pointerEnter.tag == "LeftMove")
            {
                leftImage.color = downColor;
                rightImage.color = upColor;
                upImage.color = upColor;
                downImage.color = upColor;
                playerCtrl.moveDataH = -1f;
                playerCtrl.moveDataV = 0f;
            }
            else if (e.pointerEnter.tag == "RightMove")
            {
                leftImage.color = upColor;
                rightImage.color = downColor;
                upImage.color = upColor;
                downImage.color = upColor;
                playerCtrl.moveDataH = 1f;
                playerCtrl.moveDataV = 0f;
            }
            else if (e.pointerEnter.tag == "UpMove")
            {
                leftImage.color = upColor;
                rightImage.color = upColor;
                upImage.color = downColor;
                downImage.color = upColor;
                playerCtrl.moveDataV = 1f;
                playerCtrl.moveDataH = 0f;

            }
            else if (e.pointerEnter.tag == "DownMove")
            {
                leftImage.color = upColor;
                rightImage.color = upColor;
                upImage.color = upColor;
                downImage.color = downColor;
                playerCtrl.moveDataV = -1f;
                playerCtrl.moveDataH = 0f;
            }
        }
    }

    public void OnPointerUp(PointerEventData e)
    {
        isMoveStart = false;
        leftImage.color = rightImage.color = upImage.color = downImage.color = upColor;
        playerCtrl.moveDataH = 0f;
        playerCtrl.moveDataV = 0f;
    }

    public void OnDrag(PointerEventData e)
    {
        isMoveStart = true;
        if (e.pointerEnter != null)
        {
            if (e.pointerEnter.tag == "LeftMove")
            {
                leftImage.color = downColor;
                rightImage.color = upColor;
                upImage.color = upColor;
                downImage.color = upColor;
                playerCtrl.moveDataH = -1f;
                playerCtrl.moveDataV = 0f;
            }
            else if (e.pointerEnter.tag == "RightMove")
            {
                leftImage.color = upColor;
                rightImage.color = downColor;
                upImage.color = upColor;
                downImage.color = upColor;
                playerCtrl.moveDataH = 1f;
                playerCtrl.moveDataV = 0f;
            }
            else if (e.pointerEnter.tag == "UpMove")
            {
                leftImage.color = upColor;
                rightImage.color = upColor;
                upImage.color = downColor;
                downImage.color = upColor;
                playerCtrl.moveDataV = 1f;
                playerCtrl.moveDataH = 0f;

            }
            else if (e.pointerEnter.tag == "DownMove")
            {
                leftImage.color = upColor;
                rightImage.color = upColor;
                upImage.color = upColor;
                downImage.color = downColor;
                playerCtrl.moveDataV = -1f;
                playerCtrl.moveDataH = 0f;
            }
        }
        else
        {
            if(Mathf.Abs(e.position.x - centerVec.x) > Mathf.Abs(e.position.y - centerVec.y))
            {
                if (e.position.x < centerVec.x)
                {
                    leftImage.color = downColor;
                    rightImage.color = upColor;
                    upImage.color = upColor;
                    downImage.color = upColor;
                    playerCtrl.moveDataH = -1f;
                    playerCtrl.moveDataV = 0f;
                }
                else
                {
                    leftImage.color = upColor;
                    rightImage.color = downColor;
                    upImage.color = upColor;
                    downImage.color = upColor;
                    playerCtrl.moveDataH = 1f;
                    playerCtrl.moveDataV = 0f;
                }
            }
            else
            {
                if (e.position.y > centerVec.y)
                {
                    leftImage.color = upColor;
                    rightImage.color = upColor;
                    upImage.color = downColor;
                    downImage.color = upColor;
                    playerCtrl.moveDataV = 1f;
                    playerCtrl.moveDataH = 0f;

                }
                else
                {
                    leftImage.color = upColor;
                    rightImage.color = upColor;
                    upImage.color = upColor;
                    downImage.color = downColor;
                    playerCtrl.moveDataV = -1f;
                    playerCtrl.moveDataH = 0f;
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData e)
    {
        isMoveStart = false;
        leftImage.color = rightImage.color = upImage.color = downImage.color = upColor;
        playerCtrl.moveDataH = 0f;
        playerCtrl.moveDataV = 0f;
    }
}
