using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientpharmacieWPF
{
   public class ProduitPanier : ObservableCollection<ProduitPanier>
    {
        public string nomProduit { get; set; }
        public int quantite { get; set; }
        public decimal prix_unite { get; set; }
        public decimal prix_total { get; set; }
    }
}
