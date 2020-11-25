using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Chronometer : MonoBehaviour
{
    private float chrono = 0f;
    private float minutes, secondes, fraction, b_minutes, b_secondes, b_fraction;
    public Text chronoUI;
    private float bestScore = 60f;
    private string bestScoreString;

    void Awake() {
        b_minutes = (int)(bestScore/60f);
        b_secondes = (int)(bestScore%60f);
        b_fraction = (int)((bestScore*100f)%100f);
        //setBestScore(bestScore);
        //Debug.Log("Best score is:" + bestScore);
        //PlayerPrefs.DeleteAll();
    }

    void Start() {
        bestScore = 600f;
        if(PlayerPrefs.HasKey("BestScore") == true){
            bestScore = PlayerPrefs.GetInt("BestScore");
        }else{
            setBestScore(bestScore);   
        }
        bestScoreString = "Best : " + b_minutes + ":" + b_secondes + ":" + b_fraction;
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
        Debug.Log("Chrono: " +chrono);
        Debug.Log("BestScore: " +bestScore);
        if(chrono<bestScore){
            setBestScore(chrono);
            b_minutes = (int)(chrono/60f);
            b_secondes = (int)(chrono%60f);
            b_fraction = (int)((chrono*100f)%100f);

        }
        Debug.Log("Fin!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
