using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float leftBoundX = -10f;
    public float rightBoundX = 10f;

    private bool isPlayerTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
