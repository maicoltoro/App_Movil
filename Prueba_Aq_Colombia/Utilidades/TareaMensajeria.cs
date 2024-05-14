using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Prueba_Aq_Colombia.Utilidades
{
    public class TareaMensajeria:ValueChangedMessage<TareaMensaje>
    {
        public TareaMensajeria(TareaMensaje value) : base(value)
        {

        }
    }
}
