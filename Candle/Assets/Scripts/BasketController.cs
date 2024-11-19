using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float leftBoundX = -10f;
    public float rightBoundX = 10f;
    public bool requiresPlayer = true; // Condition to require player touch

    private bool isPlayerTouching = false;
    private bool movingRight = true; // To alternate movement when requiresPlayer is false

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (requiresPlayer)
        {
            if (isPlayerTouching)
            {
                MoveBasketRight();
            }
            else
            {
                MoveBasketLeft();
            }
        }
        else
        {
            if (movingRight)
            {
                MoveBasketRight();
                if (transform.position.x >= rightBoundX)
                {
                    movingRight = false;
                }
            }
            else
            {
                MoveBasketLeft();
                if (transform.position.x <= leftBoundX)
                {
                    movingRight = true;
                }
            }
        }
    }

    private void MoveBasketRight()
    {
        if (transform.position.x < rightBoundX)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    private void MoveBasketLeft()
    {
        if (transform.position.x > leftBoundX)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerTouching = false;
        }
    }
}
