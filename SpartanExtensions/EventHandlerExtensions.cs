using System;

namespace SpartanExtensions
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises event correctly.
        /// </summary>
        public static void RaiseEvent(this EventHandler eventHandler, object sender)
        {
            if (eventHandler != null)
                eventHandler(sender, new EventArgs());
        }
    }
}
