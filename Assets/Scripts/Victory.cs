﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Victory : MonoBehaviour{
    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            Debug.LogError("Victory!");
            SceneManager.LoadScene("Nivel");
        }   
    }

}
