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
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            lsvFuncionarios.ItemsSource = MySQLCon.ListaFuncionario();
        }

        void NavegarFuncionario(Funcionario func)
        {
            PageUpDel upDel = new PageUpDel();
            upDel.funcionario = func;
            Navigation.PushAsync(upDel);
        }

        private void lsvFuncionarios_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
                NavegarFuncionario(e.SelectedItem as Funcionario);
        }

        private void btnNovo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageCadastrar());
        }
    }
}