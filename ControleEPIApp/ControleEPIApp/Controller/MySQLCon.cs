using ControleEPIApp.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace ControleEPIApp.Controller
{
    public class MySQLCon
    {
        static string conn = @"server=sql.freedb.tech;port=3306;database=freedb_ControleEPI;user=freedb_MuriloPietro;password=8*FcGMa*bxUb2Nj";

        public static List<Funcionario> ListaFuncionario()
        {
            List<Funcionario> listafuncionarios = new List<Funcionario>();
            string sql = "SELECT * FROM funcionario";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario()
                            {
                                matricula = reader.GetString(0),
                                nome= reader.GetString(1),
                                epi = reader.GetString(2),
                                data_entrega = reader.GetDateTime(3),
                                data_vencimento = reader.GetDateTime(4),
                            };
                            listafuncionarios.Add(func);
                        }
                    }
                }
                con.Close();
                return listafuncionarios;
            }
        }
        public static List<Funcionario> listaEpi()
        {
            List<Funcionario> listaepi = new List<Funcionario>();
            string sql = "SELECT epi, data_entrega, data_vencimento FROM funcionario";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Funcionario func = new Funcionario();
                            {
                                func.epi = reader.GetString(3);
                                func.data_entrega = reader.GetDateTime(4);
                                func.data_vencimento = reader.GetDateTime(5);
                            };
                            listaepi.Add(func);
                        }
                    }
                }
                con.Close();
                return listaepi;
            }
        }

        public static void InserirFuncionario(string nome, string matricula, string epi, DateTime data_entrega, DateTime data_vencimento)
        {
            string sql = "INSERT INTO funcionario(nome, matricula, epi, data_entrega, data_vencimento) VALUES (@nome,@matricula,@epi,@data_entrega,@data_vencimento)";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                try
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@data_entrega", MySqlDbType.DateTime).Value = DateTime.Today;
                        cmd.Parameters.Add("@data_vencimento", MySqlDbType.DateTime).Value = DateTime.Today.AddDays(90);
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                        cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = epi;
                        cmd.CommandType = CommandType.Text; 
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
                con.Close();
            }
        }

        public static void AtualizarFuncionario(string nome, string matricula, string epi, DateTime data_entrega, DateTime data_vencimento)
        {
            string sql = "UPDATE funcionario SET nome=@nome, matricula=@matricula, epi=@epi, data_entrega=@data_entrega, data_vencimento=@data_vencimento  WHERE id=@id";
            try
            {
                using (MySqlConnection con = new MySqlConnection(conn))
                {
                    con.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome;
                        cmd.Parameters.Add("@matricula", MySqlDbType.VarChar).Value = matricula;
                        cmd.Parameters.Add("@epi", MySqlDbType.VarChar).Value = epi;
                        cmd.Parameters.Add(data_entrega = DateTime.Today);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static void ExcluirPessoa(Funcionario func)
        {
            string sql = "DELETE FROM funcionario WHERE matricula=@matricula";
            using (MySqlConnection con = new MySqlConnection(conn))
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                {
                    cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = func.matricula;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
