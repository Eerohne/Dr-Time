using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tutorial Events
public class TutorialEventScript : MonoBehaviour
{

    public Canvas dialogueCanva;
    public GameObject panelCanvas;
    public PlayerSystem player;
    private bool gradient = false;


    void Update()
    {
        if (gradient && Input.GetKeyDown(KeyCode.Escape))
        {
            panelCanvas.SetActive(true);
            
        }
    }

    public void EndEventTriggerWASD()
    {
        player.SetFree(true);
        GameObject.Find("DialogueTrigger1_Continue").GetComponent<DialogueTrigger>().enabled = true;
    }

    public void EndEventTriggerWASD2()
    {
        GameObject.Find("RoomTransition1").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge1").GetComponent<BridgeScript>().enabled = true;
    }

    public void EndEventTriggerMonster()
    {
        GameObject.Find("RoomTransition2").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge3").GetComponent<BridgeScript>().enabled = true;
    }

    public void PauseExplanationEnd()
    {
        gradient = true;

        GameObject.Find("Trigger_AfterPause").GetComponent<DialogueTrigger>().enabled = true;
        dialogueCanva.sortingOrder = 12;
    }

    public void PauseExplanationEnd2()
    {
        gradient = false;
        panelCanvas.SetActive(false);

        GameObject.Find("RoomTransition3").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge5").GetComponent<BridgeScript>().enabled = true;
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
        GameObject.Find("Bridge7").GetComponent<BridgeScript>().enabled = true;
    }

    public void CoinExplanation()
    {
        GameObject.Find("RoomTransition5").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge9").GetComponent<BridgeScript>().enabled = true;
        //player.SetFree(true);
    }

    public void HealthPotionExplanation()
    {
        player.SetFree(true);
    }
    
    public void DrinkPotionExplanation()
    {
        GameObject.Find("RoomTransition6").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge11").GetComponent<BridgeScript>().enabled = true;
    }

    public void DamagePotionExplanation()
    {
        GameObject.Find("RoomTransition7").GetComponent<RoomTransition>().triggerZone.enabled = false;
        //player.SetFree(true);
        GameObject.Find("Bridge13").GetComponent<BridgeScript>().enabled = true;
    }

    public void KnifeExplanation()
    {
        GameObject.Find("RoomTransition8").GetComponent<RoomTransition>().triggerZone.enabled = false;
        GameObject.Find("Bridge15").GetComponent<BridgeScript>().enabled = true;
        //player.SetFree(true);
    }
    
    public void PortalExplanation()
    {
        player.SetFree(true);
        //GameObject.FindGameObjectWithTag("Portal").GetComponent<EnterScene>().enabled = true;
    }
}
