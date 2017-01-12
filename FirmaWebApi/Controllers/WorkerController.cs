using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Description;
using FirmaWebApi.Attributes;
using FirmaWebApi.DAL;
using FirmaWebApi.Models;

namespace FirmaWebApi.Controllers
{
    public class WorkerController : ApiController
    {
        private AppDbContext _db = new AppDbContext();

        [AllowAnonymous]
        [HttpGet, Route("")]
        [ResponseHttpStatusCode(HttpStatusCode.OK)]
        [ResponseType(typeof(IEnumerable<Worker>))]
        public IHttpActionResult Get()
        {
            return Ok(_db.Workers.ToList());
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}", Name = "GetWorker")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        [ResponseType(typeof(Company))]
        public IHttpActionResult Get(int id)
        {
            if (!WorkerExist(id))
            {
                return NotFound();
            }
            
            return Ok(_db.Workers.Find(id));
        }

        private bool WorkerExist(int id)
        {
            return _db.Workers.Count(x => x.WorkerId == id) > 0;
        }

        [HttpPost, Route("")]
        [ResponseHttpStatusCode(HttpStatusCode.Created)]
        [ResponseType(typeof(Worker))]
        public IHttpActionResult Post([FromBody]Worker worker)
        {
            _db.Workers.Add(worker);
            _db.SaveChanges();
            return CreatedAtRoute("GetWorker", new { id = worker.WorkerId }, worker);
        }

        [HttpPut, Route("{id}")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, [FromBody]Worker worker)
        {
            _db.Entry(worker).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch
            {
                if (!WorkerExist(id))
                {
                    return NotFound();
                }
                throw;
            }
            return Ok(worker);
        }

      
        [HttpDelete, Route("{id}")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        public IHttpActionResult Delete(int id)
        {
            if (!WorkerExist(id))
            {
                return NotFound();
            }
            _db.Workers.Remove(_db.Workers.Find(id));
            return Ok();
        }
    }
}
