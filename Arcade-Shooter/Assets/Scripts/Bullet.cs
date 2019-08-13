using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

     public bool Child;
    Rigidbody2D rb;
    public int Damage;
    [SerializeField] float Speed;
    private Rigidbody2D objectRigidbody;
    [SerializeField] private ParticleSystem BulletEffect;
#endregion
#region Bullet Methods

void Start()
{
//    if(gameObject.tag=="Player Bullet")
//    BulletEffect = Instantiate(BulletEffect);
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
        col.GetComponent<Animator>().SetTrigger("GotHit");
        
       if(ParticleManager._Instance.tempParticle.isPlaying)
          // ParticleManager._Instance.tempParticle.Stop();
        ParticleManager._Instance.tempParticle.transform.position = new Vector3(col.transform.position.x,col.transform.position.y,-0.26f);
        ParticleManager._Instance.tempParticle.Play();
        if (!Child)
        {
            gameObject.SetActive(false);  
        }
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
