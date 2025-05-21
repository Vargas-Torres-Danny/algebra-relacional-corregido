using algebra_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Algebra_2
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        BDVentasEntities ventas = new BDVentasEntities();

        protected void Page_Load(object sender, EventArgs e) { }

        // ------------------- CONSULTAS UNARIAS -------------------

        protected void btnConsulta1_Click(object sender, EventArgs e)
        {
            gvConsulta.DataSource = ventas.TCliente.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta2_Click(object sender, EventArgs e)
        {
            gvConsulta.DataSource = ventas.TVendedor.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta3_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TCliente
                .Where(c => c.CodCliente == "C0001");
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta4_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TCategoria
                .Where(t => t.Nombre == "Frutas");
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta5_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TProducto
                .Select(p => new { p.Nombre });
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta6_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TCliente
                .Select(c => new
                {
                    Codigo = c.CodCliente,
                    ApellidoCliente = c.Apellidos,
                    NombreCliente = c.Nombres
                });
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta7_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TProducto
                .Where(p => p.Precio > 10);
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta8_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TVendedor
                .Where(v => v.Apellidos.StartsWith("A"));
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta9_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TBoleta
                .Select(b => new { b.Fecha });
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta10_Click(object sender, EventArgs e)
        {
            var consulta = ventas.TBoleta
                .Select(b => new
                {
                    Numero = b.NroBoleta,
                    Dia = b.Fecha,
                    EstaAnulado = b.Anulado
                });
            gvConsulta.DataSource = consulta.ToList();
            gvConsulta.DataBind();
        }

        // ------------------- CONSULTAS BINARIAS -------------------

        protected void btnConsulta11_Click(object sender, EventArgs e)
        {
            var consultaA = ventas.TCliente
                .Where(c => c.Apellidos.StartsWith("A"))
                .Select(c => new { c.CodCliente, c.Apellidos });

            var consultaB = ventas.TCliente
                .Where(c => c.Apellidos.StartsWith("B"))
                .Select(c => new { c.CodCliente, c.Apellidos });

            gvConsulta.DataSource = consultaA.Union(consultaB).ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta12_Click(object sender, EventArgs e)
        {
            var consulta1 = ventas.TProducto
                .Where(p => p.Stock > 50)
                .Select(p => new { p.Nombre });

            var consulta2 = ventas.TProducto
                .Where(p => p.Precio < 10)
                .Select(p => new { p.Nombre });

            gvConsulta.DataSource = consulta1.Union(consulta2).ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta13_Click(object sender, EventArgs e)
        {
            var consulta1 = ventas.TCategoria
                .Where(c => c.CategoriaPadre == null)
                .Select(c => new { c.CodCategoria });

            var consulta2 = ventas.TCategoria
                .Where(c => c.CategoriaPadre != null)
                .Select(c => new { c.CodCategoria });

            gvConsulta.DataSource = consulta1.Union(consulta2).ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta14_Click(object sender, EventArgs e)
        {
            var todos = ventas.TBoleta
                .Select(b => new { b.CodCliente });

            var anulados = ventas.TBoleta
                .Where(b => b.Anulado == true)
                .Select(b => new { b.CodCliente });

            gvConsulta.DataSource = todos.Except(anulados).ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta15_Click(object sender, EventArgs e)
        {
            var todos = ventas.TVendedor
                .Select(v => new { v.CodVendedor });

            var enBoletas = ventas.TBoleta
                .Select(b => new { b.CodVendedor })
                .Distinct();

            gvConsulta.DataSource = todos.Except(enBoletas).ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta16_Click(object sender, EventArgs e)
        {
            var producto = from c in ventas.TCliente
                           from v in ventas.TVendedor
                           select new
                           {
                               Cliente = c.Nombres + " " + c.Apellidos,
                               Vendedor = v.Nombres + " " + v.Apellidos
                           };
            gvConsulta.DataSource = producto.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta17_Click(object sender, EventArgs e)
        {
            var producto = from p in ventas.TProducto
                           from c in ventas.TCategoria
                           select new
                           {
                               Producto = p.Nombre,
                               Categoria = c.Nombre
                           };
            gvConsulta.DataSource = producto.ToList();
            gvConsulta.DataBind();
        }

        protected void btnConsulta18_Click(object sender, EventArgs e)
        {
            var producto = from v in ventas.TVendedor
                           from c in ventas.TCategoria
                           select new
                           {
                               Vendedor = v.Nombres + " " + v.Apellidos,
                               Categoria = c.Nombre
                           };
            gvConsulta.DataSource = producto.ToList();
            gvConsulta.DataBind();
        }
    }
}