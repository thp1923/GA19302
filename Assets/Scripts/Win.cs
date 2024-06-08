using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    CapsuleCollider2D col;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(YouWin());
            Destroy(gameObject, 2);
        }
    }

    IEnumerator YouWin()
    {
        yield return new WaitForSecondsRealtime(2f);
        FindObjectOfType<GameSession>().youWin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
