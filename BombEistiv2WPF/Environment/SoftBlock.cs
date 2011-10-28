using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class SoftBlock : Entity
    {
        private readonly Upgrade _myUpgrade;

        public SoftBlock(int x, int y, Upgrade u = null)
            : base(x, y)
        {
            _myUpgrade = u;
        }


        public Upgrade MyUpgrade
        {
            get { return _myUpgrade; }
        }

        public void Destroy(Map m)
        {
            if(MyUpgrade != null)
            {
                m.ListOfUpgrade.Add(MyUpgrade);
                Texture._.InsertTextureEntity(MyUpgrade);
            }
        }

        protected override bool Move(int x, int y)
        {
            return false;
        }
    }
}
