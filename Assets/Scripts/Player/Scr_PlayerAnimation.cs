using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlayerAnimation : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField] Scr_PlayerMovement scrMovement;
    [SerializeField] public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Horizontal Animation
        if(scrMovement.h != 0)
            anim.SetBool("hMovement", true);
        else
            anim.SetBool("hMovement", false);
        
        //Attacking
        if (scrMovement.attack)
        {
            anim.SetTrigger("isAttackingRuller");
        }
    }
}
