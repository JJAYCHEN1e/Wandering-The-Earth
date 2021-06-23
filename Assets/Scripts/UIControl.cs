using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{

    public GameObject Result;
    public Text ResultText;
    public GameObject Tip;
    public Text TipText;
    //public GameObject Window;

    // Start is called before the first frame update
    void Start()
    {
        //Window.SetActive(false);
        Result.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowResult(string content)
    {
        Result.SetActive(true);
        ResultText.text = content;
    }

    public void ShowTip(string content)
    {
        Tip.SetActive(true);
        TipText.text = content;
    }

    public void SetWindowActive(bool v)
    {
        //Window.SetActive(v);
    }

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void LoadSimulatingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
