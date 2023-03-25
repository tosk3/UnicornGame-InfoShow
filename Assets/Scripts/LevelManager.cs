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
    [SerializeField] private float killedEnemies;
    [SerializeField] private float levelTimer;
    [SerializeField] private float levelTimerMax;
    [SerializeField] private float score;



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
        levelTimer = levelTimerMax;
    }
    private void Update()
    {
        if (RunLevelTimer())
        {
            //game Over score rampage over
            score = killedFairies * 200 + killedEnemies * 20 + levelTimerMax * 50;
        }
    }

    private void PlayerController_OnShoot(object sender, PlayerController.OnShootEventArgs e)
    {
        AudioClip clip = playerShootClips[UnityEngine.Random.Range(0, playerShootClips.Length)];
        audioSource.transform.position = e.position;
        audioSource.PlayOneShot(clip);
        StartCoroutine(PlayClipWithDelay(clip.length, reloadClip));
    }

    IEnumerator PlayClipWithDelay(float duration, AudioClip clip)
    {
        yield return new WaitForSeconds(duration - duration / 3f);
        audioSource.PlayOneShot(clip);
    }

    private void RegisterFairies()
    {
        foreach (Enemy fairy in spawner.GetFairies())
        {
            fairy.OnEnemyDeath += Fairy_OnEnemyDeath;
        }
    }
    public void RegisterEnemy(Enemy enemy)
    {
        enemy.OnEnemyDeath += Enemy_OnEnemyDeath;
    }

    private void Enemy_OnEnemyDeath(object sender, Enemy.OnDeathArgs e)
    {
        AudioClip clip = deathClips[UnityEngine.Random.Range(0, playerShootClips.Length)];
        audioSource.transform.position = e.position;
        audioSource.PlayOneShot(clip, 0.5f);
        killedEnemies++;
    }

    private void CheckForAllKilledFairies()
    {
        if (killedFairies >= spawner.GetFairies().Count)
        {
            score = killedFairies * 200 + killedEnemies * 20 + (levelTimer + levelTimerMax) * 50;
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

    private bool RunLevelTimer()
    {
        levelTimer -= Time.deltaTime;
        if (levelTimer <= 0)
        {
            return true;
        }
        return false;
    }

    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = 0)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}
