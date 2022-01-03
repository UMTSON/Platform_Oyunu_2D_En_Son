using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Text scoreValueText;
    private void Start()
    {
        scoreValueText = GameObject.Find("Score Value").GetComponent<Text>();
    }

    #region oyunun ir sonraki levele geçmesini saðlayan kod
    public void NextLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion

    #region oyunu kaybedince tekrarlamasýný saðlayan kod
    public void Restart()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
    public void ClosePanel(string parentName)
    {
        GameObject.Find(parentName).SetActive(false);   
    }

    public void AddScore(int score)
    {
        int scoreValue = int.Parse(scoreValueText.text);
        scoreValue += score;
        scoreValueText.text = scoreValue.ToString();
    }

    #region oyunun seçilen bölümden baþlamasýný saðlayan kod

    public void levelSelect(int levelNumber)
    {
        SceneManager.LoadScene("Level" + levelNumber);
    }
    #endregion
}
