using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Reflection;


public class PlayerMoviment : MonoBehaviour
{
    public Button clickLeft;
    public Button clickRigth;

    private Player player;
    private Animator playerAnimator;

    void Start()
    {
        addAllListeners();
    }

    private void addAllListeners()
    {
        Button btn1 = clickLeft.GetComponent<Button>();
        Button btn2 = clickRigth.GetComponent<Button>();

        AddEventTrigger(clickLeft, "OnPointerDown", EventTriggerType.PointerDown, new object[] { 1 });
        AddEventTrigger(clickLeft, "OnPointerUP", EventTriggerType.PointerUp);

        AddEventTrigger(clickRigth, "OnPointerDown", EventTriggerType.PointerDown, new object[] { -1 });
        AddEventTrigger(clickRigth, "OnPointerUP", EventTriggerType.PointerUp);
    }

    private void AddEventTrigger(Button _button, string action, EventTriggerType _event, params object[] objects)
    {
        EventTrigger trigger = _button.gameObject.AddComponent<EventTrigger>();
        var pointerDown = new EventTrigger.Entry();
        System.Type ourType = this.GetType();

        MethodInfo fnc = ourType.GetMethod(action, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var arguments = objects;

        pointerDown.eventID = _event;

        pointerDown.callback.AddListener((e) => { fnc.Invoke(this, arguments); });
        trigger.triggers.Add(pointerDown);
    }

    private void OnPointerDown(int teste)
    {
        playerMoveDir(teste);
    }

    private void OnPointerUP()
    {
        playerStopMove();
    }

    public void playerMoveDir(int _direction)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player.playerMoveDir = _direction;
        player.isWalking = true;
        playerAnimator.SetBool("playerMove", true);
    }

    public void playerStopMove()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        player.isWalking = false;
        playerAnimator.SetBool("playerMove", false);
    }
}
