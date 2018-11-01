using System;

namespace Ark.Framework.GUI
{
    internal interface IClickable
    {
        event EventHandler Clicked;
    }
}