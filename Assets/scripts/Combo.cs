using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    public PlayerMobile Script;
    public Animator anim;
    public int nooOfClicks = 0, nooRClicks = 0, nooUClicks = 0;
    float lastClickedTime = 0, lastclickedrasteira = 0, lastclickedUpKick = 0;
    public float maxComboDelay = 0.9f, desbug, desbug2;
    public bool batendo = false, rasteira = false, upkick = false, airkick = false;


    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int attackDamage = 20;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (desbug >= 0.6f)
        {
            rasteira = false;
            nooRClicks = 0;
        }
        if (desbug2 >= 0.55f)
        {
            upkick = false;
            nooUClicks = 0;
        }

        if (nooRClicks == 1)
        {
            desbug += Time.deltaTime;
        }
        if (nooRClicks == 0)
        {
            desbug = 0;
            rasteira = false;
        }
        if (nooUClicks == 1)
        {
            desbug2 += Time.deltaTime;
        }
        if (nooUClicks == 0)
        {
            desbug2 = 0;
            upkick = false;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            nooOfClicks = 0;
        }

        if (Time.time - lastclickedrasteira > maxComboDelay)
        {
            nooRClicks = 0;
        }

        if (Time.time - lastclickedUpKick > maxComboDelay)
        {
            nooUClicks = 0;
        }

        if (Input.GetKeyDown(KeyCode.C) && Script.noChao == true && Script.agxd == false && Script.naolevanta == false && Script.olhdup == false)
        {
            lastClickedTime = Time.time;
            nooOfClicks++;
            if(nooOfClicks == 1)
            {
                batendo = true;
                anim.SetBool("1",true);
                anim.SetBool("queda", false);
                anim.SetBool("andar", false);
                anim.SetBool("parado", false);
                anim.SetBool("correr", false);
                anim.SetBool("agaixar", false);
                anim.SetBool("andaragaixado", false);
                anim.SetBool("pulo2", false);
                anim.SetBool("slide", false);
            }
            nooOfClicks = Mathf.Clamp(nooOfClicks,0,3);
        }
        // chute rasteiro
        if(Input.GetKeyDown(KeyCode.C) && Script.noChao == true && Script.agxd == true && Script.naolevanta == false && upkick == false)
        {
            lastclickedrasteira = Time.time;
            nooRClicks++;
            if(nooRClicks == 1)
            {
                rasteira = true;

            }
            nooRClicks = Mathf.Clamp(nooRClicks,0,1);
        }
        if(rasteira == true)
        {
            anim.SetBool("rasteira", true);
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false); 
        }
        if(rasteira == false)
        {
            anim.SetBool("rasteira", false);
        }
                // Chute pra cima
        if(Input.GetKeyDown(KeyCode.C) && Script.noChao == true && Script.olhdup == true && Script.naolevanta == false && Script.agxd == false && rasteira == false)
        {
            lastclickedUpKick = Time.time;
            nooUClicks++;
            if(nooUClicks == 1)
            {
                upkick = true;

            }
            nooUClicks = Mathf.Clamp(nooUClicks,0,1);
        }
        if(upkick == true)
        {
            anim.SetBool("chutecima", true);
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false); 
        }
        if(upkick == false)
        {
            anim.SetBool("chutecima", false);
        }

        if(Input.GetKeyDown(KeyCode.C) && Script.noChao == false)
        {
            airkick = true;
            Script.countJump = 0;
        }

        if(airkick)
        {
            anim.SetBool("chutear", true);
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false); 
        }

        if(!airkick)
        {
            anim.SetBool("chutear", false);
        }
    }
    public void return1()
    {
        if(nooOfClicks >=2)
        {
            batendo = true;
            anim.SetBool("2", true);
        }
        else{
            batendo = false;
            anim.SetBool("1", false);
            nooOfClicks = 0;
        }
    }

    public void return2()
    {
        if(nooOfClicks >=3)
        {
            batendo = true;
            anim.SetBool("3", true);
        }
        else{
            batendo = false;
            anim.SetBool("2", false);
            anim.SetBool("1", false);
            nooOfClicks = 0;
        }
    }

    public void return3()
    {
        batendo = false;
        anim.SetBool("1", false);
        anim.SetBool("2", false);
        anim.SetBool("3", false);
        nooOfClicks = 0;
    }

    public void return4()
    {
        rasteira = false;
        anim.SetBool("rasteira", false);
        nooRClicks = 0;
    }

    public void return5()
    {
        upkick = false;
        anim.SetBool("chutecima", false);
        nooUClicks = 0;
    }
}
