using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CursoMVCApi.Models.WS;
using CursoMVCApi.Models;


namespace CursoMVCApi.Controllers
{
    public class AnimalController : BaseController
    {
        [HttpPost]
        public Reply Get([FromBody]SecurityViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }

            try
            {
                using (CursoMVCApiEntities db = new CursoMVCApiEntities())
                {
                    List<ListAnimalesViewModel> lst = List(db);
                    oR.result = 1;
                    oR.data = lst;
                }
            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un error en el servidor, intente mas tarde";
            }

            return oR;
        }

        [HttpPost]
        public Reply Add([FromBody]AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }
            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (CursoMVCApiEntities db = new CursoMVCApiEntities())
                {
                    animal oAnimal = new animal();
                    oAnimal.idState = 1;
                    oAnimal.name = model.Name;
                    oAnimal.patas = model.Patas;

                    db.animal.Add(oAnimal);
                    db.SaveChanges();


                    List<ListAnimalesViewModel> lst = List(db);
                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error en el servidor, intente mas tarde";
            }

            return oR;
        }


        [HttpPut]
        public Reply Edit([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }
            //validaciones
            if (!Validate(model))
            {
                oR.message = error;
                return oR;
            }

            try
            {
                using (CursoMVCApiEntities db = new CursoMVCApiEntities())
                {
                    animal oAnimal = db.animal.Find(model.id);
                    oAnimal.name = model.Name;
                    oAnimal.patas = model.Patas;
                    db.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                    List<ListAnimalesViewModel> lst = List(db);
                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error en el servidor, intente mas tarde";
            }

            return oR;
        }

        [HttpPost]
        public Reply Delete([FromBody] AnimalViewModel model)
        {
            Reply oR = new Reply();
            oR.result = 0;

            if (!Verify(model.token))
            {
                oR.message = "No autorizado";
                return oR;
            }

            try
            {
                using (CursoMVCApiEntities db = new CursoMVCApiEntities())
                {
                    animal oAnimal = db.animal.Find(model.id);
                    oAnimal.idState = 2;
                    db.Entry(oAnimal).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();


                    List<ListAnimalesViewModel> lst = List(db);
                    oR.result = 1;
                    oR.data = lst;

                }
            }
            catch (Exception ex)
            {
                oR.message = "Ocurrio un error en el servidor, intente mas tarde";
            }

            return oR;
        }

        #region HELPERS

        private bool Validate(AnimalViewModel model)
        {
            if (model.Name == "")
            {
                error = "El nombre es obligatorio";
                return false;
            }
            return true;
        }

        private List<ListAnimalesViewModel> List(CursoMVCApiEntities db)
        {
            List<ListAnimalesViewModel> lst = (from d in db.animal
             where d.idState == 1
             select new ListAnimalesViewModel
             {
                 Name = d.name,
                 Patas = d.patas
             }).ToList();

            return lst;
        }

        #endregion
    }
}
