using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float speed;
    public bool chase = false;
    public Transform startingPoint;
    private GameObject player;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (chase == true)
            Chase();
        else
        {
            ReturnStartPoint();
        }
    }


    private void ReturnStartPoint()
    {
        if (startingPoint.position.x > transform.position.x && !facingRight)
            Flip();
        if (startingPoint.position.x < transform.position.x && facingRight)
            Flip();
        if (startingPoint.position.x == transform.position.x && facingRight)
        {
            if (player.transform.position.x > transform.position.x && !facingRight)
                Flip();
            if (player.transform.position.x < transform.position.x && facingRight)
                Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
        
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;

    }

    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (player.transform.position.x > transform.position.x && !facingRight)
            Flip();
        if (player.transform.position.x < transform.position.x && facingRight)
            Flip();
    }
}
