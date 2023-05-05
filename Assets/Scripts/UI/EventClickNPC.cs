using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventClickNPC : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    //private NpcScript npcScript;
    private NPCInfos npcInfos;
    private void Awake()
    {
        npcInfos = GetComponent<NPCInfos>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //NpcScript NPCInfos = eventData.selectedObject.GetComponent<NpcScript>();
        //npcInfos.ShowNPCInfos();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //NpcScript NPCInfos = eventData.selectedObject.GetComponent<NpcScript>();
        //npcInfos.CloseNPCInfos();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //NPCInfos NPCInfos = eventData.pointerCurrentRaycast.gameObject.GetComponent<NPCInfos>();
        npcInfos.slt();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
