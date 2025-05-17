using UnityEngine;
using System.Collections;

public class EnemyBattleController : MonoBehaviour
{
    public bool enemyTurn = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        if (enemyTurn)
            StartCoroutine(AttackAnimation());
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(4f);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Enemy attacks player");
        enemyTurn = false;
        players[0].GetComponent<PlayerBattleController>().playerTurn = true;
    }
}
