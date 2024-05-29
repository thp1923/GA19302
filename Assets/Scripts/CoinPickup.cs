using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public GameObject CoinEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);

            Destroy(gameObject);

            GameObject Effect = Instantiate(CoinEffect, transform.position, transform.localRotation);

            Destroy(Effect, 3);

            FindObjectOfType<GameSession>().AddScore(10);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
