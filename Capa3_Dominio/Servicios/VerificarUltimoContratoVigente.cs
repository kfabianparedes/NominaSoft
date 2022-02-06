using Capa3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa3_Dominio.Servicios
{
    public class VerificarUltimoContratoVigente
    {
        public bool verificarFechaFinAnteriorContrato(List<Contrato> listacontratos, Contrato contrato)
        {
            int comparar;
            Contrato contrato_enviado = new Contrato();
            foreach(Contrato c in listacontratos){
                if(listacontratos == null)
                {
                    return true;
                }
                else
                {
                    comparar = DateTime.Compare(contrato.FechaInicial.Date, c.FechaFinal.Date);
                    if (comparar > 0 && c.EstaAnulado == false)
                    {
                        return true;
                    }
                }
                
            }
            return false;
        }
        public bool esVigenteContratoAnterior(List<Contrato> listacontratos)
        {
            
            foreach (Contrato c in listacontratos)
            {
                if (listacontratos == null)
                {
                    return false; // 
                }
                else
                {
                    if (c.verificarEsVigente()==true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
