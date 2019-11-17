using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip currentClip;
    public GameObject emptyPrefab;
    public string[] buttonsName = { "FORWARD", "BACKWARD", "PLAY", "KLAXON" };

    private AudioSource _audioSource;
    private RadioInteractions _buttonScript;
    private Animation _buttonClick;
    private bool _canPlay = false;
    private GameObject _radio;
    private Animation _gyrophareAnim;

    void Start()
    {
        // Get button animation
        _buttonClick = gameObject.GetComponent<Animation>();

        // Get audio source
        _audioSource = gameObject.GetComponent<AudioSource>();

        // Get Radio object
        _radio = GameObject.Find("RADIO");

        // Get gyrophare
        _gyrophareAnim = GameObject.Find("Gyrophare").GetComponent<Animation>();
        _gyrophareAnim["Gyrophare"].wrapMode = WrapMode.Loop;

        // Get buttons and assing colliders + script
        foreach (string buttonName in buttonsName)
        {
            addButtonControl(GameObject.Find(buttonName));
        }

        // Load audio clips into the array
        audioClips = new AudioClip[]{
            (AudioClip)Resources.Load("Sounds/Music/drive_soundtrack"),
            (AudioClip)Resources.Load("Sounds/Music/nightcall_kavinsky"),
            (AudioClip)Resources.Load("Sounds/Music/taxi_angela")
        };

        updateMusic();
    }

	void Update()
	{
        // Change song when current is finished
        if(GameManager.inst.startGame)
        {
            if (!_audioSource.isPlaying && _canPlay)
            {
                if (GameManager.inst.CurrentClipIndex < audioClips.Length - 1)
                {
                    // Update current clip
                    GameManager.inst.CurrentClipIndex++;
                }
                else
                {
                    // We reached end of audioClips, loop it
                    GameManager.inst.CurrentClipIndex = 0;
                }

                updateMusic();
            }
        } else if(GameManager.inst.ended)
        {
            _audioSource.volume -= 0.001f;
        }
        
    }

    public void addButtonControl(GameObject button)
    {
        button.AddComponent<BoxCollider>();
        button.AddComponent<RadioInteractions>();

        // Instantiate empty gameobject to hold our animation
        GameObject empty = Instantiate<GameObject>(this.emptyPrefab, _radio.transform);
        empty.name = "EMPTY_BUTTON_" + button.name;
        button.transform.parent = empty.transform;
        button.layer = LayerMask.NameToLayer("interactions");
    }

    public void OnButtonClick(GameObject button)
    {
        switch (button.name)
        {
            case "FORWARD":
                GameManager.inst.CurrentClipIndex++;
                updateMusic();
                break;
            case "BACKWARD":
                GameManager.inst.CurrentClipIndex--;
                updateMusic();
                break;
            case "PLAY":
                _canPlay = !_canPlay;
                toggleMusic();
                break;
            case "KLAXON":
                var siren = button.GetComponent<AudioSource>();

                if (!siren.isPlaying)
                {
                    siren.Play();
                    _gyrophareAnim.Play("Gyrophare");
                }
                else
                {
                    siren.Pause();
                    _gyrophareAnim.Stop("Gyrophare");
                }

                break;
        }

        // Play press animation
        if(button.transform.parent.GetComponent<Animation>() != null)
        {
            button.transform.parent.GetComponent<Animation>().Play("ButtonClick");
        }
    }

    public void toggleMusic()
    {
        if (_canPlay)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }


    public void updateMusic()
    {
        // Check  for looping sound
        if (GameManager.inst.CurrentClipIndex > audioClips.Length - 1)
        {
            GameManager.inst.CurrentClipIndex = 0;
        }
        else if (GameManager.inst.CurrentClipIndex < 0)
        {
            GameManager.inst.CurrentClipIndex = audioClips.Length - 1;
        }

        // Update audio source
        currentClip = audioClips[GameManager.inst.CurrentClipIndex];
        _audioSource.clip = currentClip;
        if (_canPlay)
        {
            _audioSource.Play();
        }
    }
}
