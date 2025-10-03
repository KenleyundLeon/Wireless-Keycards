using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace Wireless_Keycards;

public class Main : Plugin<Config>
{
    public Events events = new();
    public override string Name => "Wireless-Keycards";
    public override string Description => "Allows the usage of Wireless keycards";
    public override string Author => "Kenley M.";
    public override Version Version => new(1, 0, 0, 0);
    public override Version RequiredApiVersion => new(LabApiProperties.CompiledVersion);
    public string githubRepo = "KenleyundLeon/Wireless-Keycards";

    public override void Enable()
    {
        Logger.Info("WirelessCard Enabled.");
        if (!Config.Enabled) return;
        CustomHandlersManager.RegisterEventsHandler(events);
    }

    public override void Disable()
    {
        Logger.Info("WirelessCard Disabled.");
        if (!Config.Enabled) return;
        CustomHandlersManager.UnregisterEventsHandler(events);
    }
}

