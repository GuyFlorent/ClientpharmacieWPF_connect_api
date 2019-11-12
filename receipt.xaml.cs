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
    /// Logique d'interaction pour receipt.xaml
    /// </summary>
    public partial class receipt : Window
    {
        Service1Client svc = new Service1Client();
        private List<orderHisto> listeOrder;
        public ClientReturn clientReceipt;
        public receipt(ClientReturn client1, decimal TotalPrice,  List<ProduitPanier> listPanier)
        {
            InitializeComponent();

            this.clientReceipt = client1;
            var Order = svc.getcommandehisto(client1).OrderByDescending(f => f.heureCommand).FirstOrDefault();
            
            txtnomClient.Text = client1.nom +"  "+client1.prenom;
            txtDateCommande.Text = Order.heureCommand;
           /* txt_nomProduit.Text = Order.nom_Produit;
            txt_Prix_Unite.Text = Order.prix_Produit_Unite.ToString()+" €";
            txtQuantité.Text = Order.quantite.ToString();
            txtPrix_Total.Text = Order.prix_total.ToString() + " €";*/
            txt_Total_Price.Text = TotalPrice.ToString() + " €";
            txt_date_Jour.Text = Order.heureCommand;

            maList_Facture.ItemsSource = listPanier;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "invoice");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
