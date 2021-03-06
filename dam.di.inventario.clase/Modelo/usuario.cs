//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dam.di.inventario.clase.Modelo
{
    using dam.di.inventario.clase.MVVM;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class usuario : MVBase
    {
        public usuario()
        {
            this.articulo = new HashSet<articulo>();
            this.articulo1 = new HashSet<articulo>();
            this.ficherousuario = new HashSet<ficherousuario>();
            this.salida = new HashSet<salida>();
        }
        //Para comprobar inserta MVC, data annotations comentadas

        //si es auto incremental no hace falta el required
        public int idusuario { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio")] //Campo requerido
        //[MinLength(6, ErrorMessage = "La longitud minima es de 6 caracteres")]
        public string username { get; set; }
        //Como usamos un campo passwordbox, no hay que poner las data annotations
        //[Required] //Tiene que comprobarse por codigo, valida, pero no lo senyala
        //[StringLength(10, MinimumLength = 6)]
        public string password { get; set; }
        public int tipo { get; set; } //En los combos los required van abajo en los objetos
        public int rol { get; set; }
        public string grupo { get; set; }
        public Nullable<int> departamento { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string domicilio { get; set; }
        public string poblacion { get; set; }
        //[StringLength(5)] //obligatorio poner 5 digitos, se puede dejar el campo en blanco
        //Da problemas con el formulario usuario MVC, mirar lo comentado del formulario MVVM
        //Para evitar el problema poner los include en el formulario MVVM
        //[StringLength(5)]
        public string codpostal { get; set; }
        //[EmailAddress] //Da problemas con el formulario usuario MVC, mirar lo comentado del formulario MVVM
        public string email { get; set; }
        //[StringLength(9)] //Obligatorio poner 9 digitos, se puede dejar el campo en blanco
        //Da problemas con el formulario usuario MVC, mirar lo comentado del formulario MVVM
        //Para evitar el problema poner los include en el formulario MVVM
        //[StringLength(9)]
        public string telefono { get; set; }
    
        public virtual ICollection<articulo> articulo { get; set; }
        public virtual ICollection<articulo> articulo1 { get; set; }
        public virtual departamento departamento1 { get; set; }
        public virtual ICollection<ficherousuario> ficherousuario { get; set; }
        public virtual grupo grupo1 { get; set; }
        [Required]
        public virtual rol rol1 { get; set; }
        public virtual ICollection<salida> salida { get; set; }
        [Required]
        public virtual tipousuario tipousuario { get; set; }
    }
}
