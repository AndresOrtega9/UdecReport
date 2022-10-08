using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using Udec_Report.Models;
using System.Web.WebPages;

namespace Udec_Report.Controllers
{
    public class CreateReportController : Controller
    {
       
        // GET: CreateReport
        public ActionResult CreateReport()
        {

            using(RptsUdecReportEntities db=new RptsUdecReportEntities())
            {
                var lst = (from d in db.ur_reportes
                           select new Evento
                           {
                               idReportes = d.idReportes,
                               EventoId = d.idEvento,
                               idTipo = d.idTipo,
                               idZona = d.idZona,
                               idLugar = d.idLugar,
                               idAmbiente = d.idAmbiente,
                               descripcion = d.descripcion
                           }).ToList();
                    
            }


            RptsUdecReportEntities entities = new RptsUdecReportEntities();
            Evento model = new Evento();
            foreach (var evento in entities.ur_evento)
            {
                model.Eventos.Add(new SelectListItem { Text = evento.nombre, Value = evento.id.ToString() });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateReport(int? eventoId, int? tipoId, int?zonaId,int?lugarId,int?ambienteId)
        {

            //ur_imagenR modelImage=new ur_imagenR();
            //string fileName = Path.GetFileNameWithoutExtension(modelImage.ImageFile.FileName);
            //string extension = Path.GetExtension(modelImage.ImageFile.FileName);
            //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            //modelImage.rutaImagen = "~/Image/" + fileName;
            //fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            //modelImage.ImageFile.SaveAs(fileName);
            //using (RptsUdecReportEntities db = new RptsUdecReportEntities())
            //{
            //    db.ur_imagenR.Add(modelImage);
            //    db.SaveChanges();
            //}
           
            Evento model = new Evento();
            RptsUdecReportEntities entities = new RptsUdecReportEntities();
            foreach (var evento in entities.ur_evento)
            {
                model.Eventos.Add(new SelectListItem { Text = evento.nombre, Value = evento.id.ToString() });
            }

            if (eventoId.HasValue)
            {
                var tipo = (from t in entities.ur_tipoEvento
                            where t.idEvento == eventoId.Value
                            select t).ToList();
                foreach (var t in tipo)
                {
                    model.Tipos.Add(new SelectListItem { Text = t.nombre, Value = t.id.ToString() });
                }
            }


            //Zonas

            foreach (var zona in entities.ur_ddlZona)
            {
                model.Zonas.Add(new SelectListItem { Text = zona.nombreZona, Value = zona.idZona.ToString() });
            }

            if (zonaId.HasValue)
            {
                var lugares = (from lugar in entities.ur_ddlLugar
                              where lugar.idZona == zonaId.Value
                              select lugar).ToList();
                foreach (var lugar in lugares)
                {
                    model.Lugares.Add(new SelectListItem { Text = lugar.nombreLugar, Value = lugar.idLugar.ToString() });
                }

                if (lugarId.HasValue)
                {
                    var ambientes = (from ambiente in entities.ur_ddlAmbiente
                                  where ambiente.idLugar == lugarId.Value
                                  select ambiente).ToList();
                    foreach (var ambiente in ambientes)
                    {
                        model.Ambientes.Add(new SelectListItem { Text = ambiente.nombreAmbiente, Value = ambiente.idAmbiente.ToString() });
                    }
                }
            }

            try
            {
                if (ModelState.IsValid)
                {
                    using(RptsUdecReportEntities db= new RptsUdecReportEntities())
                    {
                        var otabla = new ur_reportes();
                        otabla.idEvento = model.EventoId;
                        otabla.idTipo = model.TipoId;

                        db.ur_reportes.Add(otabla);
                        db.SaveChanges();
                    }
                    return Redirect("CreateReport/");
                }
                return View(model);
            }
            catch
            {

            }

            return View(model);
        }

    }
}