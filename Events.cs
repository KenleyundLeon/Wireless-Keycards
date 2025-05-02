
using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items;
using KcItem = InventorySystem.Items.Keycards.KeycardItem;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;
using LabApi.Features.Console;

namespace Wireless_Keycard;

public class Events : CustomEventsHandler
{
    public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
    {
        if (ev.Player == null || ev.Door == null) return;

        Player ply = ev.Player;
        InventoryInfo invInfo = ply.Inventory.UserInventory;
        Door door = ev.Door;
        if (door.Permissions == DoorPermissionFlags.None)
        {
            ev.IsAllowed = true;
            ev.CanOpen = true;
            return;
        }

        foreach (ItemBase item in invInfo.Items.Values)
        {
            if (item.Category != ItemCategory.Keycard) continue;
            KcItem keycard = item as KcItem;
            DoorPermissionFlags flags = keycard.GetPermissions(ply as IDoorPermissionRequester);
            if (door.Base.RequiredPermissions.CheckPermissions(flags))
            {
                ev.IsAllowed = true;
                ev.CanOpen = true;
                return;
            }
        }
    }

    public override void OnPlayerInteractingLocker(PlayerInteractingLockerEventArgs ev)
    {
        string lockerName = ev.Locker.Base.name;
        if (lockerName.Contains("MedkitStructure"))
        {
            ev.CanOpen = true;
            ev.IsAllowed = true;
            return;
        }

        foreach (Item item in ev.Player.Items)
        {
            if (item is not KeycardItem keycard) continue;

            switch (keycard.Type)
            {
                case ItemType.KeycardResearchCoordinator:
                    if (!lockerName.Contains("PedestalStructure")) return;
                    ev.CanOpen = true;
                    ev.IsAllowed = true;
                    break;

                case ItemType.KeycardGuard:
                    if (!lockerName.Contains("LargeGunLockerStructure")) return;
                    ev.CanOpen = true;
                    ev.IsAllowed = true;
                    break;

                case ItemType.KeycardMTFPrivate:
                case ItemType.KeycardContainmentEngineer:
                case ItemType.KeycardMTFOperative:
                case ItemType.KeycardFacilityManager:
                    if (!lockerName.Contains("PedestalStructure") && !lockerName.Contains("RifleRackStructure") && !lockerName.Contains("LargeGunLockerStructure")) return;
                    ev.CanOpen = true;
                    ev.IsAllowed = true;
                    break;

                case ItemType.KeycardMTFCaptain:
                case ItemType.KeycardChaosInsurgency:
                case ItemType.KeycardO5:
                    if (!lockerName.Contains("PedestalStructure") && !lockerName.Contains("Experimental Weapon Locker") && !lockerName.Contains("RifleRackStructure") && !lockerName.Contains("LargeGunLockerStructure")) return;
                    ev.CanOpen = true;
                    ev.IsAllowed = true;

                    break;
            }
        }
    }

    public override void OnPlayerInteractingGenerator(PlayerInteractingGeneratorEventArgs ev)
    {
        foreach (Item item in ev.Player.Items)
        {
            if (item is not KeycardItem keycard) continue;

            switch (keycard.Type)
            {
                case ItemType.KeycardMTFPrivate:
                case ItemType.KeycardMTFOperative:
                case ItemType.KeycardMTFCaptain:
                case ItemType.KeycardChaosInsurgency:
                case ItemType.KeycardO5:
                    ev.IsAllowed = true;
                    ev.Generator.IsUnlocked = true;
                    break;
            }
        }
    }

    public override void OnPlayerUnlockingWarheadButton(PlayerUnlockingWarheadButtonEventArgs ev)
    {
        if (ev.Player == null) return;

        foreach (Item item in ev.Player.Items)
        {
            if (item is not KeycardItem keycard) continue;

            switch (keycard.Type)
            {
                case ItemType.KeycardFacilityManager:
                case ItemType.KeycardO5:
                    ev.IsAllowed = true;
                    break;
            }
        }
    }
}
