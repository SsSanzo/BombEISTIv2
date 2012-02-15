using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BombEistiv2WPF.View;

namespace BombEistiv2WPF.Environment
{
    public class UpgradeBomb : Entity
    {
        private readonly BombType _type;

        public UpgradeBomb(int x, int y, BombType bt)
            : base(x, y)
        {
            _type = bt;
        }

        public BombType Type
        {
            get { return _type; }
        }

        public void Burn()
        {
            Texture._.DeleteTextureEntity(this);
        }

        protected override bool Move(int x, int y)
        {
            return false;
        }
    }
}
