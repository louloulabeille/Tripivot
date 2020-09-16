using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace TriPivot
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] tabAtri = new int[] {65,58,1,14,24,4,2,0,9,7,4,8,1,6,5,56,88,64,16 };
            AfficheTableau(tabAtri);
            TriPivot(ref tabAtri, 0, tabAtri.Length-1);
            AfficheTableau(tabAtri);
        }

        public static void AfficheTableau ( int[] tabTri )
        {
            StringBuilder tabAff = new StringBuilder();
            foreach (int value in tabTri )
            {
                tabAff.Append( value );
                tabAff.Append("|");
            }
            Console.WriteLine(tabAff.ToString());
        }

        /// <summary>
        /// tri pivot ou tri rapide 
        /// </summary>
        /// <param name="tabTri"></param>
        /// <param name="indexDebut"></param>
        /// <param name="indexFin"></param>
        public static void TriPivot ( ref int[] tabTri, int indexDebut, int indexFin )
        {
            if ( indexDebut < indexFin )
            {
                int pivot = CalculPivot(indexDebut, indexFin);
                pivot = Partitionnement(ref tabTri, indexDebut, indexFin, pivot);
                // recurcivité on rappelle la fonction jusqu'à
                // indexdebut < indexFin ou la brache de recurvité sera fermé
                // ici on appelle les 2 sous tables en premant comme index du pivot calculer plus haut
                TriPivot(ref tabTri, indexDebut, pivot - 1);
                TriPivot(ref tabTri, pivot + 1, indexFin);
            }
        }

        /// <summary>
        /// renvoie un index du pivot tirer aléatoirement entre un index de début et de fin
        /// </summary>
        /// <param name="indexDebut"></param>
        /// <param name="indexFin"></param>
        /// <returns></returns>
        public static int CalculPivot ( int indexDebut, int indexFin)
        {
            var randInt = new Random();
            return randInt.Next(indexDebut ,indexFin +1);
        }


        /// <summary>
        /// tri en partie et partionne la table en 2 
        /// avec tous les éléments plus petit que la valeur du pivot
        /// et de l'autre tous les éléments supérieur
        /// en renvoyant l'index du pivot 
        /// </summary>
        /// <param name="tabAtri"></param>
        /// <param name="indexDebut"></param>
        /// <param name="indexFin"></param>
        /// <param name="pivotPart"></param>
        /// <returns></returns>
        public static int Partitionnement( ref int[] tabAtri, int indexDebut, int indexFin, int pivotPart )
        {
           
            // facilite la recherche sans se préocuper de lui
            int pivot = tabAtri[pivotPart];
            int varTemporaire = tabAtri[pivotPart];
            int newPivot = indexDebut;       //nouveau indice du pivot qui est calculé dans la boucle auto

            // on met la variable pivot à la fin
            tabAtri[pivotPart] = tabAtri[indexFin];
            tabAtri[indexFin] = varTemporaire;

            // on recherche toutes les valeurs inférieures à la valeur pivot
            //et on décale l'index du pivot
            for (int i = indexDebut ; i<indexFin ;i++ )
            {
                if ( tabAtri[i] <= pivot)
                {
                    varTemporaire = tabAtri[i];
                    tabAtri[i] = tabAtri[newPivot];
                    tabAtri[newPivot] = varTemporaire;
                    newPivot++;
                }
            }

            //on remet la valeur du pivot dans le bon emplacement au niveau de la table
            varTemporaire = tabAtri[newPivot];
            tabAtri[newPivot] = tabAtri[indexFin];
            tabAtri[indexFin] = varTemporaire;

            return newPivot;
        }
    }
}
