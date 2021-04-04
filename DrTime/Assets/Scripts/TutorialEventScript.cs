using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEventScript : MonoBehaviour
{
    public Canvas dialogueCanva;
    public PlayerSystem player;

    public void EndEventTriggerWASD()
    {
        player.SetFree(true);
        GameObject.Find("DialogueTrigger1_Continue").GetComponent<DialogueTrigger>().enabled = true;
    }

    public void EndEventTriggerWASD2()
    {
        GameObject.Find("RoomTransition1").GetComponent<RoomTransition>().triggerZone.enabled = false;
    }

    public void EndEventTriggerMonster()
    {
        GameObject.Find("RoomTransition2").GetComponent<RoomTransition>().triggerZone.enabled = false;
    }

    public void PauseExplanationEnd()
    {
        GameObject.Find("Trigger_AfterPause").GetComponent<DialogueTrigger>().enabled = true;
        dialogueCanva.sortingOrder = 11;
    }

    public void PauseExplanationEnd2()
    {
        GameObject.Find("RoomTransition3").GetComponent<RoomTransition>().triggerZone.enabled = false;
        dialogueCanva.sortingOrder = 0;
    }

    public void HeartExplanation()
    {
        GameObject.Find("Trigger_MonsterDemonstration").GetComponent<DialogueTrigger>().enabled = true;
        player.SetFree(true);
    }

    public void MonsterFailEvent()
    {
        GameObject.Find("SlimeSpawner").GetComponent<Spawner>().StartCoroutine("Spawn");
    }

    public void MonsterDeathEvent()
    {
        GameObject.Find("RoomTransition4").GetComponent<RoomTransition>().triggerZone.enabled = false;
    }

    public void CoinExplanation()
    {
        GameObject.Find("RoomTransition5").GetComponent<RoomTransition>().triggerZone.enabled = false;
        player.SetFree(true);
    }
    
    public void HealthPotionExplanation()
    {
        player.SetFree(true);
    }
    
    public void DrinkPotionExplanation()
    {
        GameObject.Find("RoomTransition6").GetComponent<RoomTransition>().triggerZone.enabled = false;
    }
    
    public void DamagePotionExplanation()
    {
        GameObject.Find("RoomTransition7").GetComponent<RoomTransition>().triggerZone.enabled = false;
        player.SetFree(true);
    }
    
    public void KnifeExplanation()
    {
        GameObject.Find("RoomTransition8").GetComponent<RoomTransition>().triggerZone.enabled = false;
        player.SetFree(true);
    }
    
    public void PortalExplanation()
    {
        player.SetFree(true);
    }
}
