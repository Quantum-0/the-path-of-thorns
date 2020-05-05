using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField]
    int Damage;
    [SerializeField]
    float upForce;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<MovePlayer>().Hit(Damage);

            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //{
        //    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
        //    (Vector2)(collision.gameObject.transform.position - transform.position).normalized * 1f,
        //    ForceMode2D.Force);
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, upForce), ForceMode2D.Impulse);
        //}
    }
}
