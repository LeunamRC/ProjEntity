using Dados;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjAula
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTable();
        }

        private void LoadTable()
        {
            DadosEntities context = new DadosEntities();
            List<tb_aula> list = context.tb_aula.ToList<tb_aula>();
            
            GDVAula.DataSource = list; //GDVAula.DataSource = new AulaDB().FindAll();
            GDVAula.DataBind();
        }

        protected void BtnInserir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastroAula.aspx");
        }

        protected void GDVAula_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int line = int.Parse(e.CommandArgument.ToString());
            int id = int.Parse(GDVAula.Rows[line].Cells[0].Text);

            //Aula aula = new AulaDB().FindById(id);
            DadosEntities context = new DadosEntities();
            tb_aula aula = context.tb_aula.First(c => c.Id == id);

            if (e.CommandName == "A")
            {
                Response.Redirect("CadastroAula.aspx?idItem=" + id);
            }
            else if (e.CommandName == "E")
            {
                lblExcluir.Text = id.ToString();
                lblMsg.Text = "Tem certeza que deseja excluir este registro?";
                DisplayModal(this);
            }
        }

        private void DisplayModal(Page page)
        {
            ClientScript.RegisterStartupScript(typeof(Page),
                                               Guid.NewGuid().ToString(),
                                               "MostrarModal();",
                                               true);
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            int id = int.Parse(lblExcluir.Text);

            DadosEntities context = new DadosEntities();
            tb_aula aula = context.tb_aula.First(c => c.Id == id);
            context.tb_aula.Remove(aula);
            context.SaveChanges();
            //new AulaDB().Delete(id);
            LoadTable();
        }
    }
}