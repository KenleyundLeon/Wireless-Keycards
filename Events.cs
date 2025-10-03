using Interactables.Interobjects.DoorUtils;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using InventorySystem.Items.Keycards;

namespace Wireless_Keycards;
public class Events : CustomEventsHandler
{
    public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
    {
        if (ev.Door.IsLocked) return;
        if (ev.Door.Permissions == DoorPermissionFlags.None)
        {
            ev.IsAllowed = true;
            ev.CanOpen = true;
            return;
        }

        if (ev.Player.CurrentItem != null && ev.Player.CurrentItem.Category == ItemCategory.Keycard && Main.Instance.Config.SkipIfKeycardInHand)
            return;

        if (ev.Player.Inventory?.UserInventory?.Items == null)
            return;

        foreach (var item in ev.Player.Inventory.UserInventory.Items.Values)
        {
            if (item is not KeycardItem keycard) continue;

            var permissions = keycard.GetPermissions(ev.Player as IDoorPermissionRequester);
            if (ev.Door.Base.RequiredPermissions.CheckPermissions(permissions))
            {
                ev.IsAllowed = true;
                ev.CanOpen = true;
                return;
            }
            else continue;
        }
    }

    public override void OnPlayerInteractingLocker(PlayerInteractingLockerEventArgs ev)
    {
        if (ev.Player.CurrentItem != null && ev.Player.CurrentItem.Category == ItemCategory.Keycard && Main.Instance.Config.SkipIfKeycardInHand)
            return;

        if (ev.Player.Inventory?.UserInventory?.Items == null)
            return;

        foreach (var item in ev.Player.Inventory.UserInventory.Items.Values)
        {
            if (item is not KeycardItem keycard) continue;
            if (keycard.GetPermissions(ev.Player as IDoorPermissionRequester).HasFlagAll(ev.Chamber.RequiredPermissions))
            {
                ev.IsAllowed = true;
                ev.CanOpen = true;
                return;
            }
            else continue;
        }
    }
    public override void OnPlayerInteractingGenerator(PlayerInteractingGeneratorEventArgs ev)
    {
        if (ev.Player.CurrentItem != null && ev.Player.CurrentItem.Category == ItemCategory.Keycard && Main.Instance.Config.SkipIfKeycardInHand)
            return;


        if (ev.Player.Inventory?.UserInventory?.Items == null)
            return;

        foreach (var item in ev.Player.Inventory.UserInventory.Items.Values)
        {
            if (item is not KeycardItem keycard) continue;
            if (keycard.GetPermissions(ev.Player as IDoorPermissionRequester).HasFlagAll(ev.Generator.RequiredPermissions))
            {
                ev.IsAllowed = true;
                ev.Generator.IsUnlocked = true;
                return;
            }
            else continue;
        }
    }

    public override void OnPlayerUnlockingWarheadButton(PlayerUnlockingWarheadButtonEventArgs ev)
    {
        if (ev.Player.CurrentItem != null && ev.Player.CurrentItem.Category == ItemCategory.Keycard && Main.Instance.Config.SkipIfKeycardInHand)
            return;

        if (ev.Player.Inventory?.UserInventory?.Items == null)
            return;

        foreach (var item in ev.Player.Inventory.UserInventory.Items.Values)
        {
            if (item is not KeycardItem keycard) continue;
            if (keycard.GetPermissions(ev.Player as IDoorPermissionRequester).HasFlagAll(DoorPermissionFlags.AlphaWarhead))
            {
                ev.IsAllowed = true;
                return;
            }
            else continue;
        }
    }
}
