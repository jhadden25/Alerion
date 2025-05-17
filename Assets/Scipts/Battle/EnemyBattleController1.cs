using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyBattleController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    public bool isEnemyTurn = false;
    private int currentHealth = 10;

    void Start()
    {
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
        healthText.text = $"{currentHealth}";
        if (isEnemyTurn)
        {
            isEnemyTurn = false;
            StartCoroutine(AttackAnimation());
        }
    }

    public void Defend(int damage, string type)
    {
        currentHealth -= damage;
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(4f);
        var players = GameObject.FindGameObjectsWithTag("Player");
        players[0].GetComponent<PlayerBattleController>().Defend(5, "physical");
        isEnemyTurn = false;
        players[0].GetComponent<PlayerBattleController>().isPlayerTurn = true;
    }
}

