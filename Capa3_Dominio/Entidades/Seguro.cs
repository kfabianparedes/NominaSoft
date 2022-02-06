using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Entidades
{
    public class Seguro
    {
        private int idSeguro;
        private String afp;
        private double porcentajeDescuento;
        public Seguro()
        {

        }
        public Seguro(int idSeguro, string afp, double porcentajeDescuento)
        {
            this.idSeguro = idSeguro;
            this.afp = afp;
            this.porcentajeDescuento = porcentajeDescuento;
        }

        public int IdSeguro { get => idSeguro; set => idSeguro = value; }
        public string Afp { get => afp; set => afp = value; }
        public double PorcentajeDescuento { get => porcentajeDescuento; set => porcentajeDescuento = value; }
    
        
    }
}
