using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float speed = 5f; // Скорость движения препятствия
    public float upperLimit = 2f; // Верхняя граница движения препятствия
    public float lowerLimit = -2f; // Нижняя граница движения препятствия

    private bool movingUp = true; // Флаг движения препятствия вверх
    private float initialY; // Начальное положение по оси Y

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        // Перемещение препятствия
        float movement = speed * Time.deltaTime;

        if (movingUp)
        {
            transform.Translate(Vector3.up * movement);
            if (transform.position.y - initialY >= upperLimit)
                movingUp = false;
        }
        else
        {
            transform.Translate(Vector3.down * movement);
            if (transform.position.y - initialY <= lowerLimit)
                movingUp = true;
        }

        // Ограничение препятствия в пределах вертикальных границ
        float clampedY = Mathf.Clamp(transform.position.y, initialY + lowerLimit, initialY + upperLimit);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }
}
