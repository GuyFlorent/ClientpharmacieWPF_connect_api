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
        
        private ClientReturn client1;
        public FenetreCliente1(ClientReturn cli )
        {
            InitializeComponent();

            this.client1 = cli;
            var listeStock = svc.listeProduit();
            listeBoxProduit.ItemsSource = listeStock; // ajout de la liste des produit dans le combobox
            listeBoxProduit.DisplayMemberPath = "nom_produit_stock";//suite
            listeBoxProduit.SelectedIndex = 0;//suite

            
            lab_nomClient.Text = cli.nom +" "+ cli.prenom; 
            lab_titre.Content = cli.nom;
            txtNom.Text = cli.nom;
        }

        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string nomClient = client1.nom;
            string nomProduit = listeBoxProduit.Text;
            string quantite = txtquantite_cmd.Text;
            svc.passerCommande(nomClient, nomProduit, Convert.ToInt32(quantite));
        }

        private void btnModier_Click(object sender, RoutedEventArgs e)
        {
            this.client1.nom = txtNom.Text;
            this.client1.prenom = txtPrenom.Text;
            this.client1.email = txtEmail.Text;
            this.client1.password = txtPassword.Password;
            svc.modifierClients(this.client1);
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show(client1.nom+" etes vous sur de vouloir votre supprimer?", "Allerte !!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                svc.supprimerClients(this.client1);
                
                MessageBox.Show("supprimer avec succes");
                
            }
        }

       
    }
}
