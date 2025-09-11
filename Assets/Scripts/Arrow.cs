using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    void Start()
    {
        // Создаем луч (Ray) в направлении форварда
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;

        // Проверяем, попал ли луч во что-то
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Если попал, перемещаем стрелку немного от стены
            Vector3 newPosition = hitInfo.point - transform.forward * 0.01f;
            transform.position = newPosition;
        }
    }
}
