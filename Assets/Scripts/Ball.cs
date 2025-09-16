using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Xuwu.FourDimensionalPortals;

public class Ball : MonoBehaviour
{
    [SerializeField] private PortalTraveler portalTraveler;
    [SerializeField] private ParticleSystem effectWall;
    public float MaxVelocity = 20f;
    private Vector3 _lastFrameVelocity;
    private Rigidbody _rigidBody;

    public Rigidbody Rigidbody => _rigidBody;
    public bool finish = false;
    private int bounceCount = 0;


    private void Start()
    {
        StartCoroutine(TimerDeath());
    }

    private IEnumerator TimerDeath()
    {
        yield return new WaitForSeconds(20);

        if (!finish && gameObject != null)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = transform.forward.normalized * MaxVelocity;
    }

    private void FixedUpdate()
    {
        if (finish)
            return;

        _lastFrameVelocity = _rigidBody.velocity;

        // Всегда поддерживаем постоянную скорость
        if (_rigidBody.velocity.magnitude > 0)
        {
           // _rigidBody.velocity = _rigidBody.velocity.normalized * MaxVelocity;
        }
    }
    private void LateUpdate()
    {
        if (finish)
            return;
        if (_rigidBody.velocity.magnitude > 0)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * MaxVelocity;
        }
    }

    private void Update()
    {
        if (finish)
            return;

        // Поворот шарика в направлении движения (только визуальный эффект)
       /*  if (_rigidBody.velocity.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidBody.velocity.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        } */
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnter _ball");
        Debug.Log("1");
        if (collision.collider.GetComponent<ExitBallPlatform>())
        {
            Debug.Log("2");
            finish = true;
            _rigidBody.isKinematic = true;
            Destroy(GetComponent<PortalTraveler>());
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(transform.GetChild(0).gameObject);

            collision.collider.GetComponent<ExitBallPlatform>().Finish();
            return;
        }
        Debug.Log("3");
        if (portalTraveler.PenetratingPortal == null)
        {
            Debug.Log("4");
            Bounce(collision.contacts[0].normal);
            bounceCount++;
            Instantiate(effectWall, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

            if (bounceCount > 4)
            {
                Debug.Log("5");
                Destroy(gameObject);
            }
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);

        // Устанавливаем скорость сразу после отскока
        _rigidBody.velocity = direction * MaxVelocity;
    }
}