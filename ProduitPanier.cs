using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientpharmacieWPF
{
   public class ProduitPanier
    {
        public string nomProduit { get; set; }
        public int quantite { get; set; }
        public decimal prix_unite { get; set; }
        public decimal prix_total { get; set; }
    }
}
