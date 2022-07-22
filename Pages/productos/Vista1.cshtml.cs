using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AppWebMvcRazor.Pages.Forecast
{
    public class Vista1ForecastModel : PageModel
    {

        [BindProperty]
        public string message { get; set; }

        [BindProperty]
        public Producto producto { get; set; }

        public IActionResult OnPostRedirectListarProductos()
        {
            if (!ModelState.IsValid)
            {                
                return Page();
            }
                         
            message = string.Empty;
            
            return RedirectToPage("/productos/Vista2", "Load", new { id_product = producto.ID_PRODUCT, name_product = producto.NAME_PRODUCT });

        }
    }

}
