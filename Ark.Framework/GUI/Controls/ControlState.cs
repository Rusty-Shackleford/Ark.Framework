namespace Ark.Framework.GUI.Controls
{
    public enum ControlState
    {
        // TODO: Is Pressed a valid state? Is a control still 
        // pressed if the mouse is not over it?  If not, then
        // you would only have Hovered_Pressed.
        Default,                    // All
        Hovered,                    // All
        Pressed,                    // All
        Hovered_Pressed,            // All
        Hovered_Checked,            // Checkbox
        Checked,                    // Checkbox
        Hovered_Checked_Pressed,    // Checkbox
        Focused,                    // Textbox
        Hovered_Focused             // Textbox
    }
}
