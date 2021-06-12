using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPU : MonoBehaviour
{
    public GameObject popUpGameOver;
    public PicaFresa picaFresa;
    public Button Ok;
    public TextMeshProUGUI vidas;

    public void Reset()
    {
        picaFresa.currentHP = 3;
        vidas.text = picaFresa.currentHP.ToString();
        picaFresa.transform.position = picaFresa.initialPosition;
        popUpGameOver.SetActive(false);
    }
    public void Defeat()
    {
        popUpGameOver.SetActive(true); //activa el pop up
    }

}
