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

    private void Update()
    {
        if (finish)
            return;

        _lastFrameVelocity = _rigidBody.velocity;

        if (_rigidBody.velocity.magnitude < MaxVelocity)
        {
            _rigidBody.velocity = _rigidBody.velocity.normalized * MaxVelocity;
        }

        // Уберите этот блок кода, чтобы шар не получал случайную скорость при нулевой скорости
        // else if (_rigidBody.velocity.magnitude > MaxVelocity)
        // {
        //     _rigidBody.velocity = _rigidBody.velocity.normalized * MaxVelocity;
        // }

        if (_rigidBody.velocity.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_rigidBody.velocity.normalized, Vector3.up);
            transform.rotation = targetRotation;
        }
        else
        {
            // Уберите следующие строки, которые задают случайное направление
            // float randomX = Random.Range(-1.0f, 1.0f);
            // float randomZ = Random.Range(-1.0f, 1.0f);
            // Vector3 randomDirection = new Vector3(randomX, 0.0f, randomZ).normalized;
            // _rigidBody.velocity = randomDirection * MaxVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<ExitBallPlatform>())
        {
            finish = true;
            _rigidBody.isKinematic = true;
            Destroy(GetComponent<PortalTraveler>());
            Destroy(GetComponent<SphereCollider>());
            Destroy(GetComponent<MeshRenderer>());
            Destroy(transform.GetChild(0).gameObject);
            
            collision.collider.GetComponent<ExitBallPlatform>().Finish();
            return;
        }

        if (portalTraveler.PenetratingPortal == null)
        {
            Bounce(collision.contacts[0].normal);
            bounceCount++;
            Instantiate(effectWall, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));

            if (bounceCount > 4)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Bounce(Vector3 collisionNormal)
    {
        var direction = Vector3.Reflect(_lastFrameVelocity.normalized, collisionNormal);

        _rigidBody.velocity = direction * MaxVelocity;
    }
}