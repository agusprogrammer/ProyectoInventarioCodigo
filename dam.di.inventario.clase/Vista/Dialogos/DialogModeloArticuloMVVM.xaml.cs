using dam.di.inventario.clase.Modelo;
using dam.di.inventario.clase.MVVM;
using dam.di.inventario.clase.Servicios;
using dam.di.inventario.Servicios;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace dam.di.inventario.clase.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogModeloArticuloMVVM.xaml
    /// </summary>
    public partial class DialogModeloArticuloMVVM : Window
    {
        //Primera forma de realizar un formulario MVVM de insertar

        private diinventarioEntities invEnt;
        private MVModelo mvMod;
        private bool edicion; //bool para activar la edicion

        //constructor modificado para permitir la edicion
        public DialogModeloArticuloMVVM(diinventarioEntities ent, modeloarticulo modeloSelect, bool _edicion)
        {
            InitializeComponent();
            invEnt = ent;
            //cosas mv
            mvMod = new MVModelo(invEnt);
            DataContext = mvMod; //aqui decimos que los objetos de los
            //bindings los buscaremos en la clase MVModelo
            //En esta definiremos los objetos que se relacionen 
            //con nuestra vista

            //deshabilitar boton de guardar en funcion de si hay errores o no
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mvMod.OnErrorEvent));
            mvMod.btnGuardar = btnGuardar;

            //Modificacion para edicion
            //Edicion
            edicion = _edicion;
            //Si la edicion es true, editamos
            if (edicion)
            {
                mvMod.modeloNuevo = modeloSelect;
            }

        }


        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            //modificacion de la edicion
            if (edicion)
            {
                if (mvMod.edita())
                {
                    MessageBox.Show("Edicion ralizada correctamente", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Error al realizar la edicion", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (mvMod.guarda())
                {
                    MessageBox.Show("Insercción ralizada correctamente", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Error al realizar la insercción", "GESTION INVENTARIO", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
