using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scie : MonoBehaviour{
    public float speed;
    public GameObject gouttePrefab; 
    public Texture tache;

    void Update(){
       transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "FraiseBoy"){
        //Instantiate goutte...
        Instantiate(gouttePrefab, transform.position, Quaternion.identity);
        GetComponent<Renderer>().material.mainTexture = tache;
        other.gameObject.GetComponent<MeatBoy>().Die();
        }
    }
}
