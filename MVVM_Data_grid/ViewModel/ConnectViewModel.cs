using MVVM_Data_grid.Model;
using MVVM_Data_grid.Utility;
using System.Net.Sockets;
using System.Windows;

using System.Windows.Controls;


namespace MVVM_Data_grid.ViewModel
{
    public class ConnectViewModel : ViewModelBase
    {
        
        
        private TcpClient? client;
        private NetworkStream? stream;


        public RelayCommand ConnectCommand => new RelayCommand(execute => Connect());
        public RelayCommand DisconnectCommand => new RelayCommand(execute => Disconnect(), canExecute => TcpViz !=null);



        private string? tcpViz = "localhost";

        public string? TcpViz
        {
            get
            {
                return tcpViz;
            }

            set
            {
                tcpViz = value;
                OnPropertyChanged(TcpViz);
            }
        }

        private async void Connect()
        {
            ConnectViz TcIpCon = new ConnectViz();

            if (client == null)
            {
                try
                {

                       
                    string? IpAdress = TcpViz;
                    int port = 6100;

                    client = new TcpClient();

                    await client.ConnectAsync(IpAdress, port);

                    stream = client.GetStream();

                }
                catch (Exception)
                {

                    MessageBox.Show("dupa dupa");
                }
            }

        }
        private void Disconnect()
        {
            if (client != null)
            { 
                client.Close();
                client = null;
            }

        }
    }
}
