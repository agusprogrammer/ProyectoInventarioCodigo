using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace dam.di.inventario.clase.Validacion
{
    public class ValidaMVC
    {
        //este es un documento con metodos de validacion para
        //los formularios MVC
        
        //Nota: si se pone static (public static bool) no funcionara, 
        //porque con static no cambia el bool
        
        //Ver si esta vacio (uso textbox)
        public bool validaCadena(string cad)
        {
            bool correcto = false;
            if (string.IsNullOrEmpty(cad)) correcto = true;
            return correcto;
        }

        //Comprobar objeto vacio (uso para combobox)
        public bool objetoVacio(object obj)
        {
            bool correcto = false;
            if (obj == null) correcto = true;
            return correcto;
        }

        //comprobar por rango
        public bool rango(int min, int max)
        {
            bool correcto = true;
            //Por completar
            return correcto;
        }

        //Mostrar y quitar error sin imagen
        public static void muestraError(Control crtl)
        {
            crtl.BorderBrush = Brushes.Red; //Red
            //img.Visibility = Visibility.Visible;


            crtl.Effect = new DropShadowEffect
            {
                Color = new Color { A = 0, R = 210, G = 0, B = 0 },
                Direction = -30,
                ShadowDepth = 4, //si se pone 1 el efecto es mas estrecho
                BlurRadius = 0,
                Opacity = 1
            };

        }

        public static void quitarError(Control crtl)
        {
            crtl.BorderBrush = Brushes.Black;
            //img.Visibility = Visibility.Hidden;
            crtl.Effect = null;
        }

        //Mostrar y quitar error con imagen
        public static void muestraError(Control crtl, Image img)
        {
            crtl.BorderBrush = Brushes.Red; //Red
            img.Visibility = Visibility.Visible;

            
            crtl.Effect = new DropShadowEffect
            {
                Color = new Color { A = 0, R = 210, G = 0, B = 0 },
                Direction = -30,
                ShadowDepth = 4, //si se pone 1 el efecto es mas estrecho
                BlurRadius = 0,
                Opacity = 1
            };
            
        }

        public static void quitarError(Control crtl, Image img)
        {
            crtl.BorderBrush = Brushes.Black;
            img.Visibility = Visibility.Hidden;
            crtl.Effect = null;
        }

    }
}
