using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : Singleton<UpdateManager>
{
    private List<IUpdate> updates = new List<IUpdate>();

    private bool inited = false;

    private void Register(IUpdate update)
    {
        if (updates.Contains(update))
        {
            Debug.LogWarning(string.Format("IUpdate {0} is already registered!", update.GetTransform().name));
            return;
        }

        updates.Add(update);
    }

    private void Unregister(IUpdate update)
    {
        if (!updates.Contains(update))
        {
            Debug.LogWarning(string.Format("Trying to unregister {0}, but it is not registered!", update.GetTransform().name));
            return;
        }

        updates.Remove(update);
    }

    private void OnEnable()
    {
        if (!inited)
        {
            Events.Instance.registerUpdate.AddListener(Register);
            Events.Instance.unregisterUpdate.AddListener(Unregister);
            inited = true;
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i] == null)
            {
                Debug.LogError("An IUpdate was destroyed yet not unregistered! Unregistering now...");
                Unregister(updates[i]);
                continue;
            }

            updates[i].FistUpdate();
        }

        for (int i = 0; i < updates.Count; i++)
            updates[i].SecondUpdate();

        for (int i = 0; i < updates.Count; i++)
            updates[i].ThirdUpdate();
    }
}
