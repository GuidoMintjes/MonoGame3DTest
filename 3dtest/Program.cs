using System;

namespace TDTestGame {
    public static class Program {
        [STAThread]
        static void Main() {

            using (var game = new TDTestGame())
                game.Run();
        }
    }
}