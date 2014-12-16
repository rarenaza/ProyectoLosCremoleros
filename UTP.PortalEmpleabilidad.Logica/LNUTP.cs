﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UTP.PortalEmpleabilidad.Datos;
using UTP.PortalEmpleabilidad.Modelo;
using UTP.PortalEmpleabilidad.Modelo.Vistas.Ofertas;

namespace UTP.PortalEmpleabilidad.Logica
{
    public class LNUTP
    {
        ADUTP adUtp = new ADUTP();

        public DataTable OfertasObtenerPendientes()
        {
            
            return adUtp.OfertasObtenerPendientes();
        }
        public DataTable EmpresaObtenerPendientes()
        {

            return adUtp.EmpresaObtenerPendientes();
        }

        //public DataTable Empresa_ObtenerPorNombre(string nombre)
        //{

        //    return adUtp.Empresa_ObtenerPorNombre(nombre);
        //}

        //public DataTable Empresa_ObtenerPorNombre(string nombre)
        //{

        //    return adUtp.Empresa_ObtenerPorNombre(nombre);
        //}

        public List<EmpresaListaEmpresa> Empresa_ObtenerPorNombre(string nombre)
        {
            List<EmpresaListaEmpresa> listaEjemplo = new List<EmpresaListaEmpresa>();


            DataTable dtResultado = adUtp.Empresa_ObtenerPorNombre(nombre);

            for (int i = 0; i <= dtResultado.Rows.Count - 1; i++)
            {
                EmpresaListaEmpresa vista = new EmpresaListaEmpresa();
                vista.NombreComercial = dtResultado.Rows[i]["Nombre"].ToString();
                vista.RazonSocial = dtResultado.Rows[i]["Razon"].ToString();
                vista.RUC = dtResultado.Rows[i]["RUC"].ToString();
                vista.Estado = dtResultado.Rows[i]["Estado"].ToString();
                vista.SectorEmpresarial = dtResultado.Rows[i]["SectorEmpresarial"].ToString();

                listaEjemplo.Add(vista);
            }

            return listaEjemplo;
        }






    }
}