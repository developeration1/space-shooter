using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Text _scoreText;

    [SerializeField]
    Sprite[] _livesSprites;
    [SerializeField]
    Image _livesImg;
    [SerializeField]
    Text _gameOverText;
    [SerializeField]
    Text _restartText;

    [SerializeField]
    GameManager _gm;

    void Start()
    {
        _scoreText.text = "Score: 0";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gm == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int current)
    {
        _livesImg.sprite = _livesSprites[current];
        if(current <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        _gm.GameOver();
        StartCoroutine(GameOverRoutine());
        _restartText.gameObject.SetActive(true);
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            _gameOverText.gameObject.SetActive(!_gameOverText.gameObject.activeSelf);
        }
    }
}
