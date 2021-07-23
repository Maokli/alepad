using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedRooms(DataContext context) 
        {
            if(await context.ChatRooms.AnyAsync()) return;
            var names = new List<string>{
                "General Realm",
                "Chill Realm",  
                "Cherry Talks",  
                "Aries Palace",  
            };
            foreach(string name in names){
                var chatRoom = new ChatRoom{
                    Name = name
                };
                await context.ChatRooms.AddAsync(chatRoom);
                await context.SaveChangesAsync();
            }
        }
    }
}