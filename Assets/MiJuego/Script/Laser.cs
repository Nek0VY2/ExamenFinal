using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float rotationTreshold; //variable del "rango/umbral"
    public float speed; //variable de la velocidad del movimiento
    public float maxOffset = 0.2f;
    [SerializeField]
    Quaternion maxLeft;
    [SerializeField]
    Quaternion maxRight;
    Coroutine currentCoroutine;

    public UnityEngine.Experimental.Rendering.Universal.Light2D light1;
    public Color color1;
    public Color color2;
    public float tiempo;

    public int damageAmount;
    private Animator animator;
    public ParticleSystem particlesHa;

    void Start()
    {
        maxLeft.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + rotationTreshold);
        maxRight.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + rotationTreshold * -1);
        currentCoroutine = StartCoroutine(MoveLeft());

        light1.color = Color.white;
        tiempo = Random.Range(0.4f, 1.5f);
    }

    void Update()
    {
        light1.color = Color.Lerp(color1, color2, Mathf.PingPong(Time.time, tiempo));
    }

    IEnumerator MoveLeft() //IEnumerator implica otro tipo de procesos extra más complejos, requiere que se ejecuten frame by frame, se les conoce como "cortinas".
    {
        while (transform.localEulerAngles.z > maxLeft.eulerAngles.z + maxOffset)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, maxLeft, Time.deltaTime * speed); //modificamos el segundo valor, por que es el que nos dira a donde se movera.
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(currentCoroutine); //metodo para detener corutinas
        currentCoroutine = StartCoroutine(MoveRight()); //iniciar la corutina tal.
    }

    IEnumerator MoveRight()
    {
        while (transform.localEulerAngles.z < maxRight.eulerAngles.z - maxOffset)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, maxRight, Time.deltaTime * speed);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(MoveLeft());


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("HazardActive");
        }
    }
}
