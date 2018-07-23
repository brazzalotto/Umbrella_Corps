using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;

namespace Umbrella_Corps
{
    class Module1
    {

        //recuperation des données (fichier complet)
        string contenu = File.ReadAllText("C:\\genome2.txt");
        

        // 1 nombre total de paires de bases AA TT GG CC ou alors : AT et GC

        /*
         * switch : foreach lettre dans la liste
         * si AT alors AT++
         * si GC alors GC++

         */


        // 2 occurence des A T G C - + le pourcentage de chacun d'entre eux


        // 3 nombre de sequences de 4 bases la plus fréquente




    }
}
