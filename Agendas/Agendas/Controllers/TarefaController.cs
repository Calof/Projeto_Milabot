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
		            linha += "<td><img src='/img/edit.png'/></td>";
                    linha += $"<td> <input type=\"button\" value=\"excluir\" onclick = \"excluir(event)\" id =\"{dr.GetInt32(0)}\" > <img src='/img/delete.png'> </td>"; 
                    linha += $"<td>{dr.GetInt32(0)}</td>";
                    linha += $"<td>{dr.GetString(1)}</td>";
                    linha += $"<td>{dr.GetString(2)}</td>";
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
 
        public SQLiteConnection PegarConexao()
                {
	            string stringConnection = "Data Source = cadastro.db;";
	            return new SQLiteConnection(stringConnection);
                }

    }

    
}
