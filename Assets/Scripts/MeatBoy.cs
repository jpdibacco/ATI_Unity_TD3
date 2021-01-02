﻿using System.Collections;
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
    public float jetPackHeat = 0f;
    public float jetPackMaxHeat = 300;
    public float jetPackHeatCount = 0.5f;
    public float jetPackSpeed;
    //public Vector3 flyVelocity;
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public JetPackBar heatBar;
    bool facingRight =  true;
    public int reSpawnTime = 2;
    // public GameObject respawnPosition;
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
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        heatBar.setMaxJetPack(jetPackMaxHeat);
    }
    void Update() {
        // CONTROLS
        if (controller == null)
            return;
        mouvement.x = Input.GetAxisRaw("Horizontal");
        mouvement.y -= gravity * Time.deltaTime;
        if(mouvement.x>0){
             this.gameObject.transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
        if(mouvement.x<0 && facingRight){
            flip();
        }else if(mouvement.x>0&& !facingRight){
            flip();
        }
        if(controller.isGrounded){
            mouvement.y = 0;
            jumpsCount = 0;
            // jetPackFuel++;
            // Debug.Log(jetPackFuel);

        }
        // this didn't work: if(Input.GetButtonDown("Jump")){
        if(Input.GetKeyDown(KeyCode.Z)){
            if(jumpsCount < jumpsMax){
                jumpsCount++;
                mouvement.y = jumpSpeed;
            }
            //Debug.Log(jumpsCount);
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
                goutte.GetComponent<Goutte>().velocity = new Vector3(mouvement.x*speed,speed*Time.deltaTime,0f);
                
            }
        }
        if(currentHealth<=0){
            Die();
        }
    }
    // nice trick for debugging:
    // private void OnControllerColliderHit(ControllerColliderHit hit){
    //     Debug.Log(hit.gameObject.name);

    //     hit.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    // }
    public void Die(){
        // this is only working when i destroy the character...
        //transform.position = defaultPosition;
        controller.enabled = false;
        //Destroy(gameObject);
        Invoke("MoveBody", reSpawnTime);
        Debug.LogError("die");
    }
    public void MoveBody(){
        currentHealth = maxHealth;
        controller.enabled = true;
        Vector3 rp = GameObject.FindGameObjectWithTag("respawn").transform.position;
        controller.Move(rp);
    }
    public void Fly(){
       mouvement.y +=0.11f*jetPackSpeed;
       heatBar.setJetPack(jetPackHeat);
       if(jetPackHeat > jetPackMaxHeat){
           mouvement.y = 0 - gravity/10;
       }

    }
    public void SuperSpeed(){
        mouvement.y -= gravity * Time.deltaTime;
        controller.Move(mouvement * Time.deltaTime * jetPackSpeed);
    }
    public void TakeDamage(int damage){
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
    void flip(){
        facingRight = !facingRight;
        //transform.Rotate(0f,180f,0f);
        this.gameObject.transform.rotation = Quaternion.Euler(0f,180f,0f);
    }
}
