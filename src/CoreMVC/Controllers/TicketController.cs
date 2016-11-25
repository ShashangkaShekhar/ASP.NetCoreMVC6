using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private TicketBookingContext _ctx = null;
        public TicketController(TicketBookingContext context)
        {
            _ctx = context;
        }

        // GET: api/Ticket/GetTicket
        [HttpGet("GetTicket"), Produces("application/json")]
        public async Task<object> GetTicket()
        {
            List<Ticket> Tickets = null;
            object result = null;
            try
            {
                using (_ctx)
                {
                    Tickets = await _ctx.Ticket.ToListAsync();
                    result = new
                    {
                        Tickets
                    };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return Tickets;
        }

        // GET api/Ticket/GetTicketByID/5
        [HttpGet("GetTicketByID/{id}"), Produces("application/json")]
        public async Task<Ticket> GetTicketByID(int id)
        {
            Ticket contact = null;
            try
            {
                using (_ctx)
                {
                    contact = await _ctx.Ticket.FirstOrDefaultAsync(x => x.TicketId == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return contact;
        }

        // POST api/Ticket/PostTicket
        [HttpPost, Route("PostTicket"), Produces("application/json")]
        public async Task<object> PostTicket([FromBody]Ticket model)
        {
            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        _ctx.Ticket.Add(model);
                        await _ctx.SaveChangesAsync();
                        _ctxTransaction.Commit();
                        message = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback();
                        e.ToString();
                        message = "Saved Error";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

        // PUT api/Ticket/PutTicket/5
        [HttpPut, Route("PutTicket/{id}")]
        public async Task<object> PutContact(int id, [FromBody]Ticket model)
        {
            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var entityUpdate = _ctx.Ticket.FirstOrDefault(x => x.TicketId == id);
                        if (entityUpdate != null)
                        {
                            entityUpdate.DestinationFrom = model.DestinationFrom;
                            entityUpdate.DestinationTo = model.DestinationTo;
                            entityUpdate.TicketFee = model.TicketFee;

                            await _ctx.SaveChangesAsync();
                        }
                        _ctxTransaction.Commit();
                        message = "Entry Updated";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        message = "Entry Update Failed!!";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

        // DELETE api/Ticket/DeleteTicketByID/5
        [HttpDelete, Route("DeleteTicketByID/{id}")]
        public async Task<object> DeleteContactByID(int id)
        {
            object result = null; string message = "";
            using (_ctx)
            {
                using (var _ctxTransaction = _ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var idToRemove = _ctx.Ticket.SingleOrDefault(x => x.TicketId == id);
                        if (idToRemove != null)
                        {
                            _ctx.Ticket.Remove(idToRemove);
                            await _ctx.SaveChangesAsync();
                        }
                        _ctxTransaction.Commit();
                        message = "Deleted Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        message = "Error on Deleting!!";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

    }
}
