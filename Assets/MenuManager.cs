using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region oyunun ilk a��l�� ekran� burada ki kod sayesinde, istenen b�l�m se�iliyor ve o b�l�m oynan�yor
    public void levelSelect(int levelNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level" + levelNumber);
    }
    #endregion
}
