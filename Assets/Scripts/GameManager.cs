using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public delegate void GameEvent();
    public event GameEvent OnGameStart;

    public static GameManager inst = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager Instance { get { return inst; } }

    public AudioMixer obsMixer;

    private float _initialGameSpeed = 30f;
    [SerializeField]
    private float _gameSpeed = 0f;// 10f per frame +/- equals 3m/s
    public bool startGame = false;
    public bool ended = false;
    public bool interacted = false;
    public bool displayUI = false;
    public string endGameStatus = "";

    /*
     * Music
     */
    public int CurrentClipIndex;
    public float obsLvl = 0.0f;

    /*
     * Speed
     */
    public float RealSpeed; // Speed in km/h
    public float RoadLength = 20f;
    public float DistanceToDestroy = 60f; // have to be higher than RoadLength (approx multiple by 1.5 - 2)
    private int maxSpeed = 190;

    /*
     *  Distances - in meter
     */
    public float distance;
    public float realDistance;
    public float oldDistance = 0f;
    public float distanceToHospital = 2000.0f;
    public float distanceRemaining = 2000.0f;

    /*
     * Time
     */
    public float timer = 0.0f;
    public int timeSinceStart; // Time in seconds
    public int gameDuration = 60;
    public int timeRemaining = 60;

    /*
     * Getter & Setter
     */
    public float GameSpeed { get => _gameSpeed; set => _gameSpeed = Mathf.Clamp(value, 0, MaxSpeed); }
    public int TimeSinceStart { get => timeSinceStart; set => timeSinceStart = value; }
    public int MaxSpeed { get => maxSpeed; }


    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (inst == null)
            
            //if not, set instance to this
            inst = this;
        
        //If instance already exists and it's not this:
        else if (inst != this)
            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    
    public void InitGame()
    {
        Debug.Log("Game started");
        GameManager.inst.startGame = true;
        GameManager.inst.displayUI = true;
        GameManager.inst.GameSpeed = _initialGameSpeed;
        GameManager.inst.timeRemaining = gameDuration;
        GameManager.inst.distanceRemaining = distanceToHospital;
        GameManager.inst.OnGameStart?.Invoke();
    }
    
    //Update is called every frame.
    void Update()
    {
        if (startGame && interacted)
        {
            timer += Time.deltaTime;
            if((int)timer > timeSinceStart)
            {
                timeRemaining -= 1;
                if (timeRemaining < 0) timeRemaining = 0;
                if(GameSpeed > 0)
                {
                    distanceRemaining -= RealSpeed / 3.6f;
                    if(distanceRemaining < 0.0f)
                    {
                        distanceRemaining = 0;
                    }
                }
                if(timeRemaining <= 0 && distanceRemaining > 10)
                {
                    Debug.Log("Loose");
                    endGameStatus = "lost";
                    StopGame();
                    return;
                } else if(distanceRemaining <= 10 && timeRemaining >= 0)
                {
                    Debug.Log("Won");
                    endGameStatus = "won";
                    StopGame();
                    return;
                }
            }
            timeSinceStart = (int)timer;
            distance += GameSpeed;
            realDistance += (3 * 1f / Time.deltaTime * GameSpeed) / 10;
            RealSpeed = (realDistance - oldDistance) * (3.6f * Time.deltaTime);
            oldDistance = realDistance;
        } else if(GameSpeed > 0 && ended)
        {
            GameSpeed -= GameSpeed > 0 ? Mathf.Pow(GameSpeed, GameSpeed * 0.0f) * 3.0f : 0;
            distanceRemaining -= RealSpeed / 3.6f;
            if (distanceRemaining < 0.0f && endGameStatus == "won")
            {
                distanceRemaining = 0;
            }
        }
        else if(!ended)
        {
            GameSpeed = 30;
        } else if(ended && GameSpeed < 0)
        {
            displayUI = false;
        }

        if(ended)
        {
            obsLvl -= 0.1f;
            if (obsLvl < -80.0f) return;
            obsMixer.SetFloat("obsVolume", obsLvl);
        }
    }

    public void StopGame()
    {
        startGame = false;
        ended = true;
        interacted = false;
    }

    public void PauseGame()
    {

    }
}