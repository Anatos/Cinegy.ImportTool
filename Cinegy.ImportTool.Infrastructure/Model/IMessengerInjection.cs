using GalaSoft.MvvmLight.Messaging;

namespace Cinegy.ImportTool.Infrastructure.Model
{
    public interface IMessengerInjection
    {
        #region Members

        void Attach(IMessenger rootMsgr);

        #endregion
    }
}