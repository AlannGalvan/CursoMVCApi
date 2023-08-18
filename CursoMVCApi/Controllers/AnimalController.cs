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
                    List<ListAnimalesViewModel> lst = (from d in db.animal
                                                      where d.idState == 1
                                                      select new ListAnimalesViewModel
                                                      {
                                                          Name = d.name,
                                                          Patas = d.patas
                                                      }).ToList();
                    oR.data = lst;
                    oR.result = 1; 
                }
            }
            catch (Exception ex)
            {

                oR.message = "Ocurrio un error en el servidor, intente mas tarde";
            }

            return oR;
        }

        
    }
}
