using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private Player player;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    private GameManager _gM;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();
        _gM= GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameOver.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text="Score: " + player.GetScore();
        _liveImage.sprite = _liveSprites[player.GetLives()];
        if (player.GetLives() == 0)
        {
            GameOverSequence();
        }
       
    }
    void GameOverSequence()
    {
        _gM.GameOver();
        _gameOver.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            _gameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
