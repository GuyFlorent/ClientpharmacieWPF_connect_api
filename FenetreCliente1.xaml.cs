﻿using ClientpharmacieWPF.ServiceReference1;
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
        List<orderHisto> listeOrder;

        public ClientReturn client1;
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
            svc.passerCommande(nomClient, nomProduit, Convert.ToInt32(quantite));
            receipt receipt = new receipt(this.client1);
            receipt.Show();
            actualliser();
           
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
            tb_prix_unité.Text = prix_textb.prix_Produit_Unite.ToString();

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

      
    }
}
