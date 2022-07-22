using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace AppWebMvcRazor.Pages.Forecast
{
    public class Vista2ForecastModel : PageModel
    {
        
        [BindProperty(Name = "id_product", SupportsGet = true)]
        public string id_product { get; set; }

        [BindProperty(Name = "name_product", SupportsGet = true)]
        public string name_product { get; set; }

        [BindProperty]
        public string message { get; set; }

        [BindProperty]
        public List<Producto> productos { get; set; } 

        ServiceProductos.ServiceProductosClient serviceProductos { get; set; }
        ServiceCategorias.ServiceCategoriasClient serviceCategorias { get; set; }
        
        [BindProperty]
        public List<Categoria> categorias { get; set; }

        public Vista2ForecastModel()
        {   
            productos = new List<Producto>();
            productosTemp = new List<Producto>();
            categorias = new List<Categoria>();
            serviceProductos = new ServiceProductos.ServiceProductosClient();
            serviceCategorias = new ServiceCategorias.ServiceCategoriasClient();
        }

        [BindProperty]
        public List<Producto> productosTemp { get; set; }

        public void OnGet()
        {
            OnPostLoadData();
        }

        public void OnGetLoad() {            
            try {

                if (id_product != null && name_product != null) {
                    System.Threading.Tasks.Task<bool> resultInsert = serviceProductos.registerProductAsync(int.Parse(id_product), name_product, -1);
                }
                OnPostLoadData();
            } catch {
                message = "Ocurrió un error al guardar el producto en BD";
            }           
        }

        public IActionResult OnPostRedirectVista1()
        {   
            return RedirectToPage("/productos/Vista1");
        }

        public void OnPostLoadData()
        {
            loadCategorias();
            loadProductos();            
        }
        public void OnPostLoadPeliculas()
        {
            System.Threading.Tasks.Task<string> jsonData = serviceProductos.findAllProductFilterJSONAsync(2);
            String json = jsonData.Result;
            productos = JsonConvert.DeserializeObject<List<Producto>>(json);
            loadCategorias();
        }
        
        public void loadCategorias()
        {
            try
            {

                System.Threading.Tasks.Task<string> jsonListCategory = serviceCategorias.findAllCategoryJSONAsync();
                String json = jsonListCategory.Result;
                categorias = JsonConvert.DeserializeObject<List<Categoria>>(json);
            }
            catch
            {
                message = "Ocurrio un error al Consultar el servicio del listado de categorias ";
            }
        }

        public void loadProductos()
        {
            try
            {

                System.Threading.Tasks.Task<string> jsonData = serviceProductos.findAllProductJSONAsync();
                String json = jsonData.Result;
                productos = JsonConvert.DeserializeObject<List<Producto>>(json);
            }
            catch
            {
                message = "Ocurrio un error al Consultar el servicio del listado de Productos ";
            }
        }

    }

    public class Categoria
    {
         
        public string ID_CATEGORY { get; set; }
                
        public string NAME_CATEGORY { get; set; }
               
    }


    public class Producto
    {
        [Required]
        [Display(Name = "Id Producto")]
        public string ID_PRODUCT { get; set; }

        [Required]
        [Display(Name = "Nombre Producto")]
        public string NAME_PRODUCT { get; set; }

        public string FK_ID_CATEGORY { get; set; }
    }

}
