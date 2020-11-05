using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip splashSound;

    public AudioSource audioS;

    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxInSnapshot;
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;

    public LayerMask enemyMask;
    bool enemyNear;

    public AudioClip[] grassSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] hardSteps;
    
    private void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 10f, transform.forward, 0f, enemyMask);

        if (hits.Length > 0)
        {
            enemyNear = true;
        }
        else
        {
            enemyNear = false;
        }
        if (!AudioManager.manager.eventRunning)
        {


            if (enemyNear)
            {
                if (!AudioManager.manager.auxIn)
                {
                    auxInSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = auxInSnapshot;
                    AudioManager.manager.auxIn = true;
                }
                else
                {
                    if(AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        auxInSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = auxInSnapshot;
                        AudioManager.manager.auxIn = true;
                    }
                }
            }
            else
            {
                if (AudioManager.manager.auxIn)
                {
                    idleSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                    AudioManager.manager.auxIn = false;
                    
                }
                else
                {
                    if (AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        idleSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                        AudioManager.manager.auxIn = false;
                    }
                }
            }
        }
    
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            auxInSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambience"))
        {
            ambInSnapshot.TransitionTo(0.5f);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            idleSnapshot.TransitionTo(0.5f); 
        }
        if (other.CompareTag("Ambience"))
        {
            ambIdleSnapshot.TransitionTo(0.5f);
        }
    }
    public void Footsteps()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, -transform.up);
        int r = Random.Range(0, 3);
        if(Physics.Raycast(ray,out hit, 1f))
        {
            switch(hit.transform.tag){
                case "WoodFloor":
                    audioS.PlayOneShot(woodSteps[r]);
                    break;

                case "HardFloor":
                    audioS.PlayOneShot(hardSteps[r]);
                    break;

                case "GrassFloor":
                    audioS.PlayOneShot(grassSteps[r]);
                    break;
                default:
                    audioS.PlayOneShot(grassSteps[r]);
                    break;
            }
        }
    }
}
