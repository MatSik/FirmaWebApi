using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FirmaWebApi.Attributes;
using FirmaWebApi.DAL;
using FirmaWebApi.Models;

namespace FirmaWebApi.Controllers
{
    [RoutePrefix("api/company")]
    public class CompanyController : ApiController
    {
        private AppDbContext _db = new AppDbContext();

        [AllowAnonymous]
        [HttpGet, Route("")]
        [ResponseHttpStatusCode(HttpStatusCode.OK)]
        [ResponseType(typeof(IEnumerable<Company>))]
        public IHttpActionResult Get()
        {
            return Ok(_db.Companies.ToList());
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}", Name = "GetCompany")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        [ResponseType(typeof(Company))]
        public IHttpActionResult Get(int id)
        {
            if (!CompanyExists(id))
            {
                return NotFound();
            }
            return Ok(_db.Companies.Find(id));
        }

        [HttpPost, Route("")]
        [ResponseHttpStatusCode(HttpStatusCode.Created)]
        [ResponseType(typeof(Company))]
        public IHttpActionResult Post([FromBody]Company company)
        {
            _db.Companies.Add(company);
            _db.SaveChanges();
            return CreatedAtRoute("GetCompany",new {id = company.CompanyId},company);

        }

      
        [HttpPut, Route("{id}")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        public IHttpActionResult Put(int id, [FromBody]Company company)
        {
            _db.Entry(company).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(company);
        }

        private bool CompanyExists(int id)
        {
            return _db.Companies.Count(x => x.CompanyId == id) > 0;
        }

        [HttpDelete, Route("{id}")]
        [ResponseHttpStatusCode(HttpStatusCode.OK, HttpStatusCode.NotFound)]
        public IHttpActionResult Delete(int id)
        {
            if (!CompanyExists(id))
            {
                return NotFound();
            }
            _db.Companies.Remove(_db.Companies.Find(id));
            _db.SaveChanges();
            return Ok();
        }
    }
}
