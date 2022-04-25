using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera cam;
    public float speed = 5f;
    public Rigidbody2D rb;
    public GameObject player;
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        
    }


        void Update()
    {



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("isRunning", true);
            rb.transform.position += Vector3.left * speed * Time.deltaTime;



        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isRunning", true);
            rb.transform.position += Vector3.right * speed * Time.deltaTime;



        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isRunning", true);
            rb.transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.transform.position += Vector3.down * speed * Time.deltaTime;
        }


    }


}


