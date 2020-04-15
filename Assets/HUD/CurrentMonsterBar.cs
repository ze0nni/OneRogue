using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bar))]
public class CurrentMonsterBar : MonoBehaviour
{
    private Bar bar;

    private Monsters.Monster currentMonster;

    private void Start()
    {
        this.bar = GetComponent<Bar>();
    }

    void Update()
    {
        if (null == currentMonster) {
            gameObject.SetActive(false);
            return;
        }
        if (false == currentMonster.enabled) {
            this.currentMonster = null;
            gameObject.SetActive(false);
            return;
        }

        bar.UpdateBar(currentMonster.damageable.LastFramePoints, currentMonster.damageable.MaxPoints, false);
    }

    public void UpdateCurrentMonster(Damageable damageable)
    {
        var monster = damageable.GetComponent<Monsters.Monster>();
        if (null == monster || this.currentMonster == monster) {
            return;
        }
        this.currentMonster = monster;

        if (null == this.currentMonster) {
            return;
        }

        gameObject.SetActive(true);
        bar.UpdateText(monster.name);
        bar.UpdateBar(currentMonster.damageable.LastFramePoints, currentMonster.damageable.MaxPoints, true);
    }
}
