using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TestProject.Models;

namespace TestProject.SignalR
{
    public class PageUpdate : Hub
    {
        private ApplicationContext db;
        public PageUpdate(ApplicationContext context)
        {
            db = context;
        }
        public async Task Send(string ch)
        {
            
            if (ch == "All")
            {
                var sortList = db.Subjects.ToList();
                await Clients.All.SendAsync("Send", sortList);
            }
            else
            {
                var sortList = db.Subjects.Where(t => t.Type == ch).ToList();
                await Clients.All.SendAsync("Send", sortList);
            }
            //извлечение из бд
           
        }
       
    }
}
