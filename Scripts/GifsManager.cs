using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GifsManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    public VideoClip[] levelVideoClips;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    public void LoadSelectedLevelGif(int level)
    {
        videoPlayer.clip = levelVideoClips[level];
    }
}
