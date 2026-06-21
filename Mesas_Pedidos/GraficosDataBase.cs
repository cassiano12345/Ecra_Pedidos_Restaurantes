using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace Mesas_Pedidos
{
    class GraficosDataBase
    {
        public static String ConnectionString = Properties.Settings.Default.ConStr; //Obtendo os dados de ligação a base de dados

        public static List<List<KeyValuePair<String, Double>>> getGrafico()
        {
            List<List<KeyValuePair<String, Double>>> finalList = new List<List<KeyValuePair<string, double>>>();
            List<KeyValuePair<String, Double>> kvpListV1 = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<String, Double>> kvpListV2 = new List<KeyValuePair<string, double>>();
            List<KeyValuePair<String, Double>> kvpListV3 = new List<KeyValuePair<string, double>>();


            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                connection.Open();

                // Configurar Database Change Notification
                OracleDependency dependency = new OracleDependency();
                dependency.OnChange += new OnChangeEventHandler(OnDatabaseChange);

                using (OracleCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT sala, mesa, descricao, qnt FROM pos_mesas_pedidos";
                    command.Notification = null;

                    // Associa o comando ao OracleDependency
                    dependency.AddCommandDependency(command);

                    // Executa o comando (requisito para iniciar a notificação)
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lê os dados inicialmente.
                            Console.WriteLine($"Sala: {reader["sala"]}, Mesa: {reader["mesa"]}, Descrição: {reader["descricao"]}, Quantidade: {reader["qnt"]}");
                        }
                    }
                }

                Console.WriteLine("Aguardando mudanças no banco de dados...");
                Console.ReadLine();
            }



            return finalList;
        }

        // Método disparado quando uma mudança é detectada
        static void OnDatabaseChange(object sender, OracleNotificationEventArgs e)
        {
            // Aqui você pode processar a notificação
            Console.WriteLine("Mudança detectada no banco de dados!");
            MessageBox.Show("Mudança detectada no banco de dados!");

            // Dependendo do tipo de mudança, você pode recarregar os dados ou apenas atualizar o que mudou.
        }


    }

}
