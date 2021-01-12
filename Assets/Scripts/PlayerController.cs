using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerIndicator;
    private AudioSource audioSource;
    public AudioClip collisionSound;
    public AudioClip bonusSound;
    public GameObject gameOverWindow;
    public float speed;
    public bool hasPowerup;
    public float powerupStrength = 10;
    public bool isGameOver = false;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        GameOver();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            audioSource.PlayOneShot(bonusSound);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        powerIndicator.SetActive(false);
        hasPowerup = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("collision with" + collision.gameObject.name + "hasPowerUp :" + hasPowerup);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
        audioSource.PlayOneShot(collisionSound);
    }

    public void GameOver()
    {
        if (transform.position.y < -15)
        {
            isGameOver = true;
            audioSource.Stop();
            gameOverWindow.SetActive(true);

        }
    }
}
