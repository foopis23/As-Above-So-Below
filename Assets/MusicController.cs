using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController Instance;
    private bool _isPlaying;
    private FMODHelper _fmod;
    
    private void Awake()
    {
        _fmod = new FMODHelper(new string[]{"OST"});
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void StartMusic()
    {
        if (_isPlaying) return;
        
        _isPlaying = true;
        _fmod.PlayLoop("OST");
    }

    public void StopMusic()
    {
        if (!_isPlaying) return;
        _isPlaying = false;
        _fmod.StopSound("OST");
    }
}
