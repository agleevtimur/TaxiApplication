using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TaxiBotClassLibrary.InterCommands;
using TaxiBotClassLibrary.Commands;
using Telegram.Bot.Types;
using DataBase.Classes;
using DataBase.Repository;

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

        public static List<string> Values = new List<string>();

        public static ICommand CurrentInterCommand => NDFAutomate<ICommand>.CurrentState.Transition.FirstOrDefault().Key;
    }
}
