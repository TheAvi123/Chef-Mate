using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //Reference Variables
    [SerializeField] TextMeshProUGUI textField = null;
    [SerializeField] AudioClip[] clips = null;
    [SerializeField] float time = 10f;
    AudioSource source;

    //Configuration Parameters

    //State Variables
    private float timer;
    //Internal Methods

    private void Start() {
        timer = time;
        source = gameObject.AddComponent<AudioSource>();
        source.loop = false;
    }

    private void Update() {
        if (timer > 0f) {
            timer -= Time.deltaTime;
        } else {
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            timer = time;
        }
    }
}
