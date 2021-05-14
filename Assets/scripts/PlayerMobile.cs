using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
{
    public CharacterController controller;
    private Animator anim;
    public Combo Script;
    public float speed = 11f, gravidd = -19f, distanciadChao = 0.4f, altpulo = 3f;
    public bool agxd = false, olhdup = false, noChao, naolevanta, andando;
    public int countJump;
    public Transform groundCheck, upCheck;
    public LayerMask groundMask;

    Vector3 vGravitacional;
    public bool leftview, rightview;
    Vector3 facingRight, facingLeft;
    public Joystick walkjoy;

    void Start()
    {
        anim = GetComponent<Animator>();
        countJump = 2;
        Cursor.visible = false;
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        facingLeft.z = facingLeft.z * -1;
    }

    void Update()
    {
        MOVE();
        if (Script.airkick == true)
        {
            speed = 3.5f;
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (andando == true && noChao == false && Script.airkick == true)
        {
            speed = 3.5f;
        }

        if (walkjoy.InputDirection.z == -1 && walkjoy.InputDirection.x != 0 && agxd && andando && Script.rasteira)
        {
            speed = 0;
        }

        if (Script.rasteira == true)
        {
            speed = 0.5f;
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (andando == true && Script.rasteira == true)
        {
            speed = 0.5f;
        }

        if (Script.upkick == true)
        {
            speed = 2.5f;
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (andando == true && Script.upkick == true)
        {
            speed = 2.5f;
        }

        // var para fazer o eixo do boneco virar
        if (walkjoy.InputDirection.x < 0 && Script.batendo == false && Script.rasteira == false && Script.upkick == false && Script.airkick == false)
        {
            leftview = true;
            rightview = false;
            transform.localScale = facingLeft;
        }

        if (walkjoy.InputDirection.x > 0 && Script.batendo == false && Script.rasteira == false && Script.upkick == false && Script.airkick == false)
        {

            rightview = true;
            leftview = false;
            transform.localScale = facingRight;
        }



        if (speed == 15.5f)
        {
            controller.center = new Vector3(0, 0.75f, 0);
            controller.height = 1.4f;
            anim.SetBool("correr", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (countJump == 1)
        {
            anim.SetBool("pulo2", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("slide", false);
        }

        if (countJump == 3)
        {
            anim.SetBool("slide", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
        }
        if (andando == true && noChao == true && agxd == true && Script.batendo == false && Script.rasteira == false)
        {
            speed = 7.5f;
        }
        if (andando == true && noChao == true && naolevanta == true && Script.batendo == false && Script.rasteira == false)
        {
            speed = 7.5f;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "PULAVEL" && noChao == false && !Script.airkick)
        {
            countJump = 3;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "PULAVEL" && noChao == false)
        {
            countJump = 2;
        }
    }

    public void MOVE()
    {
        noChao = Physics.CheckSphere(groundCheck.position, distanciadChao, groundMask);
        naolevanta = Physics.CheckSphere(upCheck.position, distanciadChao, groundMask);

        if (noChao && vGravitacional.y < 0)
        {
            vGravitacional.y = -5f;
        }

        float z = walkjoy.InputDirection.x;

        Vector3 move = transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        vGravitacional.y += gravidd * Time.deltaTime;

        controller.Move(vGravitacional * Time.deltaTime);

        // nao ficar com speed 22 qnd tiver parado
        if (z == 0)
        {
            andando = false;
            speed = 11;
            anim.SetBool("parado", true);
            anim.SetBool("andar", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (z != 0)
        {
            andando = true;
            anim.SetBool("andar", true);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (z == 0 && noChao == false)
        {
            speed = 11;
            anim.SetBool("queda", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }
        if (z != 0 && noChao == false)
        {
            speed = 11;
            anim.SetBool("queda", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        // voltar o contador de pulo duplo pra 2 ao pisar no chao !!!!!!!!!!!!PULO DUPLO!!!!!!!!!!
        if (noChao)
        {
            Script.airkick = false;
            anim.SetBool("chutear", false);
            countJump = 2;
        }
        // pular
        if (countJump > 1 && agxd == false && Script.batendo == false && naolevanta == false && Input.GetKeyDown(KeyCode.Space))
        {
            countJump--;
            vGravitacional.y = Mathf.Sqrt(altpulo * -1f * gravidd);
        }
        // mecanica de !!!!!!!!PULO NORMAL!!!!!!!
        // if (Input.GetKeyDown(KeyCode.Space) && noChao && agxd == false || Input.GetKeyDown(KeyCode.W) && noChao && agxd == false)
        // {
        //     vGravitacional.y = Mathf.Sqrt(altpulo * -2f * gravidd);
        // }

        //agaixar
        if (walkjoy.InputDirection.z == -1 && noChao == true && Script.rasteira == false && andando == true)
        {
            speed = 7.5f;
            agxd = true;
            olhdup = false;
            controller.height = 0.9f;
            controller.center = new Vector3(0, 0.52f, 0);
        }

        if (walkjoy.InputDirection.z != -1)
        {
            olhdup = false;
            agxd = false;
            controller.height = 1.71f;
            controller.center = new Vector3(0, 0.92f, 0);
        }

        if (walkjoy.InputDirection.z == 1 && noChao == true)
        {
            olhdup = true;
            agxd = false;
            controller.height = 1.71f;
            controller.center = new Vector3(0, 0.52f, 0);
        }
        // correr
        if (walkjoy.InputDirection.x == 1 && agxd == false && z != 0 && naolevanta == false || walkjoy.InputDirection.x == -1 && agxd == false && z != 0 && naolevanta == false)
        {
            andando = false;
            speed = 15.5f;
        }
        // nao ficar rapido no ar
        if (noChao == false)
        {
            speed = 11;
        }

        if (agxd == true && z == 0)
        {
            anim.SetBool("agaixar", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (agxd == true && z != 0)
        {
            anim.SetBool("andaragaixado", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (naolevanta == true && z != 0)
        {
            controller.height = 0.9f;
            controller.center = new Vector3(0, 0.52f, 0);
            anim.SetBool("andaragaixado", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (naolevanta == true && z == 0)
        {
            controller.height = 0.9f;
            controller.center = new Vector3(0, 0.52f, 0);
            anim.SetBool("agaixar", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("queda", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (naolevanta == false && agxd == false)
        {
            controller.height = 1.71f;
            controller.center = new Vector3(0, 0.92f, 0);
        }

        if (agxd == true && noChao == false)
        {
            anim.SetBool("queda", true);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }
        if (Script.batendo == true)
        {
            speed = 0.5f;
            anim.SetBool("queda", false);
            anim.SetBool("andar", false);
            anim.SetBool("parado", false);
            anim.SetBool("correr", false);
            anim.SetBool("agaixar", false);
            anim.SetBool("andaragaixado", false);
            anim.SetBool("pulo2", false);
            anim.SetBool("slide", false);
        }

        if (andando == true && Script.batendo == true)
        {
            speed = 0.5f;
        }

        if (andando == true && Script.batendo == false)
        {
            speed = 11;
        }
    }
}