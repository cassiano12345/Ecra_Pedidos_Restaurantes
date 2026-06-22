using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Mesas_Pedidos
{
    class MeusDados
    {
        public string Descricao { get; set; }
        public string mesa { get; set; }
        public string sala { get; set; }
        public string status { get; set; }
        public float quantidade { get; set; }
    }

    class OracleChangeNotifier
    {
        //Oracle
        
        
        private OracleConnection _connection;
        private string _connectionString;
        public static String ConnectionString = Properties.Settings.Default.ConStr; //Obtendo os dados de ligação a base de dados
        public string Nom_sala;
        


        

       public OracleChangeNotifier(string connectionString)
       {
           _connectionString = connectionString;
       }

       // Iniciar a escuta de mudanças no banco de dados
       public List<MeusDados> StartListening(string emp)
       {
           List<MeusDados> dadosList = new List<MeusDados>();
           _connection = new OracleConnection(_connectionString); // Passando os dados da conexão
           _connection.Open();


           using (OracleCommand command = _connection.CreateCommand())
           {
               command.CommandText = "SELECT sala, mesa, descricao, qnt, status FROM pos_mesas_pedidos where codemp = '"+ emp +"' order by ordem"; //Select SQL
               command.Notification = null;

               // Executa o comando para iniciar a notificação
               using (OracleDataReader reader = command.ExecuteReader())
               {
                   // Aqui você pode processar os dados inicialmente, se desejar
                   while (reader.Read())
                   {
                       // Extrair dados do reader
                       String descricao_x = reader.GetString(2);
                       String sala_x = reader.GetString(0);
                       String mesa_x = reader.GetString(1);
                       String status_x = reader.GetString(4);
                       float quantidade_x = reader.GetFloat(3);

                       // Criar o objeto MeusDados
                       MeusDados dados = new MeusDados
                       {
                           Descricao = descricao_x,
                           mesa = mesa_x,
                           sala = sala_x,
                           quantidade = quantidade_x,
                           status = status_x
                       };

                       // Adicionar os dados à lista
                       dadosList.Add(dados);
                   }
               }
           }
           // Juntando os dados de acordon a descrição
           var dadosAgrupados = dadosList
               .GroupBy(d => new { d.sala, d.mesa }) // Agrupa por sala e mesa
               .Select(grupo => new MeusDados
               {
                   sala = grupo.Key.sala,
                   mesa = grupo.Key.mesa,
                   // Usa o primeiro status do grupo
                   status = grupo.First().status.ToString(),
                   // Junta as descrições e quantidades
                   Descricao = String.Join(",\n", grupo.Select(d => $"{d.Descricao} ({d.quantidade})")),
                   quantidade = grupo.Sum(d => d.quantidade)
               }).ToList();
           _connection.Close();
           return dadosAgrupados; // Retorna a lista de dados agrupados
       }

       //Função destinada a obter o nome da sala
       public void selecionar_sala(string Numero_sala, string emp)
       {
           _connection = new OracleConnection(_connectionString); // Passando os dados da conexão
           _connection.Open();


           using (OracleCommand command = _connection.CreateCommand())
           {
               command.CommandText = "Select designacao from pos_mesas where codemp= " + emp + " and nrmenu=" + Numero_sala + ""; //Select SQL
               command.Notification = null;

               // Executa o comando para iniciar a notificação
               using (OracleDataReader reader = command.ExecuteReader())
               {
                   // Aqui você pode processar os dados inicialmente, se desejar
                   while (reader.Read())
                   {
                       Nom_sala = reader.GetString(0);
                   }
               }
           }
           _connection.Close();
       }

       // Função para mudar o estado para Exectudado
       public void Pedido_Executado(string sala, string mesa, string emp)
       {
           String sqlquery = "UPDATE pos_mesas_pedidos SET status = 'E' WHERE sala = " + sala + " AND mesa = " + mesa + "AND codemp = " + emp + " ";

           using (OracleConnection conn = new OracleConnection(_connectionString))
           {
               OracleCommand cmd = new OracleCommand(sqlquery, conn);
               conn.Open();
               OracleDataReader dr = cmd.ExecuteReader();
               try
               {
                   while (dr.Read())
                   {
                       Console.WriteLine($"{dr}");
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
               finally
               {
                   dr.Close();
               }

           }
       }

       //Função para apagar pedidos
       public void Delete_Pedido(string sala, string mesa, string emp)
       {
           String sqlquery = "DELETE pos_mesas_pedidos WHERE sala = " + sala + " AND mesa = " + mesa + "AND codemp = " + emp + "";

           using (OracleConnection conn = new OracleConnection(_connectionString))
           {
               OracleCommand cmd = new OracleCommand(sqlquery, conn);
               conn.Open();
               OracleDataReader dr = cmd.ExecuteReader();
               try
               {
                   while (dr.Read())
                   {
                       Console.WriteLine($"{dr}");
                   }
               }
               catch (Exception ex)
               {
                   throw ex;
               }
               finally
               {
                   dr.Close();
               }
           }
       }

       // Parar a escuta de mudanças no banco de dados
       public void StopListening()
       {
           if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
           {
               _connection.Close();
           }
       }

       


        //Mysql
        /*
        
        private MySqlConnection _connection;
        private string _connectionString;

        public static string ConnectionString = Properties.Settings.Default.ConStr_;
        public string Nom_sala;
       

        public OracleChangeNotifier(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<MeusDados> StartListening(string emp)
        {
            List<MeusDados> dadosList = new List<MeusDados>();

            _connection = new MySqlConnection(_connectionString);
            _connection.Open();

            using (MySqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT sala, mesa, descricao, qnt, status FROM pos_mesas_pedidos WHERE codemp = @emp";
                command.Parameters.AddWithValue("@emp", emp);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MeusDados dados = new MeusDados
                        {
                            sala = reader["sala"].ToString(),
                            mesa = reader["mesa"].ToString(),
                            Descricao = reader["descricao"].ToString(),
                            quantidade = Convert.ToSingle(reader["qnt"]),
                            status = reader["status"].ToString()
                        };

                        dadosList.Add(dados);
                    }
                }
            }

            _connection.Close();

            return dadosList
                .GroupBy(d => new { d.sala, d.mesa })
                .Select(grupo => new MeusDados
                {
                    sala = grupo.Key.sala,
                    mesa = grupo.Key.mesa,
                    status = grupo.First().status,
                    Descricao = string.Join(",\n", grupo.Select(d => $"{d.Descricao} ({d.quantidade})")),
                    quantidade = grupo.Sum(d => d.quantidade)
                })
                .ToList();
        }

        public void selecionar_sala(string Numero_sala, string emp)
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();

            using (MySqlCommand command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT designacao FROM pos_mesas WHERE codemp='"+ emp +"'AND nrmenu='"+ Numero_sala +"'";


                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Nom_sala = reader["designacao"].ToString();
                    }
                }
            }

            _connection.Close();
        }

        public void Pedido_Executado(string sala, string mesa, string emp)
        {
            string sql = @"UPDATE pos_mesas_pedidos
                   SET status='E'
                   WHERE sala=@sala
                     AND mesa=@mesa
                     AND codemp=@emp";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sala", sala);
                    cmd.Parameters.AddWithValue("@mesa", mesa);
                    cmd.Parameters.AddWithValue("@emp", emp);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete_Pedido(string sala, string mesa, string emp)
        {
            string sql = @"DELETE FROM pos_mesas_pedidos
                   WHERE sala=@sala
                     AND mesa=@mesa
                     AND codemp=@emp";

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@sala", sala);
                    cmd.Parameters.AddWithValue("@mesa", mesa);
                    cmd.Parameters.AddWithValue("@emp", emp);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void StopListening()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        */

    }
}
