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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientpharmacieWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Service1Client svc = new Service1Client();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            svc.ajouterclients(txtNom.Text, txtPrenom.Text, txtEmail.Text, txtPassword.Password);
            //vider(); 
            MessageBox.Show("Ajouter avec succes!");
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(conEmail.Text) || string.IsNullOrEmpty(conPassword.Password))
            {
                error.Content = "Champ vide !!!";
               // vider();
            }
            else
            {
                bool trouve = svc.verifierClients(conEmail.Text, conPassword.Password);
                if (trouve)
                {
                    ClientReturn cli = svc.recupereParEmail(conEmail.Text);

                    FenetreCliente1 fenetre1 = new FenetreCliente1(cli);
                    fenetre1.Title = cli.nom;
                    fenetre1.Show();
                    this.Close();
                }
                else
                    error.Content = "Erreur !!!";
               // vider();
            }
        }
    }
}
