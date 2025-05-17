using UnityEngine;
using System.Collections;

public class PlayerBattleController : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    public bool playerTurn = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Transform childTransform = transform.Find("Walking");
        GameObject childGameObject = childTransform.gameObject;
        animator = childGameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (playerTurn)
            StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        animator.SetBool("Punching", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Punching", false);
        playerTurn = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemies[0].GetComponent<EnemyBattleController>().enemyTurn = true;
        enemies[0].GetComponent<EnemyBattleController>().Attack();
    }
}
