using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class MeatBoy : MonoBehaviour{
    public float speed;
    public float jumpSpeed;
    public float gravity;
    private Vector3 mouvement;
    private CharacterController controller;
    private int jumpsCount = 0;
    public int jumpsMax;

    private void Awake() {
        controller = GetComponent<CharacterController>();    
    }
    void Update() {
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y -= gravity * Time.deltaTime;
        if(controller.isGrounded){
            mouvement.y = 0;
            jumpsCount = 0;
        }
        // this didn't work: if(Input.GetButtonDown("Jump")){
        if(Input.GetKeyDown(KeyCode.Space)){
            mouvement.y = jumpSpeed;
            if(jumpsCount < jumpsMax){
                jumpsCount++;
            }
            Debug.Log(jumpsCount);
        }
        controller.Move(mouvement * Time.deltaTime * speed);
    }
}
