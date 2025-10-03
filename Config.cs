using System.ComponentModel;

namespace Wireless_Keycards;

public class Config
{
    [Description("Enables or disables the Wireless Keycards plugin.")]
    public bool Enabled { get; set; } = true;
    [Description("Skips the wireless keycard usage if the player has a keycard in their hand.")]
    public bool SkipIfKeycardInHand { get; set; } = true;
}
