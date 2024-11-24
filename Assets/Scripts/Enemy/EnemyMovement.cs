using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnemyMovement : NetworkBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    private bool isDead = false;

    public float detectionRange = 10f; // Радиус поиска
    private Rigidbody2D rb; // Ригидбоди для физики
    private Collider2D coll; // Коллайдер для обнаружения (не используется в новой логике)

    void Start()
    {
        if (isServer)
        {
            rb = GetComponent<Rigidbody2D>();
            coll = GetComponent<Collider2D>();
        }
    }

    void Update()
    {
        if (isDead) return;

        // Если игрок найден, двигаемся к нему
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, enemyData.moveSpeed * Time.deltaTime);
        }
        else
        {
            // Ищем ближайшего игрока в радиусе
            FindClosestPlayer();
        }
    }

    // Ищем ближайшего игрока по компоненту PlayerMovement
    private void FindClosestPlayer()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>(); // Найдём все объекты с компонентом PlayerMovement
        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (PlayerMovement playerMovement in players)
        {
            float distance = Vector2.Distance(transform.position, playerMovement.transform.position);
            if (distance < closestDistance && distance <= detectionRange) // Проверяем, в радиусе ли
            {
                closestDistance = distance;
                closestPlayer = playerMovement.transform;
            }
        }

        // Если найден ближайший игрок в радиусе, запоминаем его
        if (closestPlayer != null)
        {
            player = closestPlayer;
        }
    }

    // Когда здоровье врага уходит в 0, он умирает
    [Command] // Этот метод будет вызываться с клиента, но выполняться на сервере
    public void CmdDie()
    {
        // Выполняем действия, связанные с уничтожением врага, только на сервере
        Die();
    }

    [Server] // Эта функция вызывается только на сервере
    private void Die()
    {
        if (isDead) return;

        isDead = true;

        // Уничтожаем объект на сервере
        RpcDie();
        Destroy(gameObject); // Удаляем объект на сервере
    }

    // Уничтожение врага на клиентах
    [ClientRpc] // Этот метод будет вызываться на всех клиентах
    private void RpcDie()
    {
        if (isDead) return;

        isDead = true;

        // Здесь мы можем выполнить дополнительные действия на клиенте, такие как анимация смерти
        // Например, проиграть анимацию смерти или сделать визуальные эффекты

        // Удаление объекта на клиенте, если оно ещё не произошло
        Destroy(gameObject); // Удаляем объект на клиенте
    }
}
