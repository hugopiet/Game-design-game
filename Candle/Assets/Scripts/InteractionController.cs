using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionController : MonoBehaviour{


public Transform player;
//private bool isInfront = false; 
public int threshHold = 100;

SpriteRenderer sprite;

private void Start(){

    sprite = GetComponent<SpriteRenderer>();

}
private void Update(){

    float distance = Vector3.Distance(transform.position, player.position);

    if(distance < threshHold){
        Debug.Log("trigger");
        changeColor();
    }
    else{
        changeColorBack();
    }
}

private void changeColor(){

    sprite.color = new Color(2,2,2,2);

}

private void changeColorBack(){

    sprite.color = new Color(2,2,2,2);

    }
}