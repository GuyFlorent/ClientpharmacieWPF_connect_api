using ClientpharmacieWPF.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientpharmacieWPF
{
    /// <summary>
    /// Logique d'interaction pour FenetreCliente1.xaml
    /// </summary>
    public partial class FenetreCliente1 : Window
    {
        Service1Client svc = new Service1Client();
        List<ProduitReturn> listeStock;
        private string _nom;
        public FenetreCliente1(ClientReturn cli )
        {
            InitializeComponent();


            var listeStock = svc.listeProduit();
            listeBoxProduit.ItemsSource = listeStock; // ajout de la liste des produit dans le combobox
            listeBoxProduit.DisplayMemberPath = "nom_produit_stock";//suite
            listeBoxProduit.SelectedIndex = 0;//suite

             _nom = cli.nom;
            lab_nomClient.Text = cli.nom +" "+ cli.prenom; 
            lab_titre.Content = cli.nom;
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string nomClient = _nom;
            string nomProduit = listeBoxProduit.Text;
            string quantite = txtquantite_cmd.Text;
            svc.passerCommande(nomClient, nomProduit, Convert.ToInt32(quantite));
        }
    }
}
