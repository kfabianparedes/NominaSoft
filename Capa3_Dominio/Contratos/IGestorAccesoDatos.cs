using System;
using System.Collections.Generic;
using System.Text;

namespace Capa3_Dominio.Contratos
{
    public interface IGestorAccesoDatos
    {
        void AbrirConexion();
        void CerrarConexion();
        void IniciarTransaccion();
        void TerminarTransaccion();
        void CancelarTransaccion();
    }
}
