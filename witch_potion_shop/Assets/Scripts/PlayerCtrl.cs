using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    private new Transform transform;
    private new Rigidbody2D rigidbody;
    private Animator animator;

    public float moveDataH;
    public float moveDataV;
    public float moveSpeed;

    public GameObject Quit;

    private int location;

    private Text coin_count;

    // Start is called before the first frame update
    void Start()
    {
        coin_count = GameObject.Find("coincount").GetComponent<Text>();
        coin_count.text = PlayerPrefs.GetInt("money").ToString();

        transform = this.GetComponent<Transform>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();

        moveSpeed = 3f;

        location = PlayerPrefs.GetInt("location");
        if(location==1)
        {
            PlayerPrefs.SetInt("location", 0);
            transform.position = new Vector3(0f, 7.5f, 0f);
        }
        if (location == 2)
        {
            PlayerPrefs.SetInt("location", 0);
            transform.position = new Vector3(8.0f, 0.5f, 0f);
        }
        if (location == 3)
        {
            PlayerPrefs.SetInt("location", 0);
            transform.position = new Vector3(-6.0f, -0.6f, 0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "wall6")
        {
            SceneManager.LoadScene("store");
        }
        if (other.gameObject.name == "CauldronImage")
        {
            SceneManager.LoadScene("cauldron");
        }
        if (other.gameObject.name == "DeskImage")
        {
            SceneManager.LoadScene("Recipe 1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!MoveCtrl.isMoveStart)
        {
            moveDataH = Input.GetAxisRaw("Horizontal");
            moveDataV = Input.GetAxisRaw("Vertical");
        }
        if (moveDataH == 0f)
            animator.SetBool("isMove", false);
        else
            animator.SetBool("isMove", true);
        if (moveDataV > 0f)
        {
            animator.SetBool("isUpMove", true);
            animator.SetBool("isDownMove", false);
        }
        else if (moveDataV < 0f)
        {
            animator.SetBool("isDownMove", true);
            animator.SetBool("isUpMove", false);
        }
        else
        {
            animator.SetBool("isDownMove", false);
            animator.SetBool("isUpMove", false);
        }

        transform.position += Vector3.right * moveDataH * moveSpeed * Time.deltaTime;
        transform.position += Vector3.up * moveDataV * moveSpeed * Time.deltaTime;
        if (moveDataH < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDataH > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void OnDestroy()
    {
        PlayerPrefs.SetInt("money", int.Parse(coin_count.text));
    }
}
