using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class SoftBlock : Entity
    {
        private readonly Upgrade _myUpgrade;
        private readonly UpgradeBomb _myBombSec;

        public SoftBlock(int x, int y, Upgrade u = null, UpgradeBomb bt = null)
            : base(x, y)
        {
            _myUpgrade = u;
            _myBombSec = bt;
        }


        public Upgrade MyUpgrade
        {
            get { return _myUpgrade; }
        }

        public UpgradeBomb MyBombSec
        {
            get { return _myBombSec; }
        }

        public void Destroy(Map m)
        {
            if(MyUpgrade != null)
            {
                m.ListOfUpgrade.Add(MyUpgrade);
                Texture._.InsertTextureEntity(MyUpgrade);
            }else if(MyBombSec != null)
            {
                m.ListOfUpgradeBomb.Add(MyBombSec);
                Texture._.InsertTextureEntity(MyBombSec);
            }
            
        }

        protected override bool Move(int x, int y)
        {
            return false;
        }
    }
}
