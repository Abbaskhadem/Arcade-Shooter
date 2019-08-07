using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables
    Rigidbody2D rb;
    public int Damage;
    [SerializeField] float Speed;
    private Rigidbody2D objectRigidbody;
    [SerializeField] private GameObject BulletEffect;
#endregion
#region Bullet Methods

void Start()
{
    
    BulletEffect = Instantiate(BulletEffect);
}
void Update()
    {
        objectRigidbody = transform.GetComponent<Rigidbody2D>();
        objectRigidbody.velocity = transform.up * Speed;
    }

void OnTriggerEnter2D(Collider2D col)
{
    if (col.gameObject.tag == "Enemy" && gameObject.tag=="Player Bullet")
    {
        BulletEffect.transform.position = col.transform.position;
        BulletEffect.SetActive(true);
        gameObject.SetActive(false);
        col.GetComponent<Enemy_SpaceShip>().TakeDamage(Damage);
    }
    if (col.gameObject.tag == "Player" && gameObject.tag=="Enemy Bullet" )
    {
       
        gameObject.SetActive(false);
        col.GetComponent<Main_SpaceShip>().TakeDamage(Damage);
    }
}
#endregion
}
