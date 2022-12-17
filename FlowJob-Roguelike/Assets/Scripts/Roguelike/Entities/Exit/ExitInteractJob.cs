using System.Threading.Tasks;
using Core.Tools;
using FlowJob;

namespace Roguelike
{
    public class ExitInteractJob : Job<Void, (Entity exit, Entity agent)>
    {
        protected override async Task<Void> Run((Entity exit, Entity agent) args)
        {
            Entity exit = args.exit;
            Entity agent = args.agent;

            // exit.Kill();
            
            return default;
        }
    }
}