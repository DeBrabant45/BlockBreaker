using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip crack;
    public Sprite[] hitsSprites;
    public GameObject smoke;

    public static int breakableCount = 0;
    private int timesHit;
    private bool isBreakable;
    private LevelManager levelManager;

    // Use this for initialization
    void Start ()
    {
        timesHit = 0;
        isBreakable = (this.tag == "Breakable");
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        // Keep track of breakable bricks
        if (isBreakable)
        {
            breakableCount++;
        }
        
    }


    void OnCollisionEnter2D(Collision2D colision)
    {
        AudioSource.PlayClipAtPoint (crack, transform.position);
        if (isBreakable)
        {
            HandleHits();   
        }
    }

    void HandleHits ()
    {
        timesHit++;
        int maxHits = hitsSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            breakableCount--;
            levelManager.BrickDestroyed();
            PuffSmoke();
            Destroy(gameObject);
        }
        else
        {
            LoadSprites();
        }
    }

    void PuffSmoke ()
    {
        GameObject smokePuff = Instantiate(smoke, transform.position, Quaternion.identity) as GameObject;
        smokePuff.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void LoadSprites()
    {
        int spriteIndex = timesHit - 1;
        if (hitsSprites[spriteIndex] != null)
        {
            this.GetComponent<SpriteRenderer>().sprite = hitsSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Brink sprite missing");
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    // TODO remove this method once we can really win!
    void SimulateWin()
    {
        levelManager.LoadNextLevel();
    }
}
