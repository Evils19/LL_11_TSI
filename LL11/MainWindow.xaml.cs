using System.Numerics;
using System.Security.Cryptography;
using System.Windows;


namespace LL11;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

 

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {

        string Text = txtMessage.Text;
        if (string.IsNullOrEmpty(Text))
        {
            MessageBox.Show("Complectati inputul");
            
        }
        else
        {
            Algoritm_RSA algoritmRsa = new Algoritm_RSA();
            BigInteger[] cripto = algoritmRsa.CriptoRSA(Text);
        
            txtDecodedMessage.Text = "";
            for (int i = 0; i < cripto.Length; i++)
            {
                txtDecodedMessage.Text += cripto[i] + ",";
            
            }
            txtKey2.Text =algoritmRsa.e +","+algoritmRsa.n ;
            txtKey.Text = algoritmRsa.d +","+algoritmRsa.n; 
            
        
        }
      


    }
    
    
    

    private void Button_Click_2(object sender, RoutedEventArgs e)
    {
        string Text = txtMessage.Text;
        string Key = txtKey.Text;

        if (string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Key))
        {
            MessageBox.Show("Complectati inputul si cheia privata");
        }
        else
        {
            string[] key = Key.Split(',');
            BigInteger[] key1 = new BigInteger[key.Length];

            for (int i = 0; i < key.Length; i++)
            {
                key1[i] = BigInteger.Parse(key[i]);
            }
            Console.WriteLine("Cheia privata: " + key1[0] + " " + key1[1]);

            string[] text = txtDecodedMessage.Text.Split( ',' , StringSplitOptions.RemoveEmptyEntries);
            BigInteger[] cripto = new BigInteger[text.Length];

            for (int j = 0; j < text.Length; j++)
            {
                if (BigInteger.TryParse(text[j], out BigInteger result))
                {
                    cripto[j] = result;
                    Console.WriteLine(cripto[j]);
                }
                else
                {
                    Console.WriteLine($"Unable to parse '{text[j]}' as a BigInteger.");
                }
            }

            
            Console.WriteLine("Cheia privata: " + key1[0] + " " + key1[1]);
            string text1 = Algoritm_RSA.DecriptoRSA(cripto, key1[0], key1[1]);
            txtDecodedMessage.Text = text1;
        }
    }

    }

    


