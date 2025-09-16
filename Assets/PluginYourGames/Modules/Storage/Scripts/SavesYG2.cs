
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public partial class SavesYG
    {
        public int idSave;
        public int currentLevel = 0;
        public int maxOpenLevel = 0;
        public float sensitivity = 0.2f;
        
        // Дополнительные поля для замены PlayerPrefs
        public float timerLider;
        public float maxTimerLider = 0;
        public string playerName = "123";
        public int colorAim = 0;
    }
}
