using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] Button _restartButton;

    public void RestartButtonPressed()
    {
        SceneManager.LoadScene(0);
    }
}
