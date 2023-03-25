using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{ 
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemySpawner spawner;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] playerShootClips;
    [SerializeField] private AudioClip[] enemyShootClips;
    [SerializeField] private AudioClip[] deathClips;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private float killedFairies;

    //level timer
    //score counter
    //end screen
    //death screen
    //check fairy kills
    //play audio

    private void Start()
    {
        playerController.OnShoot += PlayerController_OnShoot;
        RegisterFairies();
    }

    private void PlayerController_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
        AudioClip clip = playerShootClips[UnityEngine.Random.Range(0, playerShootClips.Length)];
        audioSource.transform.position = e.position;
        audioSource.PlayOneShot(clip);
        StartCoroutine(PlayClipWithDelay(clip.length, reloadClip));
    }

    IEnumerator PlayClipWithDelay(float duration,AudioClip clip)
    {
        yield return new WaitForSeconds(duration - duration/3f);
        audioSource.PlayOneShot(clip);
    }
    private void RegisterFairies()
    {
        foreach (Enemy fairy in spawner.GetFairies())
        {
            fairy.OnEnemyDeath += Fairy_OnEnemyDeath;
        }
    }
    private void CheckForAllKilledFairies()
    {
        if(killedFairies <= spawner.GetFairies().Count)
        {
            //game over you win !!!
        }
    }

    private void Fairy_OnEnemyDeath(object sender, Enemy.OnDeathArgs e)
    {
        AudioClip clip = deathClips[UnityEngine.Random.Range(0, playerShootClips.Length)];
        audioSource.transform.position = e.position;
        audioSource.PlayOneShot(clip);
        killedFairies++;
        
        CheckForAllKilledFairies();

    }
}
