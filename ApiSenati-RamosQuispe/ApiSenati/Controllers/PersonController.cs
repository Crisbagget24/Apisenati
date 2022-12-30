using ApiSenati.Models;
using ApiSenati.Models.Request;
using PersonModel.Response;
using PersonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PersonModel.Request;

namespace ApiSenati.Controllers
{
    public class PersonController : ApiController 
    {
        [HttpPost] 
        public IHttpActionResult AddPerson(PersonRequestV1 Person) {
            
            using (SENATI4Entities1 dbSenati = new SENATI4Entities1())
            {
                //INSERTAR DATOS
                if (Person.Id == 0)
                {
                    Person oPerson = new Person();
                    oPerson.Nombre = Person.Nombre;
                    oPerson.Ciudad = Person.Ciudad;
                    dbSenati.Person.Add(oPerson);
                }
                //Actualizar datos
                else
                {
                    Person oPerson = dbSenati.Person.Where(X => X.id == Person.Id).FirstOrDefault();
                    oPerson.Nombre = Person.Nombre;
                    oPerson.Ciudad = Person.Ciudad;
                    dbSenati.Entry(oPerson).State = System.Data.Entity.EntityState.Modified;
                    
                }
                dbSenati.SaveChanges();

            }
            return Ok("OPERACION REALIZADA CON EXITO");
        }
        //Mostrar Datos
        [HttpPost]
        [Route("getPerson")]
        public IHttpActionResult getPerson(PersonRequestV2 Person)
        {
            List<PersonResponseV1> PersonAll = new List<PersonResponseV1>();
            using (SENATI4Entities1 dbSenati = new SENATI4Entities1())
            {
                if (Person.des != "")
                {

                    PersonAll = (from p in dbSenati.Person
                                 where p.Nombre.ToString().Contains(Person.des)
                                 select new PersonResponseV1
                                 {
                                     Nombre = p.Nombre,
                                     Ciudad = p.Ciudad
                                 }).ToList();
                }
                else
                {
                    PersonAll = (from p in dbSenati.Person
                                 select new PersonResponseV1
                                 {
                                     Nombre = p.Nombre,
                                     Ciudad = p.Ciudad
                                 }).ToList();

                }
            }
            return Ok(PersonAll); 
        }
        [HttpPost]
        [Route("DeletePerson/{PersonId}")]
        public IHttpActionResult deletePerson(int PersonId)
        {
            using (SENATI4Entities1 dbSenati = new SENATI4Entities1())
            {
                Person oPerson = dbSenati.Person.Where(x => x.id == PersonId).FirstOrDefault();
                dbSenati.Person.Remove(oPerson);

                dbSenati.SaveChanges();
            }
            return Ok("Elemento Eliminado con Exito");
        }


    }
}
