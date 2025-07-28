using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class ChatCharacter
    {
        public string Name { get; set; } = "The Rizzler";
        public string Id { get; set; } = "Rizz";
        public bool IsAlive { get; set; } = true;
        public ChatFightStatus Status { get; set; } = ChatFightStatus.walk;
        public int MaxHealth { get; set; } = 2;
        public int Health { get; set; } = 2;
        public int Damage { get; set; } = 1;
    }
}
