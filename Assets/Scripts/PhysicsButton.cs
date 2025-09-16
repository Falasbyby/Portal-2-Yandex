using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{   
    public GameObject parentLine;
    public Transform buttonTop;
    public Transform buttonLowerLimit;
    public Transform buttonUpperLimit;
    public float moveSpeed = 5f;
    public bool isPressed;
    private bool prevPressedState;
    private AudioSource pressedSound;
    public UnityEvent onPressed;
    public UnityEvent onReleased;

    public LineRenderer lineRenderer;

    private Door door;
    private bool hasBoxOnButton = false;
    private Box boxOnButton;

    // Start is called before the first frame update
    void Start()
    {   
        lineRenderer = parentLine.GetComponentInChildren<LineRenderer>();
        door = GetComponentInParent<Door>();
        if (door)
        {
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0,transform.position + new Vector3(0,0.3f,0));
            lineRenderer.SetPosition(1,door.doorLine.transform.position);
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        pressedSound = GetComponent<AudioSource>();
        
        // Устанавливаем начальную позицию кнопки
        buttonTop.position = buttonUpperLimit.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Определяем целевую позицию кнопки
        Vector3 targetPosition;
        bool shouldBePressed = false;
        
        // Если есть куб на кнопке и он не взят - кнопка нажата
        if (hasBoxOnButton && boxOnButton != null && !boxOnButton.grab)
        {
            targetPosition = buttonLowerLimit.position;
            shouldBePressed = true;
        }
        else
        {
            targetPosition = buttonUpperLimit.position;
            shouldBePressed = false;
        }
        
        // Плавно перемещаем кнопку к целевой позиции
        buttonTop.position = Vector3.Lerp(buttonTop.position, targetPosition, moveSpeed * Time.deltaTime);
        
        // Проверяем состояние нажатия
        isPressed = shouldBePressed;

        // Вызываем события при изменении состояния
        if(isPressed && prevPressedState != isPressed)
            Pressed();
        if(!isPressed && prevPressedState != isPressed)
            Released();
    }

    void Pressed(){
        prevPressedState = isPressed;
        pressedSound.Play();
        onPressed.Invoke();
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.green;
    }

    void Released(){
        prevPressedState = isPressed;
       // releasedSound.pitch = Random.Range(1.1f, 1.2f);
      //  releasedSound.Play();
        onReleased.Invoke();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
    }
    
    // Обнаружение входа куба в триггер
    void OnTriggerStay(Collider other)
    {
        Box box = other.GetComponent<Box>();
        if (box != null && !box.grab)
        {
            hasBoxOnButton = true;
            boxOnButton = box;
        }
    }
    
    // Обнаружение выхода куба из триггера
    void OnTriggerExit(Collider other)
    {
        Box box = other.GetComponent<Box>();
        if (box != null && box == boxOnButton )
        {
            hasBoxOnButton = false;
            boxOnButton = null;
        }
    }
}
