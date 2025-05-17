using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerBattleController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI playerHealthText;
    private Animator animator;
    public bool isPlayerTurn = true;
    public int currentHealth = 20;

    void Start()
    {
        Transform childTransform = transform.Find("Walking");
        GameObject childGameObject = childTransform.gameObject;
        animator = childGameObject.GetComponent<Animator>();
    }

    void Update()
    {
        playerHealthText.text = $"{currentHealth}";
    }

    public void Defend(int damage, string damageType)
    {
        currentHealth -= damage;
    }

    public void Attack()
    {
        if (isPlayerTurn)
            StartCoroutine(PlayAttackAnimation());
    }

    private IEnumerator PlayAttackAnimation()
    {
        animator.SetBool("Punching", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Punching", false);

        isPlayerTurn = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies[0].GetComponent<EnemyBattleController>().Defend(5, "physical");
        enemies[0].GetComponent<EnemyBattleController>().isEnemyTurn = true;
    }
}

