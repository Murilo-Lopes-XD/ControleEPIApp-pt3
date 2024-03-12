using ControleEPIApp.Controller;
using ControleEPIApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControleEPIApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Clicked(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();
            funcionario.data_vencimento = DateTime.Now.AddDays(Double.Parse(txtValidade.Text));
            MySQLCon.InserirFuncionario(funcionario.nome, funcionario.matricula, funcionario.epi, funcionario.data_entrega, funcionario.data_vencimento);
            DisplayAlert("Inserção", "Pessoa Cadastrada com sucesso!", "OK");
            Navigation.PushAsync(new PageListar());
        }
    }
}