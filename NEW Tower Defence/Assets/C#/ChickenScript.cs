using UnityEngine;

public class ChickController : MonoBehaviour
{
    public float speed = 2f; // швидкість руху
    public float leftLimit = -5f; // межа зліва
    public float rightLimit = 5f; // межа справа

    private bool movingRight = true; // напрямок руху

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
