using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public enum MushroomState
    {
        Start,
        Reloading,
        Ready,
        Jump,
        Attack,
        Hiding
    }

    private GameObject player;
    private float startY;
    private float vspeed;
    private float timer;
    [SerializeField] float Damage = 1f;
    MushroomState state = MushroomState.Start;
    DragonBones.UnityArmatureComponent anim;

    Renderer m_Renderer;
    public ParticleSystem particleObject;

    [SerializeField]
    float SeeDistance = 3f;
    [SerializeField]
    float DamageDistance = 4f;
    [SerializeField]
    float HidingSpeed = 0.6f;
    [SerializeField]
    float HidingDepth = 0.8f;
    [SerializeField]
    float ReadyDepth = 0.5f;
    [SerializeField]
    float ReloadingTimer = 5f;
    [SerializeField]
    float AttackTimer = 3f;
    [SerializeField]
    float JumpSpeed = 15f;


    // Start is called before the first frame update
    void Start()
    {
        // Ставим себе тег
        this.tag = "Enemy";
        // Вытягиваем игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player");
        // Запоминаем изначальную позицию
        startY = transform.position.y;
        // Прячемся под землю и говорим что мы готовы к прыжку
        transform.Translate(Vector3.down * ReadyDepth, Space.World);
        state = MushroomState.Ready;
        //particleObject.Stop();

        m_Renderer = GetComponent<Renderer>();
        anim = GetComponent<DragonBones.UnityArmatureComponent>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void AudioSystem(string nameOfClip)
    {
        var a = GetComponents<AudioSource>().FirstOrDefault(s => s.clip.name == nameOfClip);

        if (a != null)
        {
            a.volume = PlayerPrefs.GetFloat("volume");
            a.Play();
        }
        else
        {
            Debug.Log("Звук не найден");
        }
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case MushroomState.Ready:
                anim.animation.Play("statik");
                // Если видим игрока - прыгаем
                if ((player.transform.position - this.transform.position).magnitude < SeeDistance)
                {
                    AudioSystem("Mushroom_grow");
                    state = MushroomState.Jump;
                    vspeed = JumpSpeed;
                }
                break;
            case MushroomState.Jump:
                // Летим
                anim.animation.Play("ia_tutachki");
                transform.Translate(Vector3.up * vspeed * Time.fixedDeltaTime, Space.World);
                // Если не под землёй - гравитация
                if (transform.position.y > startY)
                {
                    vspeed -= 0.5f;
                }
                // Иначе если уже упали под землю - переходим в атаку
                else if (vspeed < 0)
                {
                    particleObject.Play();
                    // Встаём куда надо
                    transform.position.Set(transform.position.x, startY, transform.position.z);
                    // Атакуем
                    anim.animation.Play("poop");
                    AudioSystem("Mushroom_atack");
                    state = MushroomState.Attack;

                    timer = AttackTimer;
                }
                break;
            case MushroomState.Attack:
                // Ждём время атаки

                if ((player.transform.position - this.transform.position).magnitude < DamageDistance)
                {
                    player.GetComponent<MovePlayer>().health -= 3f;
                }

                timer -= Time.fixedDeltaTime;
                // По истечению таймера - прячемся
                if (timer <= 0)
                {
                    state = MushroomState.Hiding;
                }
                break;
            case MushroomState.Hiding:
                // Уползаем под землю

                transform.Translate(Vector3.down * HidingSpeed * Time.fixedDeltaTime, Space.World);

                // Когда уползли на нужную глубину - перезарядка
                if (transform.position.y < (startY - HidingDepth))
                {
                    anim.animation.Play("poka");
                    anim.animation.Play("statik");
                    state = MushroomState.Reloading;
                    timer = ReloadingTimer;
                }
                break;
            case MushroomState.Reloading:
                // Выжидаем таймер перезарядки
                if (timer > 0)
                {
                    timer -= Time.fixedDeltaTime;
                    //if (timer > 0)
                }
                else // Когда истёк - поднимаемся до высоты Ready
                {
                    transform.Translate(Vector3.up * HidingSpeed * Time.fixedDeltaTime, Space.World);
                    if (transform.position.y > (startY - ReadyDepth))
                        state = MushroomState.Ready;
                }
                break;
            default:
                break;
        }
    }
}
