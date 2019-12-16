using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using TaxiBotClassLibrary.Commands;

namespace TaxiBotClassLibrary
{
    public static class Configurator
    {
        public static List<Command> GetCommands()
        {
            var list = new List<Command>();
            var baseType = typeof(Command);
            var heirs = Assembly.GetAssembly(baseType).GetTypes().Where(c => c.IsSubclassOf(baseType));
            foreach (var heir in heirs)
                list.Add((Command)Activator.CreateInstance(heir));
            return list;
        }
        public static int index = 0;

        public static List<InterCommand> GetInternalCommand()
        {// заработает после /start
            var command = new List<InterCommand>();
            
            if (NDFAutomate<InterCommand>.States != null)
            {
                foreach(var com in NDFAutomate<InterCommand>.States[index].Transition.Keys)
                {
                    command.Add(com);
                }
            }
            //реализовать доступ к текущим командам через автомат
            return command;
        } 
        
    }
}
