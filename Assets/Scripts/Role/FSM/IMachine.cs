


//-------------------------------------------
//  author: Billy
//  description:  
//-------------------------------------------

namespace DS.Role.FSM
{
    public interface IMachine
    {
        void Update();
        void FixUpdate();
        void TranslateTo(IPlayerState newPlayerState);
        IPlayerState GetCurrentState();

    }
}

