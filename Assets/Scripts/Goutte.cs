using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goutte : MonoBehaviour{
    public Vector3 velocity;
    public int force;
    private float cptGoutte;
    void Start(){
        GetComponent<Rigidbody>().AddForce(velocity*force);
    }
    void OnCollisionEnter(Collision collision){
        // this didn't work ....Destroy(this);
        Destroy(gameObject);
    }
    // void Update() {
    //     cptGoutte -= Time.deltaTime;
    //     if(cptGoutte <= 0f){
    //         GameObject goutte = Instantiate (gouttePrefab, transform.position + groutteOffset, Quaternion.identity) as GameObject;
    //         goutte.GetComponent <ScriptGoutte().velocity = new Vector3 (-mouvement.x, speed, 0f);
    //     }
    // }


}
