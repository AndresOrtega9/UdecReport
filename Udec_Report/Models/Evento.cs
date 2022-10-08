using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Udec_Report.Models
{

    public class Evento
    {
        public Evento()
        {
            this.Eventos = new List<SelectListItem>();
            this.Tipos = new List<SelectListItem>();

            this.Zonas = new List<SelectListItem>();
            this.Lugares = new List<SelectListItem>();
            this.Ambientes = new List<SelectListItem>();
    }

        public List<SelectListItem> Eventos { get; set; }
        public List<SelectListItem> Tipos { get; set; }

        public List<SelectListItem> Zonas { get; set; }
        public List<SelectListItem> Lugares { get; set; }
        public List<SelectListItem> Ambientes { get; set; }


        //Tipos
        public int EventoId { get; set; }
        public int TipoId { get; set; }

        //Zonas
        public int ZonaId { get; set; }
        public int LugarId { get; set; }
        public int AmbienteId { get; set; }

        //Imagen
        public int idImagen { get; set; }
        public string rutaImagen { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }

        //Reportes
        public int idReportes { get; set; }
        public int idEvento { get; set; }
        public int idTipo { get; set; }
        public int idZona { get; set; }
        public int idLugar { get; set; }
        public int idAmbiente { get; set; }
        public string descripcion { get; set; }


    }
}