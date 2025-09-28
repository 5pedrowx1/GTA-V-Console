using System;
using System.Threading.Tasks;

namespace ScriptHook_GTA5_Injector
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "ScriptHookV - Injector by 5pedrowx1";

            var config = new InjectorConfig();
            var injector = new ScriptHookInjector(config);

            var result = await injector.InjectAsync();

            Console.WriteLine($"\nOperação concluída: {result}");
            Console.WriteLine("Pressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}