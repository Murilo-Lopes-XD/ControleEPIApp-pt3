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
    public partial class PageUpDel : ContentPage
    {
        public Funcionario funcionario;
        public PageUpDel()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = this.funcionario;
        }

        private void btnAtualizar_Clicked(object sender, EventArgs e)
        {
            funcionario.data_vencimento = DateTime.Now.AddDays(Double.Parse(txtValidade.Text));
            MySQLCon.AtualizarFuncionario(funcionario.nome, funcionario.matricula, funcionario.epi, funcionario.data_entrega, funcionario.data_vencimento);
            DisplayAlert("Edição", "Pessoa atualizada com sucesso!", "OK");
        }

        private void btnExcluir_Clicked(object sender, EventArgs e)
        {
            if (funcionario.matricula != null)
            {
                MySQLCon.ExcluirPessoa(funcionario);
                DisplayAlert("Exclusão", "Pessoa excluida com sucesso!", "OK");
                Navigation.PopAsync();
            }
        }
    }
}