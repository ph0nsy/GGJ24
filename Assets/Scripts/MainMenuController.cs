using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public string gameScene = "scene name here";
    [SerializeField] public float minLoadingDuration = 10;
    [HideInInspector] GameObject loadingPanel;
    [HideInInspector] Slider loadingBar;

    void Start(){
        loadingPanel = this.transform.GetChild(1).gameObject;
        loadingBar = loadingPanel.transform.GetChild(0).GetComponent<Slider>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Play (){
        loadingPanel.SetActive(true);
        StartCoroutine(LoadSceneAsync(gameScene));
    } 

    IEnumerator LoadSceneAsync (string levelName){
        float startTimeStamp = Time.time;
        loadingPanel.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(levelName);
        while(!op.isDone){
            float progress = Mathf.Clamp01(op.progress /.9f);
            loadingBar.value= progress;
            yield return null;
        } 
    }

    public void Quit(){
        Application.Quit();
    }
}
