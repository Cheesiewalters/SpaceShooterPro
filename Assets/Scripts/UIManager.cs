using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _GameOverText;
    [SerializeField]
    private Text _RestartlevelText;
    private Player player;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private float _fireRate = 1.0f;
    [SerializeField]
    private float _nextFire = 0.0f;
    [SerializeField]
    private Gamemanager _Gm;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        _Gm = GameObject.Find("GameManager").GetComponent<Gamemanager>();
        _GameOverText.text = "";
        _RestartlevelText.text = "";

        if(_Gm == null)
        {
            Debug.Log("Game Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + player.GetScore();
        UpdateLives();
    }

    private void UpdateLives()
    {
        if (player.GetLives() > 0)
        {
            _livesImage.sprite = _livesSprites[player.GetLives()];
        }
        else
        {
            _livesImage.sprite = _livesSprites[player.GetLives()];
            _GameOverText.text = "GAME OVER";
            _RestartlevelText.text = "Press R to restart the Level";
            _Gm.GameOver();
        }
    }
}
