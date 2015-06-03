using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour{
    private Vector3 screenSize;
    private Vector3 offscreen;
    private BulletPattern pattern;
    private bool ready;

    public float respawnTime;
    public float powerupTime;

    public void Start(){
        Debug.Log("foo");
        ready = false;
        respawnTime = 10.0f;
        powerupTime = 4.0f;
        Camera mainCamera = GameObject.Find(
                "Main Camera").GetComponent<Camera>() as Camera;
        screenSize = mainCamera.ScreenToWorldPoint(new Vector3(
                    Screen.width,
                    Screen.height, 0)) * 0.8f;
        pattern = gameObject.AddComponent<SpiralWavePattern>();
        offscreen = new Vector3(2000, 2000, 0);
        transform.position = offscreen;
        Debug.Log("bar");
        StartCoroutine(Respawn());
    }

    public void OnTriggerEnter2D(Collider2D other){
        Debug.Log("OnTriggerEnter");
        if(ready){
            Ship ship = other.gameObject.GetComponent<Ship>();
            if(ship != null){
                Debug.Log("adding pattern");
                StartCoroutine(AddPattern(ship));
            }
        }
    }

    private IEnumerator AddPattern(Ship ship){
        ship.PowerupPattern(pattern);
        ship.PowerupOn();
        ready = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(powerupTime);
        ship.PowerupOff();
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn(){
        Debug.Log("pubup");
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("mugum");
        transform.position = new Vector3(
                ((Random.value * 2f) - 1f) * screenSize.x,
                ((Random.value * 2f) - 1f) * screenSize.y,
                0);
        ready = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
