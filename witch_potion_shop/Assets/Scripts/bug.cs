using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class bug : MonoBehaviour
{

    bool IsMoving;
    int direction;
    float movespeed;

    private Text coin_count;
    private SpriteRenderer picture;
    private Animator dying;

    // Start is called before the first frame update
    void Start()
    {
        coin_count = GameObject.Find("coincount").GetComponent<Text>();
        picture = this.GetComponent<SpriteRenderer>();
        dying = this.GetComponent<Animator>();
        movespeed = 9f;
        IsMoving = false;
        float temp = Random.Range(0f, 15f);
        Invoke("moving_start", temp);
    }

    void moving_start()
    {
        IsMoving = true;
        direction = Random.Range(0, 4);
        float temp;
        if(direction == 0)
        {
            this.transform.Rotate(0f, 0f, 270f);
            temp = Random.Range(-2f, 6f);
            this.transform.position = new Vector3(-10f, temp, -1);
        }
        else if (direction == 1)
        {
            this.transform.Rotate(0f, 0f, 180f);
            temp = Random.Range(-8f, 8f);
            this.transform.position = new Vector3(temp, 13f, -1);
        }
        else if (direction == 2)
        {
            this.transform.Rotate(0f, 0f, 90f);
            temp = Random.Range(-2f, 6f);
            this.transform.position = new Vector3(10f, temp, -1);
        }
        else
        {
            this.transform.Rotate(0f, 0f, 0f);
            temp = Random.Range(-8f, 8f);
            this.transform.position = new Vector3(temp, -4f, -1);
        }
    }

    void moving()
    {
        if(direction==0)
        {
            this.transform.position += Vector3.right * movespeed * Time.deltaTime;
            if(this.transform.position.x > 10f)
            {
                IsMoving = false;
                reincarnation();
            }
        }
        else if (direction == 1)
        {
            this.transform.position += Vector3.down * movespeed * Time.deltaTime;
            if (this.transform.position.y < -4f)
            {
                IsMoving = false;
                reincarnation();
            }
        }
        else if (direction == 2)
        {
            this.transform.position += Vector3.left * movespeed * Time.deltaTime;
            if (this.transform.position.y < -10f)
            {
                IsMoving = false;
                reincarnation();
            }
        }
        else if (direction == 3)
        {
            this.transform.position += Vector3.up * movespeed * Time.deltaTime;
            if (this.transform.position.y > 13f)
            {
                IsMoving = false;
                reincarnation();
            }
        }
    }

    void reincarnation()
    {
        dying.SetBool("bug_dead", false);
        this.transform.position = new Vector3(-6f, 6f, -1);
        if (direction==0)
        {
            this.transform.Rotate(0f, 0f, -270f);
        }
        else if (direction == 1)
        {
            this.transform.Rotate(0f, 0f, -180f);
        }
        else if (direction == 2)
        {
            this.transform.Rotate(0f, 0f, -90f);
        }
        else if (direction == 3)
        {
            this.transform.Rotate(0f, 0f, -0f);
        }
        picture.sprite = (Sprite)Resources.Load("image/bug", typeof(Sprite));
        float temp = Random.Range(0f, 15f);
        Invoke("moving_start", temp);
    }

    public void OnMouseDown()
    {
        if(IsMoving)
        {
            coin_count.text = (int.Parse(coin_count.text) + 10).ToString();
        }
        picture.sprite = (Sprite)Resources.Load("image/bug_dead", typeof(Sprite));
        IsMoving = false;
        dying.SetBool("bug_dead", true);
        Invoke("reincarnation", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving) moving();
    }
}
