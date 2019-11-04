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
        private Client cli;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtPassword.Password) || string.IsNullOrEmpty(txtEmail.Text)
                || string.IsNullOrEmpty(txtPrenom.Text))
            {
                MessageBox.Show(" un ou plusieurs Champs sont vide !!!");

            }
            else
            {
                try
                {
                    cli = new Client();
                    cli.nom = txtNom.Text;
                    cli.prenom = txtPrenom.Text;
                    cli.email = txtEmail.Text;
                    cli.password = txtPassword.Password;
                    svc.ajouterclients(cli);
                    MessageBox.Show(svc.ajouterclients(cli).ToString());
                }catch (Exception) { MessageBox.Show("changer de email!!"); }
            }
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
