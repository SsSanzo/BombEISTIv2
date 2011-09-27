using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BombEISTIv2.Environment
{
    public enum UpgradeType
    {
        //bonus
        Kick, PowerUp, SpeedUp, PowerMax, BombUp,
        //malus
        PowerDown, SpeedDown, ChangeDirection, Teleport, SpeedMax
    }
}
