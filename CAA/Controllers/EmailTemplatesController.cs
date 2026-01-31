using Microsoft.AspNetCore.Mvc;

namespace CAA.Controllers
{
    public class EmailTemplatesController : Controller
    {
        public IActionResult EmailImage(string imgName)
        {
            // Caminho para a pasta de imagens
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgName);

            // Verifica se o arquivo existe
            if (!System.IO.File.Exists(imgPath))
            {
                return NotFound();
            }

            // Detecta o tipo de imagem
            string contentType = "image/png"; // padrão

            // Você pode determinar o content-type pela extensão
            if (imgPath.EndsWith(".jpg") || imgPath.EndsWith(".jpeg"))
                contentType = "image/jpeg";
            else if (imgPath.EndsWith(".svg"))
                contentType = "image/svg+xml";
            else if (imgPath.EndsWith(".gif"))
                contentType = "image/gif";

            // Lê o arquivo e retorna como FileResult
            byte[] imageData = System.IO.File.ReadAllBytes(imgPath);
            return File(imageData, contentType);
        }
    }
}
