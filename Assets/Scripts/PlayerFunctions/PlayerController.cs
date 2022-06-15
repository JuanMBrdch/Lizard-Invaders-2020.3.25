using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject rocketPrefab;
    [Header("Variables")]
    [SerializeField] private float speed;
    [SerializeField] private Transform shootPoint;
    [HideInInspector] public float currentRocketCooldown;
    [HideInInspector] public float rocketCooldown;
    [HideInInspector] public float bulletCooldown;
    [HideInInspector] public HealthManagerScript healthController;
    public Action <float> OnRocketCooldownChanged;
    private Animator animator;
    private Rigidbody2D body;
    private float currentBulletCooldown;
    private int timer = 1000;
    public bool isInvulnerable;
    private bool cooldownIncreased = false;
    public bool P1;
    bool acelerating = false;
    Vector2 move;    

    //Enfriamiento de los misiles expresado en float para indicar por la UI
    public float GetCurrentMissileCooldown()
    {
        return (float) currentRocketCooldown / rocketCooldown;
    }

    private void Awake()
    {
        gameObject.SetActive(true);
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthController = GetComponent<HealthManagerScript>();
        healthController.ResetHealth();
        healthController.OnDie?.AddListener(OnDieListener);
        healthController.OnHealthChanged += OnHealthChangedHandler;
    }
    private void OnHealthChangedHandler(float currentHealth)
    {
        //Debug.Log($"CurrentHealth:{currentHealth}");
    }

    void Update()
    {     
        //Evento que notifica si el enfriamiento de los misiles ha cambiado
        OnRocketCooldownChanged?.Invoke(currentRocketCooldown);
        
        //Enfriamiento para que no se disparen balas de forma muy repetida
        currentBulletCooldown += Time.deltaTime;
        //Enfriamiento del cohete
        currentRocketCooldown += Time.deltaTime;
        
        Controllers();


        if (healthController.GetCurrentHealth() <= 0)
        {
            timer -= 1;
            if (timer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        //Asignacion de animaciones del jugador en base a su salud y su movimiento
        animator.SetFloat("health", healthController.GetCurrentHealth());
        animator.SetBool("is acelerating", acelerating);
    }
    
    //Funcion para que los jugadores disparen sus cohetes
    private void FireRocket()
    {
        if (P1)
        {
            rocketPrefab.GetComponent<P2EMPRocket>().P1 = true;
            Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
        }
        else
        {
            rocketPrefab.GetComponent<P2EMPRocket>().P1 = false;
            Instantiate(rocketPrefab, shootPoint.position, shootPoint.rotation);
        }
    }

    //Funcion para que los jugadores disparen sus balas
    private void FireBullet()
    {
        if (P1)
        {
            bulletPrefab.GetComponent<PlayerBulletScript>().P1 = true;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
        else
        {
            bulletPrefab.GetComponent<PlayerBulletScript>().P1 = false;
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        }
    }

    //Aqui se calcula y aplica el movimiento del jugador
    private void FixedUpdate()
    {
        body.velocity = move * speed;
    }

    //Controles del jugador 1 y 2
    void Controllers()
    {
        if (P1)
        {
            move.x = Input.GetAxisRaw("Horizontal");
            move.y = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(KeyCode.D))
            {
                acelerating = true;
            }
            if (Input.GetKey(KeyCode.A))
            {
                acelerating = false;
            }
            //Se agrega la condicion del timer para que no se puedan disparar muy repetidamente las balas
            if (Input.GetKey(KeyCode.Space) && currentBulletCooldown >= bulletCooldown)
            {
                FireBullet();
                currentBulletCooldown = 0f;                
            }
            //Se agrega la condicion del timer para que no se pueda abusar de los cohetes
            if (Input.GetKeyDown(KeyCode.R) && currentRocketCooldown >= rocketCooldown)
            {
                FireRocket();
                currentRocketCooldown = 0f;
            }
        }
        else
        {
            move.x = Input.GetAxisRaw("Horizontal2");
            move.y = Input.GetAxisRaw("Vertical2");

            if (Input.GetKey(KeyCode.RightArrow))
            {
                acelerating = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                acelerating = false;
            }
            if (Input.GetKey(KeyCode.RightControl) && currentBulletCooldown >= bulletCooldown)
            {
                FireBullet();
                currentBulletCooldown = 0f;
            }
            if (Input.GetKeyDown(KeyCode.RightShift) && currentRocketCooldown >= rocketCooldown)
            {
                FireRocket();
                currentRocketCooldown = 0f;
            }
        }
    }

    //Funcion que mata al jugador cuando se queda sin salud
    public void Kill()
    {
        if (healthController.currentHealth <= 0)
        {
            GameManager.instance.OnPlayerTrulyDie(P1);
        }        
    }
    
    //Metodo que se encarga de matar al jugador
    private void OnDieListener()
    {
        Kill();
    }

    
    //Metodo que aumenta la cadencia del jugador una vez obtenga un pickup de aumento de cadencia
    IEnumerator AumentarCadencia(float pickUpTimer)
    {
        var defaultBulletCooldown = bulletCooldown;
        bulletCooldown = bulletCooldown / 2;
        yield return new WaitForSeconds(pickUpTimer);
        bulletCooldown = defaultBulletCooldown;
        cooldownIncreased = false;
    }
    //Metodo que implementa el aumenta la cadencia de tiro del jugador
    public void FireRateRateUp(float pickUpTimer)
    {
        if (cooldownIncreased == false)
        {            
            cooldownIncreased = true;
            StartCoroutine(AumentarCadencia(pickUpTimer));
        }
    }


}
