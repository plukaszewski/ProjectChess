using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1;

    [Range(0f, 0.5f)]
    public float volumeRand = 0.1f;
    [Range(0f, 0.5f)]
    public float pitchRand = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-volumeRand/2f, volumeRand /2f));
        source.pitch = pitch * (1 + Random.Range(-pitchRand / 2f, pitchRand / 2f));
        source.Play();
    }

}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager");
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        for(int i=0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + 1 + "_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string _name)
    {
        for(int i=0; i <sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("AudioManager: Sound not found in array. " + _name);
    }
}
