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
    public GameObject gouttePrefab;
    public float delayGoutte;
    private float cptGoutte;
    private Vector3 defaultPosition;
    private void Awake() {
        controller = GetComponent<CharacterController>();
        if (controller == null){
            Debug.LogError("Character Controller not found.");
            enabled = false;
        }
        cptGoutte = delayGoutte;
        defaultPosition = transform.position;
    }
    void Start() { 
    }
    void Update() {
        // CONTROLS
        if (controller == null)
            return;
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y -= gravity * Time.deltaTime;
        if(controller.isGrounded){
            mouvement.y = 0;
            jumpsCount = 0;
        }
        // this didn't work: if(Input.GetButtonDown("Jump")){
        if(Input.GetKeyDown(KeyCode.Space)){
            if(jumpsCount < jumpsMax){
                jumpsCount++;
                mouvement.y = jumpSpeed;
            }
            Debug.Log(jumpsCount);
            // Création des gouttes quand MeatBoy Jump:
            Instantiate(gouttePrefab, transform.position, Quaternion.identity);
        }
        controller.Move(mouvement * Time.deltaTime * speed);
        // Création des gouttes quand MeatBoy court:
        if(mouvement.x != 0f){
            cptGoutte -= Time.deltaTime;
            //Debug.Log(cptGoutte);
            if(cptGoutte <= 0f){
                cptGoutte = delayGoutte;
                //Instantiate goutte... 
                GameObject goutte = Instantiate(gouttePrefab, transform.position, Quaternion.identity) as GameObject;
                // this is not working:
                goutte.GetComponent<Goutte>().velocity = new Vector3(mouvement.x,speed*Time.deltaTime,0f);
                
            }
        }
    }
    // nice trick for debugging:
    // private void OnControllerColliderHit(ControllerColliderHit hit){
    //     Debug.Log(hit.gameObject.name);

    //     hit.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    // }
    public void Die(){
        // this is only working when i destroy the character...
        transform.position = defaultPosition;
        controller.enabled = true;
        Debug.LogError("die");
    }
}
