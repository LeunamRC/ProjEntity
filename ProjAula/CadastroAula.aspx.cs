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
    public partial class CadastroAula : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDataPage();
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {

            string idItem = Request.QueryString["idItem"];

            DadosEntities context = new DadosEntities();
            tb_aula aula = new tb_aula()
            {
                nomedisciplina = TxtNomeDisciplina.Text,
                qtd_alunos = int.Parse(TxtQtd_Alunos.Text),
                nomeprofessor = TxtNomeProfessor.Text,
                nomefaculdade = TxtNomeFaculdade.Text
            };
            
            /*Aula aula = new Aula()
            {
                NomeDisciplina = TxtNomeDisciplina.Text,
                Qtd_Alunos = int.Parse(TxtQtd_Alunos.Text),
                NomeProfessor = TxtNomeProfessor.Text,
                NomeFaculdade = TxtNomeFaculdade.Text
            };*/

            if (String.IsNullOrEmpty(idItem))
            {
                //new AulaDB().Save(aula);
                context.tb_aula.Add(aula);
                lblMSG.Text = "Registro Inserido!";
            }
            else
            {
                //aula.Id = int.Parse(idItem);
                //new AulaDB().Update(aula);
                int idNew = int.Parse(idItem);
                tb_aula b = context.tb_aula.First(c => c.Id == idNew);
                b.nomedisciplina = TxtNomeDisciplina.Text;
                b.qtd_alunos = int.Parse(TxtQtd_Alunos.Text);
                b.nomeprofessor = TxtNomeProfessor.Text;
                b.nomefaculdade = TxtNomeFaculdade.Text;
                lblMSG.Text = "Registro Atualizado!";
            }
            context.SaveChanges();
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        private void LoadDataPage()
        {
            string idItem = Request.QueryString["idItem"];

            if (!String.IsNullOrEmpty(idItem))
            {
                //Aula aula = new AulaDB().FindById(int.Parse(idItem));
                DadosEntities context = new DadosEntities();
                int idNew = int.Parse(idItem);
                tb_aula aula = context.tb_aula.First(c => c.Id == idNew);

                lblId.Visible = true;
                TxtId.Visible = true;

                /*
                TxtQtd_Alunos.Text = aula.Qtd_Alunos.ToString();
                TxtNomeDisciplina.Text = aula.NomeDisciplina;
                TxtId.Text = aula.Id.ToString();
                TxtNomeProfessor.Text = aula.NomeProfessor;
                TxtNomeFaculdade.Text = aula.NomeFaculdade;
                */

                TxtQtd_Alunos.Text = aula.qtd_alunos.ToString();
                TxtNomeDisciplina.Text = aula.nomedisciplina;
                TxtId.Text = aula.Id.ToString();
                TxtNomeProfessor.Text = aula.nomeprofessor;
                TxtNomeFaculdade.Text = aula.nomefaculdade;
            }
        }
    }
}