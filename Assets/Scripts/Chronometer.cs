using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Chronometer : MonoBehaviour
{
    private float chrono = 0f;
    private float minutes, secondes, fraction;
    public Text chronoUI;
    private float bestScore = 600f;
    private string bestScoreString;

    void Awake() {
        minutes = (int)(bestScore/60f);
        secondes = (int)(bestScore%60f);
        fraction = (int)((bestScore*100f)%100f);
        //setBestScore(bestScore);
        Debug.Log("Best score is:" + bestScore);
    }

    void Start() {
        if(PlayerPrefs.HasKey("BestScore") == true){
            bestScore = PlayerPrefs.GetInt("BestScore");
        }else{
            bestScore = 600f;
            setBestScore(bestScore);   
        }
        bestScoreString = "Best : " + minutes + ":" + secondes + ":" + fraction;
    }
    void setBestScore(float score){
        PlayerPrefs.SetInt("BestScore", (int)(score));
    }
    void Update(){
        chrono += Time.deltaTime;
        minutes = (int)(chrono/60f);
        secondes = (int)(chrono%60f);
        fraction = (int)((chrono*100f)%100f);
        chronoUI.text = bestScoreString + "\n" + "Temps : " + minutes + ":" + secondes + ":" + fraction;
    }
    public void End(){
        if(chrono<bestScore){
            setBestScore(chrono);
            SceneManager.LoadScene("Victory");
            Debug.Log("Fin!");
        }

    }
}
