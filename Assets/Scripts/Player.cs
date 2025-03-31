using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    //publþc or private reference
    //data type(int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField]//private olsa da componentsten deðiþtirlebilir ama diðer gameobjectler etki edemez
    private float _speed = 3.5f;//f stands for float(int olsa problem yok) public olduðunda diðer gameobjectlerde etkide bulunabilir ve componentsten deðiþtirilebilir
    [SerializeField]
    private GameObject _laserPrefab;//laseri tanýmladýk
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private GameObject[] EngineHurts;
    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;
    private Animator _turnAnimation;


    // Start is called before the first frame update
    void Start()
    {
        EngineHurts[0].SetActive(false);
        EngineHurts[1].SetActive(false);
        transform.position = new Vector3(0, 0, 0);
        _spawnManager= GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _audioSource=GetComponent<AudioSource>();
        _turnAnimation=GetComponent<Animator>();
        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the  player is NULL");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        FireLaser();
        DamageVisualizer();
        
    } 
    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //new Vector3(1, 0, 0) * 3.5 * real time; ---> saniyede 5 metre
        if (horizontalInput > 0) {
            _turnAnimation.ResetTrigger("RightToStop");
            _turnAnimation.SetTrigger("TurnRight");
        }
        else if(horizontalInput< 0) {
            _turnAnimation.ResetTrigger("LeftToStop");
            _turnAnimation.SetTrigger("TurnLeft");
        }
        else
        {
            _turnAnimation.SetTrigger("RightToStop");
            _turnAnimation.SetTrigger("LeftToStop");
            _turnAnimation.ResetTrigger("TurnRight");
            _turnAnimation.ResetTrigger("TurnLeft");
        }
        transform.Translate(((Vector3.right * horizontalInput) + (Vector3.up * verticalInput)) * _speed * Time.deltaTime);

        //Vector3 direction= new Vector3(horizontalInput, verticalInput, 0);
        //transform.Translate(direction * _speed * Time.deltaTime);


        //bounds of y axis
       
        /*
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }
        */
       
        //anathor method for clamping
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        //bounds of x axis
        //if x >=11
        //x=-11
        //else if x<=-11
        //x=11
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
    void FireLaser()
    {
        //If I hit the space 
        //spawn gameObject (Laser)(Instantiate)

        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire) //Time.time oyun baþlamasýndan beri geçen süre
        {
            
            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive)
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            else
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            _audioSource.Play();//(spawnlanacak prefab,spawnlanacak yeri(playerýnolduðu yer þu an,rotation(þu an default))
        }
       
    }

    public void Damage()
    {
        if(_isShieldActive)
        {
            _isShieldActive= false;
            shield.SetActive(false);

        }
        else
        {
            _lives--;
        }
        
        
        
        
        if (_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }
    public void TripleShotActive()
    {
        _isTripleShotActive= true;
       StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _speed *= 2;
        StartCoroutine(SpeedBoostDownRoutine());
    }
    IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= 2;
    }

    public void ShieldPowerupActive()
    {
        _isShieldActive = true;
        shield.SetActive(true);
    }
    public void IncreaseScore()
    {
        _score += 10;
    }
    public int GetScore()
    {
        return _score;
    }
    public int GetLives()
    {
        return _lives;
    }

    public void DamageVisualizer()
    {
        if(_lives==2)
        {
            EngineHurts[0].SetActive(true);
        }
        if(_lives==1)
        {
            EngineHurts[1].SetActive(true);
        }
    }
}
