using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlotCreator3D : MonoBehaviour
{
    [SerializeField]
    private Slot slot;
    [SerializeField]
    private Vector2Int gridSize;
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Vector2 gap;
    [SerializeField]
    private Vector2 offset;
    

    private bool workOnce;



    void Start()
    {
        if (workOnce)
        {
            return;
        }

        gameController.slotDictionary = new Dictionary<int, Slot>();
        var cnt = 0;
        var loadCnt = 0;
        
        var settings = SettingsController.Instance;
        settings.LoadSettings();
        var itemIds = settings._userSettingsSO.UserSettings.itemId;
        var slotIds = settings._userSettingsSO.UserSettings.slotId;
        
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                var slotInstance = Instantiate(slot, new Vector3((y * gap.y) + offset.y, 0, (x * gap.x)+offset.x), Quaternion.identity, transform);
                slotInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
                slotInstance.id = cnt;
                gameController.slots.Add(slotInstance);
                gameController.slotDictionary.Add(cnt, slotInstance);
                
         
                if (slotIds.Contains(cnt))
                {
                    var desiredItem = itemIds[loadCnt];
                    slotInstance.CreateItem3D(desiredItem);
                    loadCnt++;
                }
                
                cnt++;
            }
        }
        workOnce = true;
    }

}
