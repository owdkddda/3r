using UnityEngine;

public class enemyfollow : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    public Transform player;
    public float followSpeed;
    public float followDistance;
    public GameObject hpBarGreen;
    public GameObject hpBarRed;
    public Vector3 hpBarOffset = new Vector3(0f, 1f, 0f);


    void Start()
    {
        currentHealth = maxHealth;

        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.SetActive(false); // hide the green bar initially
            hpBarRed.SetActive(false); //Hide the red bar initially
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
            Debug.Log("Hit");
        }
    }

    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroyed();
        }

        if (hpBarGreen!= null && hpBarRed != null)
        {
            hpBarGreen.SetActive(true); //show the green bar
            hpBarRed.SetActive(true);
            UpdateHpBar(); //update hp bar size
        }

        private void UpdateHpBar()
        {
            // uppdate the green bar's widht based on the current health
            float healthPercentage = (float)currentHealth / maxHealth;
            Vector3 scale = hpBarGreen.transform.localScale;
            scale.x = healthPercentage;
            hpBarGreen.transform.localScale = scale;
        }
    }
        void Update()
    {
        //Move toward the Companion
        transform.position = Vector3.MoveTowards(transform.position, companion.position);
        
        //Position the HP bar above the enemy
        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.transform.position = transform.position + hpBarOffset;
            hpBarRed.transform.position = transform.position + hpBarOffset;
        }
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            Vector3 targetPosition = player.position;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
