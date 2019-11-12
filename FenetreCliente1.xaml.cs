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
using System.Diagnostics;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections;
using Color = System.Drawing.Color;

namespace ClientpharmacieWPF
{
    /// <summary>
    /// Logique d'interaction pour FenetreCliente1.xaml
    /// </summary>
    public partial class FenetreCliente1 : Window
    {
        Service1Client svc = new Service1Client();
        List<ProduitReturn> listeStock;
        List<orderHisto> listeOrdere;
        public double prixUnite;

       public List<ProduitPanier> listPanier = new List<ProduitPanier>() ;
        ProduitPanier panier;
        public ClientReturn client1;

        public string nomPro;

        public decimal TotalPrice;
        public FenetreCliente1(ClientReturn cli)
        {
            InitializeComponent();

            this.client1 = cli;
            actualliser();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

           
            string nomClient = client1.nom;
            string nomProduit = listeBoxProduit.Text;
            string quantite = txtquantite_cmd.Text;
            var QuantitéStock = svc.listeProduit().FirstOrDefault(f=>f.nom_produit_stock == nomProduit).quantite_produit;
            if (Convert.ToInt32(quantite) <= QuantitéStock)
            {
                foreach (ProduitPanier p in listPanier)
                {
                    svc.passerCommande(nomClient, p.nomProduit, p.quantite);

                }
                receipt receipt = new receipt(this.client1, TotalPrice, listPanier);
                receipt.Show();
                
                actualliser();
               
            }
            else
            {
                MessageBox.Show("La quantité restante est insuffisante car elle est de : " + QuantitéStock.ToString());
            }
           
        }

        private void btnModier_Click(object sender, RoutedEventArgs e)
        {
            this.client1.nom = txtNom.Text;
            this.client1.prenom = txtPrenom.Text;
            this.client1.email = txtEmail.Text;
            this.client1.password = txtPassword.Password;
            svc.modifierClients(this.client1);
          
            actualliser();
        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(client1.nom + " etes vous sur de vouloir votre supprimer?", "Allerte !!", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                svc.supprimerClients(this.client1);

                MessageBox.Show("supprimer avec succes");
                actualliser();

                MainWindow mn = new MainWindow();
                mn.Show();
                this.Close();

            }
        }

        public void actualliser()
        {
            var listeStock = svc.listeProduit();
            listeBoxProduit.ItemsSource = listeStock; // ajout de la liste des produit dans le combobox
            listeBoxProduit.DisplayMemberPath = "nom_produit_stock";//suite
            listeBoxProduit.SelectedIndex = 0;//suite


            lab_nomClient.Text = this.client1.nom + " " + this.client1.prenom;
            lab_titre.Content = this.client1.nom;
            txtNom.Text = this.client1.nom;
            var listeOrder = svc.getcommandehisto(this.client1).OrderByDescending(f=> f.heureCommand);
            var prix_textb = listeOrder.FirstOrDefault(s => s.nom_Produit == listeBoxProduit.SelectedItem.ToString());
           


            //  tb_prix_unité.Text = prix_textb.prix_Produit_Unite.ToString();

            maListeBox.DataContext = listeOrder;
            
              /* foreach (var a in listeOrder)
                {
                    if (!maListeBox.Items.Contains(a.nom_Produit))
                    {
                        maListeBox.Items.Add(a.nom_Produit + "   Prix à l'unité :   " + a.prix_Produit_Unite + "  Quantitée  :  " + a.quantite + "   Prix total  :  " + a.prix_total + " € " + " Date et Heure de Commande  :   " + a.heureCommand + "   Statut de livraison   :    " + a.statutLivraison);

                    maListeBox.Items.Refresh();
                    }
                    else
                    {
                        Console.WriteLine("error");
                    }

                }*/
       
        }

        private void listeBoxProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            
              var listeStock = svc.listeProduit();
              try
              {
                  var img = listeStock[listeBoxProduit.SelectedIndex].image_test;//recuperer image produit
                mon_image.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(img);

                lab_Quantite_restante.Text = (listeStock[listeBoxProduit.SelectedIndex].quantite_produit).ToString() + " pièces";

                //achiffage le prix a l'unité en  bas de l'image



                string nom = listeStock[listeBoxProduit.SelectedIndex].nom_produit_stock; //recupration du nom du produit selectionné
                nomPro = nom;

                txt_prix_unite.Text = svc.infoProduits().FirstOrDefault(f => f.nom_produit == nom).prix_unite.ToString() + " €";

             

                /* Stream StreamObj = new MemoryStream(img); //code permettant de recuperer l'image de la base de donnée

                  BitmapImage BitObj = new BitmapImage();

                  BitObj.BeginInit();

                  BitObj.StreamSource = StreamObj;

                  BitObj.EndInit();

                  mon_image.Source = BitObj;*/
            }
              catch (Exception) { }
        }

        private void Tb_prix_unité_Click(object sender, RoutedEventArgs e)
        {
            
            panier = new ProduitPanier();
            panier.nomProduit = nomPro;
            panier.quantite = Convert.ToInt32(txtquantite_cmd.Text);
            panier.prix_unite = (decimal)svc.infoProduits().FirstOrDefault(f => f.nom_produit == nomPro).prix_unite;
            panier.prix_total = (decimal)svc.infoProduits().FirstOrDefault(f => f.nom_produit == nomPro).prix_unite * Convert.ToInt32(txtquantite_cmd.Text);
            listPanier.Add(panier);
            TotalPrice += panier.prix_total;
            ma_ListView.ItemsSource = listPanier;
           
        }
    }
}
