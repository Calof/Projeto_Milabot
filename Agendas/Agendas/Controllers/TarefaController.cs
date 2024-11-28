using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;


namespace Agendas.Controllers
{
    public class TarefaController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string senha)
        {
            // Teste de login
            if (email == "admin@example.com" && senha == "agenda12345")
            {

                return RedirectToAction("Escolha");
            }


            ViewBag.ErrorMessage = "Email ou senha inválidos.";
            return View();
        }


        [HttpPost]
        public IActionResult Cadastrar()
        {
            return View();
        }
        
        public IActionResult Escolha()
        {
            return View();
        }

        public IActionResult Tarefas()
        {
            return View();
        }

        public IActionResult Anotacoes()
        {
            return View();
        }
        public IActionResult Adm()
        {
             return View();


        }

        [HttpDelete]
        public string Delete(int matricula)
        {
            SQLiteConnection sqlite_conn;
            try
            {
                sqlite_conn = PegarConexao();
                sqlite_conn.Open();

                string sql = $"DELETE from CadUsuario WHERE matricula = {matricula};";
                SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
                comandoSQL.ExecuteNonQuery();

                return "sucesso";
            }
            catch(Exception ex)
            {
                return $"Não foi possível excluir {ex} ";
            }
        }


        [HttpPost]

        public string Salvar(string nome, string email, string senha )
        {

            SQLiteConnection sqlite_conn;
            try
            {
                sqlite_conn = PegarConexao();
                sqlite_conn.Open();


                string sql = $"insert into CadUsuario(nome,email,senha) values('{nome}','{email}' ,'{senha}');";

                SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
                comandoSQL.ExecuteNonQuery();
                sqlite_conn.Close();
                return "Sucesso !";
            }
            catch (Exception ex)
            {
                return $"Não foi possível inserir {ex} ";
            }

        }

		[HttpGet]
		public string Buscar(string nome)
		{

			SQLiteConnection sqlite_conn;
            try
            {
            sqlite_conn = PegarConexao();
		    sqlite_conn.Open();
 
            string sql = "SELECT * FROM CadUsuario";

		    SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
		    SQLiteDataReader dr = comandoSQL.ExecuteReader();
		    string linhas = "";


                while (dr.Read())
                {
                    string linha = "<tr>";
		           linha += $"<th>{dr.GetInt32(0)}</th>";
                    linha += $"<th>{dr.GetString(1)}</th>";
                    linha += $"<th>{dr.GetString(2)}</th>"; 
                    linha += "<th><img src='/img/edit.png' onclick = \"editar()\"/></th>";
                    linha += $"<th> <img src='/img/delete.png' id =\"{dr.GetInt32(0)}\" onclick = \"excluir(event)\" > </th>"; 
                    
                    linha += "</tr>";
 
                    linhas += linha;
                }
	         sqlite_conn.Close();
                return linhas;
            }
            catch (Exception ex)
            {
                return $"Não foi possível consultar!!!{ex}";
            }
        }

        [HttpPost]
        public String Alterar(string nome, string email, string senha)
        {
            SQLiteConnection? sqlite_conn = null;
            try
            {
                sqlite_conn = PegarConexao();
                sqlite_conn.Open();
                string sql = $"update CadUsuario(nome,email,senha) values('{nome}','{email}' ,'{senha}');";
                SQLiteCommand comandoSQL = new SQLiteCommand(sql, sqlite_conn);
                string resposta;
                if (comandoSQL.ExecuteNonQuery() > 0)
                {
                    resposta = "Dados alterados com sucesso!!!";
                }
                else
                {
                    resposta = "Não foi possível alterar!!!";
                }
                sqlite_conn.Close();
                return resposta;
            }
            catch (Exception ex)
            {
                if (sqlite_conn != null)
                {
                    sqlite_conn.Close();
                }
                return "Não foi possível alterar!!!";
            }
        }


        public SQLiteConnection PegarConexao()
                {
	            string stringConnection = "Data Source = cadastro.db;";
	            return new SQLiteConnection(stringConnection);
                }

    }

    
}
