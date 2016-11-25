using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreMVC.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreMVC.Controllers
{
    public class TicketsController : Controller
    {
        private TicketBookingContext _ctx = null;
        public TicketsController(TicketBookingContext context)
        {
            _ctx = context;
        }

        // GET: Tickets/Index/
        public async Task<IActionResult> Index()
        {
            List<Ticket> tickets = null;
            try
            {
                tickets = await _ctx.Ticket.ToListAsync();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return View(tickets);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }


        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _ctx.Add(ticket);
                    await _ctx.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            return View(ticket);
        }


        // GET: Tickets/Details/5 
        public async Task<IActionResult> Details(int? id)
        {
            object ticket = null;
            try
            {
                if ((id != null) && (id > 0))
                {
                    ticket = await _ctx.Ticket.SingleOrDefaultAsync(m => m.TicketId == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            object ticket = null;
            try
            {
                if ((id != null) && (id > 0))
                {
                    ticket = await _ctx.Ticket.SingleOrDefaultAsync(m => m.TicketId == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View(ticket);
        }

        [ValidateAntiForgeryToken, HttpPost]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id == ticket.TicketId)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _ctx.Update(ticket);
                        await _ctx.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException dce)
                    {
                        if (!TicketExists(ticket.TicketId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            dce.ToString();
                        }
                    }
                    return RedirectToAction("Index");
                }
            }

            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            object ticket = null;

            try
            {
                if ((id != null) && (id > 0))
                {
                    ticket = await _ctx.Ticket.SingleOrDefaultAsync(m => m.TicketId == id);
                    if (ticket == null)
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [ValidateAntiForgeryToken, HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var ticket = await _ctx.Ticket.SingleOrDefaultAsync(m => m.TicketId == id);
                _ctx.Ticket.Remove(ticket);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return RedirectToAction("Index");
        }

        private bool TicketExists(int id)
        {
            return _ctx.Ticket.Any(e => e.TicketId == id);
        }
    }
}
