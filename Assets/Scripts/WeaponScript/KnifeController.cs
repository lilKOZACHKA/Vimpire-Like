using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponsController
{
    public float spreadAngle = 15f; // Угол отклонения ножей (градусы)

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        // Количество ножей
        int knifeCount = 3;
        float angleStep = spreadAngle / (knifeCount - 1); // Расчёт шага угла
        float startAngle = -spreadAngle / 2; // Начальный угол

        for (int i = 0; i < knifeCount; i++)
        {
            // Рассчитываем текущий угол
            float currentAngle = startAngle + (angleStep * i);

            // Поворачиваем направление движения игрока
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * pm.lastMovedVector;

            // Создаём нож
            GameObject spawnedKnife = Instantiate(weaponData.Prefab);
            spawnedKnife.transform.position = transform.position;

            // Передаём ножу направление
            spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(direction);
        }
    }
}