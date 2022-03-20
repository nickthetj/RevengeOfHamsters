using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour

{
    // Start is called before the first frame update
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trap_insta_death")){
            Death();
        }
    }

    private void Death() {
        anim.SetTrigger("death");
    }
}
