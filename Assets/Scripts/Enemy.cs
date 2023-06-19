using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private bool isDead;
    public float attackRate = .2f;
    private Vector3 position;
    public GameObject player;
    public ParticleSystem deathFX;
    public DynamicTextData textData;
    private Transform target;
    private NavMeshAgent agent;
    public Animator anim;
    public bool isStatic = false;
    IEnumerator currentDamageCoroutine;
    public AudioSource audioSource;
    public AudioClip damageSFX;
    public AudioClip hitSFX;
    public AudioClip deathSFX;
    Ragdoll ragdoll;
    public float HP = 10f;
    public float damage = 5f;
    public float prize = 10f;
    [SerializeField] private Vector3 offset = new Vector3(0,0,0);


    private void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<Ragdoll>();
        target = PlayerManager.instance.player.transform;
        currentDamageCoroutine = DamagePlayer();
        HP = Spawner.instance.HP;
        damage = Spawner.instance.damage;
        prize = Spawner.instance.prize;

        var rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidBody in rigidBodies)
        {
            HitBox hitBox = rigidBody.gameObject.AddComponent<HitBox>();
            hitBox.enemy = this;
        }
    }

    private void Update()
    {
        agent.SetDestination(target.position);
        FaceTarget();
        position = GetComponent<Enemy>().transform.TransformPoint(Vector3.zero);
        if (!isStatic)    
            anim.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void Damage(int damage)
    {
        if (HP > 0)
        {
            HP -= damage;
            audioSource.PlayOneShot(damageSFX);
        }
        if (HP <= 0)
            Death();
    }

    private void Death()
    {
        if (isDead)
            return;
        GivePrize();
        audioSource.PlayOneShot(deathSFX);
        Instantiate(deathFX, position + offset, Quaternion.Euler(0,0,0));
        deathFX.Play();
        Destroy(gameObject);
        isDead = true;
    }

    private void GivePrize()
    {
        Weapon.score += (int)prize;
        Weapon.weapon.totalScore += (int)prize;
        PlayerManager.instance.Heal(prize/5);
    }

    IEnumerator DamagePlayer()
    {
        while (true)
        {
            PlayerManager.instance.Damage(damage);
            audioSource.PlayOneShot(hitSFX);
            yield return new WaitForSeconds(attackRate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            StartCoroutine(currentDamageCoroutine); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopCoroutine(currentDamageCoroutine);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation,Time.deltaTime * 5f);
    }
}