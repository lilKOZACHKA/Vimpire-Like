using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponsController
{
    public float spreadAngle = 15f; // ���� ���������� ����� (�������)

    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();

        // ���������� �����
        int knifeCount = 3;
        float angleStep = spreadAngle / (knifeCount - 1); // ������ ���� ����
        float startAngle = -spreadAngle / 2; // ��������� ����

        for (int i = 0; i < knifeCount; i++)
        {
            // ������������ ������� ����
            float currentAngle = startAngle + (angleStep * i);

            // ������������ ����������� �������� ������
            Vector2 direction = Quaternion.Euler(0, 0, currentAngle) * pm.lastMovedVector;

            // ������ ���
            GameObject spawnedKnife = Instantiate(weaponData.Prefab);
            spawnedKnife.transform.position = transform.position;

            // ������� ���� �����������
            spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(direction);
        }
    }
}