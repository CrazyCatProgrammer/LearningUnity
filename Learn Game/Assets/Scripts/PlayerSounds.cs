using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static AudioClip jumpSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        //note: The audio folder is strickly for music whereas the Resources folder is for sound effects. i.g. jumping, fight sounds, death sounds, ect.
        jumpSound = Resources.Load<AudioClip>("jump");
        audioSrc = GetComponent<AudioSource>();
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("PlayerSounds"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
        }
    }
}
