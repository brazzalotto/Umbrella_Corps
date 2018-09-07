using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella_Corps.Modeles
{
    class Paquet
    {
        private int numero { get; set; }

        private int ligneDebut { get; set; }

        private int ligneFin { get; set; }

        private bool actif { get; set; }

        public Paquet(int num,int deb,int fin)
        {
            numero = num;
            ligneDebut = deb;
            ligneFin = fin;
        }
    }
}
