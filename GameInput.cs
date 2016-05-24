using System.Collections;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameInput
    {
        //For maintaining Keyboard Keys with their corresponding states 
        private static Hashtable KeyTable = new Hashtable();

        //For setting the Keyboard Keys with their corresponding states
        public static void ChangeState(Keys key, bool state)
        {
            KeyTable[key] = state;
        }

        //For Checking whether a particular Keyboard Key has been Pressed or not
        public static bool PressedKey(Keys key)
        {
            if (KeyTable[key] == null)
                return false;
            else
                return (bool)KeyTable[key];
        }
    }
}
