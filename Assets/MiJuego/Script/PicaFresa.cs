using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PicaFresa : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody;

    [Header("Balance variables")]
    [SerializeField]
    public float moveSpeed = 0.1f;
    [SerializeField]
    private float jumpForce = 1;

    public int maxHP = 3; // mi entero vale 30 de forma inicial. en este caso mi HP
    public int currentHP = 3; //si la de arriba es incial, esta vera cuando le pegan y asi

    private bool enPiso;
    public TextMeshProUGUI vidasUI;

    // Start is called before the first frame update
    void Start()
    {
        vidasUI.text = currentHP.ToString();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space) && enPiso == true)
        {
            rigidbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);  //agrega una fuerza (addforce) hacia arriba, x se queda en 0f.
            
        }

            transform.position = new Vector2(transform.position.x + (moveSpeed/10), transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) //cuando el trigger entre, es cuando pasa. (cuando entra en colision con el objeto)
    {
        if (collision.CompareTag("Hazard"))
        {
            if ((currentHP - collision.GetComponent<Laser>().damageAmount) < 0) //comparar con el limite inferior
            {
                currentHP = 0;
                animator.SetTrigger("Dead");
            }

            else
            {
                currentHP -= collision.GetComponent<Laser>().damageAmount; //"-=" hace una resta automatica de lo que tiene mi variable hp , get component es para que traiga a hazard, y ahi aplica su DamageAmount
                animator.SetTrigger("Damage");
                vidasUI.text = currentHP.ToString();

            }
        }

      
        }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            enPiso = true;
            animator.SetBool("Jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Piso"))
        {
            enPiso = false;
            animator.SetBool("Jump", true);
        }
    }
}
    
