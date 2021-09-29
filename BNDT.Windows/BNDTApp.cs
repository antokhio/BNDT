using Stride.Engine;

namespace BNDT
{
    class BNDTApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
