using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbrella_Corps.Modeles
{
    public class Paquet
    {
        public int numero { get; set; }

        public int ligneDebut { get; set; }

        public int ligneFin { get; set; }

        public bool actif { get; set; }

        public Paquet(int num,int deb,int fin)
        {
            numero = num;
            ligneDebut = deb;
            ligneFin = fin;
        }
    }
}
