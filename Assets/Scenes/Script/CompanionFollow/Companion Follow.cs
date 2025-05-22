using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NewMonoBehaviourScript : MonoBehaviour
{

    public Transform player;
    public float followSpeed;
    public float followDistance;


        void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            Vector3 targetPosition = player.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
    private int CompcurrentHealth;
    public int CompmaxHealth;
    private bool isInvicible = false;
    public float invicibilityDuration = 1.5f;

    void Start()
    {
        CompcurrentHealth = CompmaxHealth; //companion HP
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Companion"), LayerMask.NameToLayer("Player"));
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvicible)
        {
            TakeDamage();
            Debug.Log("Companion Hit!");
        }
    }
    void TakeDamage()
    {
        //returns the higher of the two values
        CompcurrentHealth = Mathf.Max(CompcurrentHealth -1, 0);
        if (CompcurrentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }else{
            StartCoroutine(InvicibilityCooldown()); //Mulai cooldown
        }
    }
    IEnumerator InvicibilityCooldown()
    {
        isInvicible = true; //companion kebal sementara
        yield return new WaitForSeconds(invicibilityDuration);
        isInvicible = false;
    }
}
