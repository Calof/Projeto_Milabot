using Microsoft.AspNetCore.Mvc;

namespace Agendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult CadastroCliente()
        {
            return View();
        }
    }
}
