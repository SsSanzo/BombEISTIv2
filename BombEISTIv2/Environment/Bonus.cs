namespace BombEISTIv2.Environment
{
    public class Bonus : Upgrade
    {

        public Bonus(int x, int y, UpgradeType u)
            : base(x, y, u)
        {

        }

        public void UseBonus(Player p)
        {
            switch (Type)
            {
                    case UpgradeType.Kick:
                        KickPower(p);
                    break;
                    case UpgradeType.BombUp:
                        BombUpPower(p);
                    break;
                    case UpgradeType.PowerMax:
                        PowerMaxPower(p);
                    break;
                    case UpgradeType.PowerUp:
                        PowerUpPower(p);
                    break;
                    case UpgradeType.SpeedUp:
                        SpeedUpPower(p);
                    break;
            }
        }

        public void KickPower(Player p)
        {
            
        }

        public void BombUpPower(Player p)
        {

        }

        public void PowerMaxPower(Player p)
        {

        }

        public void PowerUpPower(Player p)
        {

        }

        public void SpeedUpPower(Player p)
        {

        }
    }
}
